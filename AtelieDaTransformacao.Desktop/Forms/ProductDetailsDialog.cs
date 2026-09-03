using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Forms;

public sealed partial class ProductDetailsDialog : Form
{
    private readonly ProductDto _product;

    private static readonly Image _imagePlaceholder =
        CreateImagePlaceholder();

    // Construtor usado pelo Windows Forms Designer.
    public ProductDetailsDialog()
    {
        _product = new ProductDto
        {
            Title = "Produto de exemplo",
            CategoryName = "Categoria",
            Price = 0m,
            StockQuantity = 0,
            Description = "Descrição de exemplo.",
            Image = string.Empty,
            IsFeatured = false
        };

        InitializeComponent();

        _closeButton.Click += CloseButton_Click;

        Load += async (_, _) =>
            await LoadProductAsync();
    }

    public ProductDetailsDialog(ProductDto product)
    {
        _product =
            product ?? throw new ArgumentNullException(nameof(product));

        InitializeComponent();

        _closeButton.Click += CloseButton_Click;

        Load += async (_, _) =>
            await LoadProductAsync();
    }

    // ================================================================
    // FECHAR
    // ================================================================

    private void CloseButton_Click(
        object? sender,
        EventArgs e)
    {
        Close();
    }

    // ================================================================
    // CARREGAR PRODUTO
    // ================================================================

    private async Task LoadProductAsync()
    {
        _titleLabel.Text =
            _product.Title;

        _categoryValue.Text =
            string.IsNullOrWhiteSpace(_product.CategoryName)
                ? "Sem categoria"
                : _product.CategoryName;

        _priceValue.Text =
            _product.Price.ToString(
                "C2",
                CultureInfo.GetCultureInfo("pt-BR"));

        _stockValue.Text =
            _product.StockQuantity.ToString();

        _statusValue.Text =
            _product.StockQuantity == 0
                ? "Sem estoque"
                : _product.StockQuantity <= 5
                    ? "Baixo"
                    : "Disponível";

        _featuredValue.Text =
            _product.IsFeatured
                ? "Sim"
                : "Não";

        _descriptionValue.Text =
            string.IsNullOrWhiteSpace(_product.Description)
                ? "Sem descrição cadastrada."
                : _product.Description;

        _imageValue.Text =
            string.IsNullOrWhiteSpace(_product.Image)
                ? "Sem imagem cadastrada."
                : _product.Image;

        // ------------------------------------------------------------
        // COR DO STATUS
        // ------------------------------------------------------------

        _statusValue.ForeColor =
            _product.StockQuantity == 0
                ? Color.FromArgb(192, 0, 0)
                : _product.StockQuantity <= 5
                    ? Color.FromArgb(180, 120, 15)
                    : Color.FromArgb(35, 145, 55);

        // ------------------------------------------------------------
        // PLACEHOLDER INICIAL
        // ------------------------------------------------------------

        _pictureBox.Image =
            _imagePlaceholder;

        if (string.IsNullOrWhiteSpace(_product.Image))
            return;

        // ------------------------------------------------------------
        // CARREGAR IMAGEM
        // ------------------------------------------------------------

        var imageKey =
            NormalizeImageKey(_product.Image);

        if (string.IsNullOrWhiteSpace(imageKey))
            return;

        var image =
            await LoadProductImageAsync(
                imageKey,
                _product.Image);

        if (IsDisposed)
        {
            image?.Dispose();
            return;
        }

        if (image is null)
            return;

        var oldImage =
            _pictureBox.Image;

        _pictureBox.Image =
            image;

        if (oldImage is not null &&
            !ReferenceEquals(
                oldImage,
                _imagePlaceholder))
        {
            oldImage.Dispose();
        }
    }

    // ================================================================
    // CARREGAMENTO DA IMAGEM
    // ================================================================

    private static async Task<Image?> LoadProductImageAsync(
        string imageKey,
        string originalValue)
    {
        // ------------------------------------------------------------
        // 1. TENTA PELO ENDEREÇO HTTP
        // ------------------------------------------------------------

        try
        {
            var image =
                await ImageLoader.LoadAsync(imageKey);

            if (image is not null)
                return image;
        }
        catch
        {
            // Continua para o fallback local.
        }

        // ------------------------------------------------------------
        // 2. FALLBACK PARA WWWROOT/UPLOADS
        // ------------------------------------------------------------

        var localPath =
            FindLocalUploadImage(originalValue);

        if (string.IsNullOrWhiteSpace(localPath))
            return null;

        try
        {
            return await ImageLoader.LoadAsync(localPath);
        }
        catch
        {
            return null;
        }
    }

