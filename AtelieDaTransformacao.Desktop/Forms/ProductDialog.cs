using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

public partial class ProductDialog : Form
{
    public ProductWriteDto? Result { get; private set; }

    private readonly ProductDto? _item;
    private readonly IEnumerable<CategoryDto> _categories;
    private string _selectedImageFile = string.Empty;
    private string _imageValue = string.Empty;
    private Bitmap? _previewImage;

    public ProductDialog(ProductDto? item, IEnumerable<CategoryDto> categories)
    {
        _item = item;
        _categories = categories;
        InitializeComponent();
        Load += async (_, _) => await LoadValuesAsync();
        _saveButton.Click += (_, _) => Save();
        _cancelButton.Click += (_, _) => DialogResult = DialogResult.Cancel;
        _chooseImageButton.Click += ChooseImageButton_Click;
        _clearImageButton.Click += ClearImageButton_Click;
        _imageUrlTextBox.Leave += async (_, _) => await LoadPreviewFromUrlAsync();
    }

    private async Task LoadValuesAsync()
    {
        _categoryComboBox.DataSource = _categories.ToList();
        _categoryComboBox.DisplayMember = "Name";
        _categoryComboBox.ValueMember = "Id";

        _titleTextBox.Text = _item?.Title ?? string.Empty;
        _descriptionTextBox.Text = _item?.Description ?? string.Empty;
        _priceNumeric.Value = _item is null ? 0.01M : Math.Max(_priceNumeric.Minimum, Math.Min(_priceNumeric.Maximum, _item.Price));
        _stockNumeric.Value = _item?.StockQuantity ?? 0;
        _featuredCheckBox.Checked = _item?.IsFeatured ?? false;

        if (_item is not null)
            _categoryComboBox.SelectedValue = _item.CategoryId;

        _imageValue = _item?.Image?.Trim() ?? string.Empty;
        _imageUrlTextBox.Text = _imageValue;

        if (!string.IsNullOrWhiteSpace(_imageValue))
            await SetPreviewAsync(_imageValue);
    }

