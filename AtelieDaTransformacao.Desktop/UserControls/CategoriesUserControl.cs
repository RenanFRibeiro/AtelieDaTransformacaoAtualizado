using System.Drawing;
using System.Windows.Forms;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Forms;
using AtelieDaTransformacao.Desktop.Services;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class CategoriesUserControl : UserControl
{
    private readonly CategoriesApiService _service = new();
    private List<CategoryDto> _items = new();

    public CategoriesUserControl()
    {
        InitializeComponent();

        // configuração de cor de seleção do DataGridView (melhora visibilidade)
        _grid.EnableHeadersVisualStyles = false;

        // Seleção clara (dourado/âmbar) com texto escuro — combina com tema marrom
        _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(217, 168, 91); // #D9A851
        _grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 20, 20);   // texto escuro

        // Garantir também para o tema do Guna2 (quando aplicável)
        try
        {
            _grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(217, 168, 91);
            _grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(20, 20, 20);
        }
        catch
        {
            // ignora se propriedade não existir em runtime
        }

        // Cores das linhas para coerência com o resto do app
        _grid.RowsDefaultCellStyle.BackColor = Color.FromArgb(43, 26, 18);
        _grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(65, 40, 27);
        _grid.DefaultCellStyle.BackColor = Color.FromArgb(43, 26, 18);
        _grid.DefaultCellStyle.ForeColor = Color.FromArgb(239, 230, 220);

        _newButton.Visible = SessionManager.IsAdmin;
        _editButton.Visible = SessionManager.IsAdmin;
        _deleteButton.Visible = SessionManager.IsAdmin;
        _refreshButton.Click += async (_, _) => await LoadAsync();
        _newButton.Click += async (_, _) => await EditAsync(null);
        _editButton.Click += async (_, _) => await EditSelectedAsync();
        _deleteButton.Click += async (_, _) => await DeleteSelectedAsync();
        _grid.CellDoubleClick += async (_, _) => await EditSelectedAsync();
        Load += async (_, _) => await LoadAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            _items = await _service.GetAllAsync() ?? new();
            _grid.DataSource = _items.Select(x => new { x.Id, Categoria = x.Name, Descrição = x.Description }).ToList();
            if (_grid.Columns["Id"] is not null) _grid.Columns["Id"].Visible = false;
            _countLabel.Text = $"{_items.Count} categoria(s) cadastrada(s)";
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private CategoryDto? Selected()
    {
        if (_grid.CurrentRow is null || _grid.CurrentRow.Cells["Id"]?.Value is null) return null;
        return _items.FirstOrDefault(x => x.Id == Convert.ToInt32(_grid.CurrentRow.Cells["Id"].Value));
    }

    private async Task EditSelectedAsync() { var item = Selected(); if (item is not null) await EditAsync(item); }

    private async Task EditAsync(CategoryDto? item)
    {
        if (!SessionManager.IsAdmin) return;
        using var dialog = new CategoryDialog(item);
        if (dialog.ShowDialog(FindForm()) != DialogResult.OK || dialog.Result is null) return;
        try { if (item is null) await _service.CreateAsync(dialog.Result); else await _service.UpdateAsync(item.Id, dialog.Result); await LoadAsync(); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private async Task DeleteSelectedAsync()
    {
        var item = Selected();
        if (item is null) return;
        if (MessageBox.Show(this, $"Excluir a categoria \"{item.Name}\"?", "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        try { await _service.DeleteAsync(item.Id); await LoadAsync(); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}