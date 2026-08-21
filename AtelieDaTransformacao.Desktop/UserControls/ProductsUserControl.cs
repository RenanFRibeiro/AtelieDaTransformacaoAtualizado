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

    public ProductsUserControl()
    {
        InitializeComponent();
        _newButton.Visible = SessionManager.IsAdmin;
        _editButton.Visible = SessionManager.IsAdmin;
        _deleteButton.Visible = SessionManager.IsAdmin;
        _refreshButton.Click += async (_, _) => await LoadAsync();
        _newButton.Click += async (_, _) => await EditAsync(null);
        _editButton.Click += async (_, _) => await EditSelectedAsync();
        _deleteButton.Click += async (_, _) => await DeleteSelectedAsync();
        _searchTextBox.KeyDown += async (_, e) => { if (e.KeyCode == Keys.Enter) await LoadAsync(); };
        _categoryComboBox.SelectedIndexChanged += async (_, _) => { if (!_loading) await LoadAsync(); };
        _grid.CellDoubleClick += async (_, _) => await EditSelectedAsync();
        _grid.CellContentClick += Grid_CellContentClick;
        _grid.CellPainting += Grid_CellPainting;
        Load += async (_, _) => await LoadAsync();
    }

    private async Task LoadAsync()
    {
        if (_loading) return;
        _loading = true;
        try
        {
            _categoryItems = await _categories.GetAllAsync() ?? new();
            var selectedId = _categoryComboBox.SelectedValue is int id ? id : 0;
            var categories = new List<CategoryDto> { new() { Id = 0, Name = "Todas as categorias" } };
            categories.AddRange(_categoryItems);
            _categoryComboBox.DataSource = categories;
            _categoryComboBox.DisplayMember = "Name";
            _categoryComboBox.ValueMember = "Id";
            if (categories.Any(x => x.Id == selectedId)) _categoryComboBox.SelectedValue = selectedId;

            _items = await _service.GetAllAsync(_searchTextBox.Text.Trim(), selectedId > 0 ? selectedId : null) ?? new();
            _grid.DataSource = _items.Select(x => new
            {
                x.Id,
                Produto = x.Title,
                Categoria = x.CategoryName,
                Preço = x.Price.ToString("C2"),
                Estoque = x.StockQuantity,
                Status = x.StockQuantity == 0 ? "Sem estoque" : x.StockQuantity <= 5 ? "Baixo" : "Disponível",
                Destaque = x.IsFeatured ? "Sim" : "Não",
                Acoes = "Visualizar"
            }).ToList();
            if (_grid.Columns["Id"] is not null) _grid.Columns["Id"].Visible = false;
            _countLabel.Text = $"{_items.Count} produto(s) encontrado(s)";
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Erro ao carregar produtos", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        finally { _loading = false; }
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
        if (_grid.Columns[e.ColumnIndex].Name != "Acoes") return;

        var item = SelectedAt(e.RowIndex);
        if (item is null) return;

        using var dialog = new ProductDetailsDialog(item);
        dialog.ShowDialog(FindForm());
    }

    private void Grid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Status") return;

        e.Handled = true;
        e.PaintBackground(e.CellBounds, true);
        var text = e.FormattedValue?.ToString() ?? string.Empty;
        var bounds = new Rectangle(e.CellBounds.X + 7, e.CellBounds.Y + 6,
            Math.Max(20, e.CellBounds.Width - 14), Math.Max(18, e.CellBounds.Height - 12));

        using var path = RoundedRect(bounds, 7);
        using var brush = new SolidBrush(ProductStatusColor(text));
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.FillPath(brush, path);
        using var font = new Font("Segoe UI Semibold", 7.5F);
        using var textBrush = new SolidBrush(Color.White);
        var size = e.Graphics.MeasureString(text, font);
        e.Graphics.DrawString(text, font, textBrush,
            bounds.X + (bounds.Width - size.Width) / 2,
            bounds.Y + (bounds.Height - size.Height) / 2 + 1);
        e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
    }

    private ProductDto? SelectedAt(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _grid.Rows.Count) return null;
        if (_grid.Rows[rowIndex].Cells["Id"]?.Value is null) return null;
        var id = Convert.ToInt32(_grid.Rows[rowIndex].Cells["Id"].Value);
        return _items.FirstOrDefault(x => x.Id == id);
    }

    private static Color ProductStatusColor(string text) => text switch
    {
        "Sem estoque" => Color.FromArgb(192, 0, 0),
        "Baixo" => Color.FromArgb(205, 145, 24),
        "Disponível" => Color.FromArgb(35, 164, 64),
        _ => Color.FromArgb(120, 120, 120)
    };

    private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle rect, int radius)
    {
        var path = new System.Drawing.Drawing2D.GraphicsPath();
        var d = radius * 2;
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    private ProductDto? Selected()
    {
        if (_grid.CurrentRow is null || _grid.CurrentRow.Cells["Id"]?.Value is null) return null;
        var id = Convert.ToInt32(_grid.CurrentRow.Cells["Id"].Value);
        return _items.FirstOrDefault(x => x.Id == id);
    }

    private async Task EditSelectedAsync()
    {
        var item = Selected();
        if (item is not null) await EditAsync(item);
    }

    private async Task EditAsync(ProductDto? item)
    {
        if (!SessionManager.IsAdmin)
        {
            MessageBox.Show(this, "Somente administradores podem alterar produtos.", "Permissão", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        using var dialog = new ProductDialog(item, _categoryItems);
        if (dialog.ShowDialog(FindForm()) != DialogResult.OK || dialog.Result is null) return;
        try
        {
            if (item is null) await _service.CreateAsync(dialog.Result);
            else await _service.UpdateAsync(item.Id, dialog.Result);
            await LoadAsync();
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Não foi possível salvar", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private async Task DeleteSelectedAsync()
    {
        var item = Selected();
        if (item is null) return;
        if (MessageBox.Show(this, $"Excluir o produto \"{item.Title}\"?", "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        try { await _service.DeleteAsync(item.Id); await LoadAsync(); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
