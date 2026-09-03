using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Windows.Forms;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Forms;
using AtelieDaTransformacao.Desktop.Services;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class ProductsUserControl : UserControl
{
    private readonly ProductsApiService _service = new();
    private readonly CategoriesApiService _categories = new();

    private List<ProductDto> _items = new();
    private List<CategoryDto> _categoryItems = new();

    private bool _loading;

    // Cacheia a imagem por URL/caminho para que o CellPainting nunca faça
    // downloads repetidos durante os vários repaints do DataGridView.
    private readonly ConcurrentDictionary<string, Task<Image?>> _imageCache = new(StringComparer.OrdinalIgnoreCase);
    private static readonly HttpClient _imageClient = new()
    {
        Timeout = TimeSpan.FromSeconds(15)
    };
    private static readonly Image _imagePlaceholder = CreateImagePlaceholder();

    public ProductsUserControl()
    {
        InitializeComponent();

        // ============================================================
        // CONFIGURAÇÃO VISUAL DA TABELA
        // ============================================================

        _grid.EnableHeadersVisualStyles = false;

        // ------------------------------------------------------------
        // CABEÇALHO
        // ------------------------------------------------------------

        _grid.ColumnHeadersDefaultCellStyle.BackColor =
            Color.FromArgb(82, 52, 36);

        _grid.ColumnHeadersDefaultCellStyle.ForeColor =
            Color.White;

        _grid.ColumnHeadersDefaultCellStyle.Font =
            new Font("Segoe UI Semibold", 9F);

        _grid.ColumnHeadersDefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleLeft;

        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor =
            Color.FromArgb(82, 52, 36);

        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor =
            Color.White;

        // ------------------------------------------------------------
        // LINHAS NORMAIS
        // ------------------------------------------------------------

        _grid.DefaultCellStyle.BackColor =
            Color.White;

        _grid.DefaultCellStyle.ForeColor =
            Color.FromArgb(35, 35, 35);

        _grid.DefaultCellStyle.Font =
            new Font("Segoe UI", 9F);

        // ------------------------------------------------------------
        // LINHA SELECIONADA
        // ------------------------------------------------------------

        _grid.DefaultCellStyle.SelectionBackColor =
            Color.FromArgb(220, 220, 220);

        _grid.DefaultCellStyle.SelectionForeColor =
            Color.FromArgb(25, 25, 25);

        // ------------------------------------------------------------
        // LINHAS ALTERNADAS
        // ------------------------------------------------------------

        // Todas as linhas possuem o mesmo fundo.
        // Isso evita o efeito "uma sim e outra não".
        _grid.AlternatingRowsDefaultCellStyle.BackColor =
            Color.White;

        _grid.AlternatingRowsDefaultCellStyle.ForeColor =
            Color.FromArgb(35, 35, 35);

        _grid.AlternatingRowsDefaultCellStyle.SelectionBackColor =
            Color.FromArgb(220, 220, 220);

        _grid.AlternatingRowsDefaultCellStyle.SelectionForeColor =
            Color.FromArgb(25, 25, 25);

        // ------------------------------------------------------------
        // REMOVER LINHAS DIVISÓRIAS
        // ------------------------------------------------------------

        _grid.GridColor =
            Color.White;

        _grid.CellBorderStyle =
            DataGridViewCellBorderStyle.None;

        _grid.BorderStyle =
            BorderStyle.None;

        _grid.RowHeadersVisible =
            false;

        // O mouse sobre a tabela não deve alterar a aparência das linhas.
        // A seleção visual acontece somente quando a linha é realmente selecionada/clicada.
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.MultiSelect = false;

        _grid.ColumnHeadersHeight =
            42;

        _grid.RowTemplate.Height =
            72;

        // ------------------------------------------------------------
        // GUNA2
        // ------------------------------------------------------------

        try
        {
            _grid.ThemeStyle.GridColor =
                Color.White;

            _grid.ThemeStyle.RowsStyle.BackColor =
                Color.White;

            _grid.ThemeStyle.RowsStyle.ForeColor =
                Color.FromArgb(35, 35, 35);

            _grid.ThemeStyle.RowsStyle.SelectionBackColor =
                Color.FromArgb(220, 220, 220);

            _grid.ThemeStyle.RowsStyle.SelectionForeColor =
                Color.FromArgb(25, 25, 25);

            _grid.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(82, 52, 36);

            _grid.ThemeStyle.HeaderStyle.ForeColor =
                Color.White;

            _grid.ThemeStyle.HeaderStyle.Font =
                new Font("Segoe UI Semibold", 9F);

            _grid.ThemeStyle.HeaderStyle.Height =
                42;
        }
        catch
        {
            // Compatibilidade com diferentes versões do Guna.
        }

        // ============================================================
        // BOTÕES
        // ============================================================

        // Qualquer usuário autenticado pode criar, editar e excluir produtos.
        _newButton.Visible =
            !string.IsNullOrWhiteSpace(SessionManager.Token);

        _editButton.Visible =
            !string.IsNullOrWhiteSpace(SessionManager.Token);

        _deleteButton.Visible =
            !string.IsNullOrWhiteSpace(SessionManager.Token);

        // ============================================================
        // EVENTOS
        // ============================================================

        _refreshButton.Click += async (_, _) =>
            await LoadAsync();

        _newButton.Click += async (_, _) =>
            await EditAsync(null);

        _editButton.Click += async (_, _) =>
            await EditSelectedAsync();

        _deleteButton.Click += async (_, _) =>
            await DeleteSelectedAsync();

        _searchTextBox.KeyDown += async (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
                await LoadAsync();
        };

        _categoryComboBox.SelectedIndexChanged += async (_, _) =>
        {
            if (!_loading)
                await LoadAsync();
        };

        _grid.CellDoubleClick += async (_, _) =>
            await EditSelectedAsync();

        _grid.CellContentClick +=
            Grid_CellContentClick;

        _grid.CellPainting +=
            Grid_CellPainting;

        // Atualiza a aparência do botão Visualizar
        // quando a linha selecionada muda.
        _grid.SelectionChanged +=
            Grid_SelectionChanged;

        Load += async (_, _) =>
            await LoadAsync();
    }

    // ================================================================
    // SELEÇÃO ALTERADA
    // ================================================================

    private void Grid_SelectionChanged(
        object? sender,
        EventArgs e)
    {
        if (_grid.Rows.Count == 0)
            return;

        // Redesenha somente a tabela.
        _grid.Invalidate();
    }

    // ================================================================
    // CARREGAR PRODUTOS
    // ================================================================

    private async Task LoadAsync()
    {
        if (_loading)
            return;

        _loading = true;

        try
        {
            // --------------------------------------------------------
            // CATEGORIAS
            // --------------------------------------------------------

            _categoryItems =
                await _categories.GetAllAsync()
                ?? new();

            var selectedId =
                _categoryComboBox.SelectedValue is int id
                    ? id
                    : 0;

            var categories =
                new List<CategoryDto>
                {
                    new()
                    {
                        Id = 0,
                        Name = "Todas as categorias"
                    }
                };

            categories.AddRange(
                _categoryItems);

            _categoryComboBox.DataSource =
                categories;

            _categoryComboBox.DisplayMember =
                "Name";

            _categoryComboBox.ValueMember =
                "Id";

            if (categories.Any(
                    x => x.Id == selectedId))
            {
                _categoryComboBox.SelectedValue =
                    selectedId;
            }

            // --------------------------------------------------------
            // PRODUTOS
            // --------------------------------------------------------

            _items =
                await _service.GetAllAsync(
                    _searchTextBox.Text.Trim(),
                    selectedId > 0
                        ? selectedId
                        : null)
                ?? new();

            _grid.DataSource =
                _items.Select(x => new
                {
                    x.Id,

                    Imagem =
                        x.Image,

                    Produto =
                        x.Title,

                    Categoria =
                        x.CategoryName,

                    Preço =
                        x.Price.ToString("C2"),

                    Estoque =
                        x.StockQuantity,

                    Status =
                        x.StockQuantity == 0
                            ? "Sem estoque"
                            : x.StockQuantity <= 5
                                ? "Baixo"
                                : "Disponível",

                    Destaque =
                        x.IsFeatured
                            ? "Sim"
                            : "Não",

                    Acoes =
                        "Visualizar"

                }).ToList();

            // --------------------------------------------------------
            // OCULTAR ID
            // --------------------------------------------------------

            if (_grid.Columns["Id"] is not null)
            {
                _grid.Columns["Id"].Visible =
                    false;
            }

            _countLabel.Text =
                $"{_items.Count} produto(s) encontrado(s)";

            // Começa o carregamento das imagens em segundo plano. O grid pode
            // continuar responsivo e o CellPainting usa o cache quando a imagem chegar.
            _ = PreloadImagesAsync(_items);

            // --------------------------------------------------------
            // GARANTIR PADRÃO DAS LINHAS
            // --------------------------------------------------------

            foreach (DataGridViewRow row in _grid.Rows)
            {
                row.DefaultCellStyle.BackColor =
                    Color.White;

                row.DefaultCellStyle.ForeColor =
                    Color.FromArgb(35, 35, 35);

                row.DefaultCellStyle.SelectionBackColor =
                    Color.FromArgb(220, 220, 220);

                row.DefaultCellStyle.SelectionForeColor =
                    Color.FromArgb(25, 25, 25);
            }

            // --------------------------------------------------------
            // CABEÇALHO
            // --------------------------------------------------------

            _grid.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(82, 52, 36);

            _grid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            _grid.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI Semibold",
                    9F);

            // Redesenha a tabela
            _grid.Invalidate();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                "Erro ao carregar produtos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    // ================================================================
    // CLIQUE EM VISUALIZAR
    // ================================================================

    private void Grid_CellContentClick(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 ||
            e.ColumnIndex < 0)
            return;

        if (_grid.Columns[e.ColumnIndex].Name !=
            "Acoes")
            return;

        var item =
            SelectedAt(e.RowIndex);

        if (item is null)
            return;

        using var dialog =
            new ProductDetailsDialog(item);

        dialog.ShowDialog(
            FindForm());
    }

    // ================================================================
    // DESENHO DAS CÉLULAS
    // ================================================================

    private void Grid_CellPainting(
        object? sender,
        DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 ||
            e.ColumnIndex < 0)
            return;

        var columnName =
            _grid.Columns[e.ColumnIndex].Name;

        // ============================================================
        // IMAGEM
        // ============================================================

        if (columnName == "Imagem")
        {
            e.Handled = true;

            bool selected = _grid.Rows[e.RowIndex].Selected;
            var backgroundColor = selected
                ? Color.FromArgb(220, 220, 220)
                : Color.White;

            using var backgroundBrush = new SolidBrush(backgroundColor);
            e.Graphics.FillRectangle(backgroundBrush, e.CellBounds);

            var product = SelectedAt(e.RowIndex);
            var imageKey = NormalizeImageKey(product?.Image);
            Image? image = null;

            if (!string.IsNullOrWhiteSpace(imageKey) &&
                _imageCache.TryGetValue(imageKey, out var imageTask) &&
                imageTask.IsCompletedSuccessfully)
            {
                image = imageTask.Result;
            }

            image ??= _imagePlaceholder;

            var target = FitRectangle(
                image.Size,
                new Rectangle(
                    e.CellBounds.X + 8,
                    e.CellBounds.Y + 5,
                    Math.Max(1, e.CellBounds.Width - 16),
                    Math.Max(1, e.CellBounds.Height - 10)));

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.DrawImage(image, target);

            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            return;
        }

        // ============================================================
        // VISUALIZAR
        // ============================================================

        if (columnName == "Acoes")
        {
            e.Handled = true;

            bool selected =
                _grid.Rows[e.RowIndex].Selected;

            // Fundo da célula
            using var backgroundBrush =
                new SolidBrush(
                    selected
                        ? Color.FromArgb(220, 220, 220)
                        : Color.White);

            e.Graphics.FillRectangle(
                backgroundBrush,
                e.CellBounds);

            // --------------------------------------------------------
            // BOTÃO
            // --------------------------------------------------------

            const int buttonWidth = 72;
            const int buttonHeight = 24;

            var buttonBounds =
                new Rectangle(
                    e.CellBounds.X +
                        (e.CellBounds.Width - buttonWidth) / 2,
                    e.CellBounds.Y +
                        (e.CellBounds.Height - buttonHeight) / 2,
                    buttonWidth,
                    buttonHeight);

            using var path =
                RoundedRect(
                    buttonBounds,
                    7);

            // Selecionado = marrom
            // Normal = cinza claro
            Color buttonColor =
                selected
                    ? Color.FromArgb(82, 52, 36)
                    : Color.FromArgb(241, 243, 248);

            using var buttonBrush =
                new SolidBrush(
                    buttonColor);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            e.Graphics.FillPath(
                buttonBrush,
                path);

            // --------------------------------------------------------
            // TEXTO
            // --------------------------------------------------------

            const string text =
                "Visualizar";

            using var font =
                new Font(
                    "Segoe UI Semibold",
                    7.5F);

            using var textBrush =
                new SolidBrush(
                    selected
                        ? Color.White
                        : Color.FromArgb(
                            50,
                            50,
                            50));

            var size =
                e.Graphics.MeasureString(
                    text,
                    font);

            e.Graphics.DrawString(
                text,
                font,
                textBrush,
                buttonBounds.X +
                    (buttonBounds.Width -
                        size.Width) / 2,
                buttonBounds.Y +
                    (buttonBounds.Height -
                        size.Height) / 2);

            // Mantém as divisões da tabela sempre visíveis, inclusive ao passar o mouse.
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            return;
        }

        // ============================================================
        // STATUS
        // ============================================================

        if (columnName == "Status")
        {
            e.Handled = true;

            bool selected =
                _grid.Rows[e.RowIndex].Selected;

            // Fundo da célula
            using var backgroundBrush =
                new SolidBrush(
                    selected
                        ? Color.FromArgb(
                            220,
                            220,
                            220)
                        : Color.White);

            e.Graphics.FillRectangle(
                backgroundBrush,
                e.CellBounds);

            var text =
                e.FormattedValue?.ToString()
                ?? string.Empty;

            using var statusFont =
                new Font(
                    "Segoe UI Semibold",
                    7.5F);

            var textSize =
                e.Graphics.MeasureString(
                    text,
                    statusFont);

            const int horizontalPadding = 14;
            const int badgeHeight = 20;

            var badgeWidth =
                Math.Min(
                    Math.Max(
                        42,
                        (int)Math.Ceiling(textSize.Width) +
                            horizontalPadding),
                    Math.Max(42, e.CellBounds.Width - 10));

            var bounds =
                new Rectangle(
                    e.CellBounds.X +
                        (e.CellBounds.Width - badgeWidth) / 2,
                    e.CellBounds.Y +
                        (e.CellBounds.Height - badgeHeight) / 2,
                    badgeWidth,
                    badgeHeight);

            using var path =
                RoundedRect(
                    bounds,
                    7);

            using var brush =
                new SolidBrush(
                    ProductStatusColor(
                        text));

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            e.Graphics.FillPath(
                brush,
                path);

            using var textBrush =
                new SolidBrush(
                    Color.White);

            var size =
                e.Graphics.MeasureString(
                    text,
                    statusFont);

            e.Graphics.DrawString(
                text,
                statusFont,
                textBrush,
                bounds.X +
                    (bounds.Width -
                        size.Width) / 2,
                bounds.Y +
                    (bounds.Height -
                        size.Height) / 2 +
                    1);

            // Mantém as divisões da tabela sempre visíveis, inclusive ao passar o mouse.
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            return;
        }

        // ============================================================
        // DEMAIS CÉLULAS
        // ============================================================

        e.Handled = true;

        bool rowSelected =
            _grid.Rows[e.RowIndex].Selected;

        Color cellBackgroundColor =
            rowSelected
                ? Color.FromArgb(
                    220,
                    220,
                    220)
                : Color.White;

        using var cellBrush =
            new SolidBrush(
                cellBackgroundColor);

        e.Graphics.FillRectangle(
            cellBrush,
            e.CellBounds);

        var value =
            e.FormattedValue?.ToString()
            ?? string.Empty;

        using var normalFont =
            new Font(
                "Segoe UI",
                9F);

        using var normalTextBrush =
            new SolidBrush(
                Color.FromArgb(
                    35,
                    35,
                    35));

        var textBounds =
            new Rectangle(
                e.CellBounds.X + 6,
                e.CellBounds.Y,
                e.CellBounds.Width - 12,
                e.CellBounds.Height);

        using var stringFormat =
            new StringFormat
            {
                Alignment =
                    StringAlignment.Near,

                LineAlignment =
                    StringAlignment.Center
            };

        e.Graphics.DrawString(
            value,
            normalFont,
            normalTextBrush,
            textBounds,
            stringFormat);

        // A pintura personalizada não deve apagar as linhas da grade.
        // Desenhamos somente a borda depois do conteúdo para que ela permaneça fixa.
        e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
    }

    // ================================================================
    // CACHE E CARREGAMENTO DAS IMAGENS
    // ================================================================

    private async Task PreloadImagesAsync(IEnumerable<ProductDto> products)
    {
        var keys = products
            .Select(x => NormalizeImageKey(x.Image))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (keys.Count == 0)
            return;

        await Task.WhenAll(keys.Select(GetImageAsync));

        if (!IsDisposed && IsHandleCreated)
            BeginInvoke(new Action(() =>
            {
                if (_grid.Columns["Imagem"] is { } imageColumn)
                    _grid.InvalidateColumn(imageColumn.Index);
            }));
    }

    private Task<Image?> GetImageAsync(string key)
    {
        var task = _imageCache.GetOrAdd(
            key,
            static imageKey => LoadImageAsync(imageKey));

        // Não deixa uma falha temporária ficar armazenada no cache para sempre.
        _ = task.ContinueWith(
            completedTask =>
            {
                if (completedTask.Status == TaskStatus.RanToCompletion &&
                    completedTask.Result is null)
                {
                    _imageCache.TryRemove(
                        new KeyValuePair<string, Task<Image?>>(key, task));
                }
            },
            CancellationToken.None,
            TaskContinuationOptions.ExecuteSynchronously,
            TaskScheduler.Default);

        return task;
    }

    private static async Task<Image?> LoadImageAsync(string key)
    {
        try
        {
            return await ImageLoader.LoadAsync(key);
        }
        catch
        {
            return null;
        }
    }

    private static string NormalizeImageKey(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var image = value.Trim();

        if (Uri.TryCreate(image, UriKind.Absolute, out var absolute) &&
            (absolute.Scheme == Uri.UriSchemeHttp || absolute.Scheme == Uri.UriSchemeHttps))
            return absolute.ToString();

        if (Path.IsPathRooted(image) && File.Exists(image))
            return image;

        if (Uri.TryCreate(AppConfig.ImageBaseUrl, UriKind.Absolute, out var baseUri))
            return new Uri(baseUri, image.TrimStart('/', '\\')).ToString();

        return image;
    }

    private static Rectangle FitRectangle(Size imageSize, Rectangle bounds)
    {
        if (imageSize.Width <= 0 || imageSize.Height <= 0)
            return bounds;

        var scale = Math.Min(
            (double)bounds.Width / imageSize.Width,
            (double)bounds.Height / imageSize.Height);

        var width = Math.Max(1, (int)Math.Round(imageSize.Width * scale));
        var height = Math.Max(1, (int)Math.Round(imageSize.Height * scale));

        return new Rectangle(
            bounds.X + (bounds.Width - width) / 2,
            bounds.Y + (bounds.Height - height) / 2,
            width,
            height);
    }

    private static Image CreateImagePlaceholder()
    {
        var bitmap = new Bitmap(64, 64);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.FromArgb(245, 245, 245));
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using var borderPen = new Pen(Color.FromArgb(205, 205, 205), 1.5F);
        graphics.DrawRectangle(borderPen, new Rectangle(5, 5, 54, 54));

        using var iconPen = new Pen(Color.FromArgb(150, 150, 150), 2.2F);
        graphics.DrawRectangle(iconPen, 17, 19, 30, 22);
        graphics.DrawEllipse(iconPen, 35, 23, 6, 6);
        graphics.DrawLine(iconPen, 19, 38, 27, 31);
        graphics.DrawLine(iconPen, 27, 31, 34, 37);
        graphics.DrawLine(iconPen, 34, 37, 40, 32);

        return bitmap;
    }

    // ================================================================
    // PRODUTO PELO ÍNDICE
    // ================================================================

    private ProductDto? SelectedAt(
        int rowIndex)
    {
        if (rowIndex < 0 ||
            rowIndex >= _grid.Rows.Count)
            return null;

        if (_grid.Rows[rowIndex]
            .Cells["Id"]?.Value is null)
            return null;

        var id =
            Convert.ToInt32(
                _grid.Rows[rowIndex]
                    .Cells["Id"]
                    .Value);

        return _items.FirstOrDefault(
            x => x.Id == id);
    }

    // ================================================================
    // CORES DOS STATUS
    // ================================================================

    private static Color ProductStatusColor(
        string text)
    {
        return text switch
        {
            "Sem estoque" =>
                Color.FromArgb(
                    192,
                    0,
                    0),

            "Baixo" =>
                Color.FromArgb(
                    205,
                    145,
                    24),

            "Disponível" =>
                Color.FromArgb(
                    35,
                    164,
                    64),

            _ =>
                Color.FromArgb(
                    120,
                    120,
                    120)
        };
    }

    // ================================================================
    // RETÂNGULO ARREDONDADO
    // ================================================================

    private static GraphicsPath RoundedRect(
        Rectangle rect,
        int radius)
    {
        var path =
            new GraphicsPath();

        var d =
            radius * 2;

        path.AddArc(
            rect.X,
            rect.Y,
            d,
            d,
            180,
            90);

        path.AddArc(
            rect.Right - d,
            rect.Y,
            d,
            d,
            270,
            90);

        path.AddArc(
            rect.Right - d,
            rect.Bottom - d,
            d,
            d,
            0,
            90);

        path.AddArc(
            rect.X,
            rect.Bottom - d,
            d,
            d,
            90,
            90);

        path.CloseFigure();

        return path;
    }

    // ================================================================
    // PRODUTO SELECIONADO
    // ================================================================

    private ProductDto? Selected()
    {
        if (_grid.CurrentRow is null ||
            _grid.CurrentRow
                .Cells["Id"]?.Value is null)
            return null;

        var id =
            Convert.ToInt32(
                _grid.CurrentRow
                    .Cells["Id"]
                    .Value);

        return _items.FirstOrDefault(
            x => x.Id == id);
    }

    // ================================================================
    // EDITAR SELECIONADO
    // ================================================================

    private async Task EditSelectedAsync()
    {
        var item =
            Selected();

        if (item is not null)
            await EditAsync(item);
    }

    // ================================================================
    // CRIAR / EDITAR
    // ================================================================

    private async Task EditAsync(
    ProductDto? item)
    {
        // Criar e editar produto: qualquer usuário autenticado.
        var podeSalvar = !string.IsNullOrWhiteSpace(SessionManager.Token);

        if (!podeSalvar)
        {
            MessageBox.Show(
                this,
                "Você não tem permissão para realizar esta operação.",
                "Permissão",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        using var dialog =
            new ProductDialog(
                item,
                _categoryItems);

        if (dialog.ShowDialog(
                FindForm()) != DialogResult.OK ||
            dialog.Result is null)
            return;

        try
        {
            if (item is null)
            {
                await _service.CreateAsync(
                    dialog.Result);
            }
            else
            {
                await _service.UpdateAsync(
                    item.Id,
                    dialog.Result);
            }

            await LoadAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                "Não foi possível salvar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // ================================================================
    // EXCLUIR
    // ================================================================

    private async Task DeleteSelectedAsync()
    {
        var item =
            Selected();

        if (item is null)
            return;

        if (MessageBox.Show(
                this,
                $"Excluir o produto \"{item.Title}\"?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
            != DialogResult.Yes)
            return;

        try
        {
            await _service.DeleteAsync(
                item.Id);

            await LoadAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                ex.Message,
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