    // ================================================================
    // ENCONTRAR IMAGEM LOCAL DO UPLOAD
    // ================================================================

    private static string? FindLocalUploadImage(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var image =
            value.Trim();

        // Se já for um arquivo local existente.
        if (Path.IsPathRooted(image) &&
            File.Exists(image))
        {
            return image;
        }

        // Remove a parte inicial /uploads/
        // para obter somente o nome do arquivo.
        var normalized =
            image.Replace(
                '\\',
                '/');

        const string uploadsPrefix =
            "/uploads/";

        if (!normalized.StartsWith(
                uploadsPrefix,
                StringComparison.OrdinalIgnoreCase) &&
            !normalized.StartsWith(
                "uploads/",
                StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var fileName =
            normalized
                .Substring(
                    normalized.IndexOf(
                        "uploads/",
                        StringComparison.OrdinalIgnoreCase)
                    + "uploads/".Length)
                .TrimStart(
                    '/',
                    '\\');

        if (string.IsNullOrWhiteSpace(fileName))
            return null;

        // ------------------------------------------------------------
        // Procura o projeto UI subindo pelas pastas.
        // ------------------------------------------------------------

        foreach (var root in EnumerateParentDirectories(
                     AppContext.BaseDirectory))
        {
            var uploads =
                Path.Combine(
                    root.FullName,
                    "AtelieDaTransformacao.UI",
                    "wwwroot",
                    "uploads");

            var candidate =
                Path.Combine(
                    uploads,
                    fileName);

            if (File.Exists(candidate))
                return candidate;
        }

        foreach (var root in EnumerateParentDirectories(
                     Environment.CurrentDirectory))
        {
            var uploads =
                Path.Combine(
                    root.FullName,
                    "AtelieDaTransformacao.UI",
                    "wwwroot",
                    "uploads");

            var candidate =
                Path.Combine(
                    uploads,
                    fileName);

            if (File.Exists(candidate))
                return candidate;
        }

        return null;
    }

    // ================================================================
    // PASTAS PAI
    // ================================================================

    private static IEnumerable<DirectoryInfo>
        EnumerateParentDirectories(
            string startPath)
    {
        var directory =
            new DirectoryInfo(startPath);

        while (directory is not null)
        {
            yield return directory;

            directory =
                directory.Parent;
        }
    }

    // ================================================================
    // NORMALIZAR IMAGEM
    // ================================================================

    private static string NormalizeImageKey(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var image =
            value.Trim();

        // ------------------------------------------------------------
        // URL absoluta
        // ------------------------------------------------------------

        if (Uri.TryCreate(
                image,
                UriKind.Absolute,
                out var absolute) &&
            (absolute.Scheme ==
                Uri.UriSchemeHttp ||
             absolute.Scheme ==
                Uri.UriSchemeHttps))
        {
            return absolute.ToString();
        }

        // ------------------------------------------------------------
        // ARQUIVO LOCAL
        // ------------------------------------------------------------

        if (Path.IsPathRooted(image))
            return image;

        // ------------------------------------------------------------
        // CAMINHO RELATIVO
        // ------------------------------------------------------------

        if (Uri.TryCreate(
                AppConfig.ImageBaseUrl,
                UriKind.Absolute,
                out var baseUri))
        {
            return new Uri(
                baseUri,
                image.TrimStart(
                    '/',
                    '\\'))
                .ToString();
        }

        return image;
    }

    // ================================================================
    // PLACEHOLDER
    // ================================================================

    private static Image CreateImagePlaceholder()
    {
        var bitmap =
            new Bitmap(
                190,
                190);

        using var graphics =
            Graphics.FromImage(bitmap);

        graphics.Clear(
            Color.FromArgb(
                65,
                40,
                27));

        graphics.SmoothingMode =
            SmoothingMode.AntiAlias;

        using var pen =
            new Pen(
                Color.FromArgb(
                    145,
                    115,
                    92),
                2F);

        graphics.DrawRectangle(
            pen,
            48,
            58,
            94,
            70);

        graphics.DrawEllipse(
            pen,
            111,
            69,
            13,
            13);

        graphics.DrawLine(
            pen,
            52,
            118,
            77,
            91);

        graphics.DrawLine(
            pen,
            77,
            91,
            101,
            113);

        graphics.DrawLine(
            pen,
            101,
            113,
            119,
            96);

        return bitmap;
    }
}