    private async void ChooseImageButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Selecionar imagem do produto",
            Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp;*.bmp;*.gif|Todos os arquivos|*.*",
            Multiselect = false,
            CheckFileExists = true
        };

        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        _selectedImageFile = dialog.FileName;
        _imageUrlTextBox.Text = string.Empty;
        _imageFileLabel.Text = Path.GetFileName(_selectedImageFile);
        await SetPreviewAsync(_selectedImageFile);
    }

    private async void ClearImageButton_Click(object? sender, EventArgs e)
    {
        _selectedImageFile = string.Empty;
        _imageValue = string.Empty;
        _imageUrlTextBox.Text = string.Empty;
        _imageFileLabel.Text = "Nenhuma imagem selecionada";
        SetPreview(null);
        await Task.CompletedTask;
    }

    private async Task LoadPreviewFromUrlAsync()
    {
        if (!IsHandleCreated || string.IsNullOrWhiteSpace(_imageUrlTextBox.Text))
        {
            if (string.IsNullOrWhiteSpace(_imageUrlTextBox.Text) && string.IsNullOrWhiteSpace(_selectedImageFile))
                SetPreview(null);
            return;
        }

        // Se o usuário acabou de selecionar um arquivo, o upload tem prioridade.
        if (!string.IsNullOrWhiteSpace(_selectedImageFile))
            return;

        var value = _imageUrlTextBox.Text.Trim();
        if (value.Length < 8) return;

        await SetPreviewAsync(value);
    }

    private async Task SetPreviewAsync(string? source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            SetPreview(null);
            return;
        }

        var image = await ImageLoader.LoadAsync(NormalizeImageSource(source));
        if (IsDisposed)
        {
            image?.Dispose();
            return;
        }

        SetPreview(image);
    }

    private void SetPreview(Image? image)
    {
        var old = _previewImage;
        _previewImage = image is null ? null : new Bitmap(image);
        _imagePreviewPictureBox.Image = _previewImage;
        old?.Dispose();
        image?.Dispose();
    }

    private static string NormalizeImageSource(string source)
    {
        if (Uri.TryCreate(source, UriKind.Absolute, out var absolute) &&
            (absolute.Scheme == Uri.UriSchemeHttp || absolute.Scheme == Uri.UriSchemeHttps))
            return absolute.ToString();

        if (Path.IsPathRooted(source))
            return source;

        if (Uri.TryCreate(AppConfig.ImageBaseUrl, UriKind.Absolute, out var baseUri))
            return new Uri(baseUri, source.TrimStart('/', '\\')).ToString();

        return source;
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(_titleTextBox.Text))
        {
            MessageBox.Show(this, "O nome é obrigatório.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_priceNumeric.Value <= 0)
        {
            MessageBox.Show(this, "O preço deve ser maior que zero.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_categoryComboBox.SelectedValue is null || Convert.ToInt32(_categoryComboBox.SelectedValue) <= 0)
        {
            MessageBox.Show(this, "Selecione uma categoria.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var image = _imageValue;

            // Upload local: copia para o mesmo wwwroot/uploads usado pelo site.
            if (!string.IsNullOrWhiteSpace(_selectedImageFile))
            {
                image = CopyImageToUiUploads(_selectedImageFile);
            }
            else if (!string.IsNullOrWhiteSpace(_imageUrlTextBox.Text))
            {
                image = _imageUrlTextBox.Text.Trim();
            }
            else if (_item is not null)
            {
                // Ao editar sem escolher outra imagem, preserva a existente.
                image = _item.Image ?? string.Empty;
            }

            Result = new ProductWriteDto
            {
                Title = _titleTextBox.Text.Trim(),
                Description = _descriptionTextBox.Text.Trim(),
                Price = _priceNumeric.Value,
                StockQuantity = (int)_stockNumeric.Value,
                CategoryId = Convert.ToInt32(_categoryComboBox.SelectedValue),
                IsFeatured = _featuredCheckBox.Checked,
                Image = image
            };

            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Não foi possível salvar a imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string CopyImageToUiUploads(string sourceFile)
    {
        if (!File.Exists(sourceFile))
            throw new FileNotFoundException("A imagem selecionada não foi encontrada.", sourceFile);

        var uploadsFolder = FindUiUploadsFolder();
        Directory.CreateDirectory(uploadsFolder);

        var extension = Path.GetExtension(sourceFile);
        if (string.IsNullOrWhiteSpace(extension))
            extension = ".jpg";

        var fileName = $"{Guid.NewGuid():N}_{Path.GetFileNameWithoutExtension(sourceFile)}{extension}";
        var destination = Path.Combine(uploadsFolder, fileName);

        File.Copy(sourceFile, destination, false);
        return "/uploads/" + fileName;
    }

    private static string FindUiUploadsFolder()
    {
        foreach (var root in EnumerateParents(AppContext.BaseDirectory)
                     .Concat(EnumerateParents(Environment.CurrentDirectory)))
        {
            var uiWwwRoot = Path.Combine(root.FullName, "AtelieDaTransformacao.UI", "wwwroot");

            if (!Directory.Exists(uiWwwRoot))
                continue;

            var uploads = Path.Combine(uiWwwRoot, "uploads");
            Directory.CreateDirectory(uploads);
            return uploads;
        }

        throw new DirectoryNotFoundException(
            "Não encontrei a pasta AtelieDaTransformacao.UI\\wwwroot\\uploads. " +
            "Abra o projeto completo da solução para usar o upload pelo Desktop.");
    }

    private static IEnumerable<DirectoryInfo> EnumerateParents(string startPath)
    {
        var directory = new DirectoryInfo(startPath);

        while (directory is not null)
        {
            yield return directory;
            directory = directory.Parent;
        }
    }

}
