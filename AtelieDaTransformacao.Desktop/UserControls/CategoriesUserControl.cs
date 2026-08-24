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

        // ============================================================
        // CONFIGURAÇÃO VISUAL DA TABELA
        // PADRÃO: CINZA + BRANCO + TEXTO PRETO
        // ============================================================

        _grid.EnableHeadersVisualStyles = false;

        // ============================================================
        // CABEÇALHO / TÍTULO DAS COLUNAS
        // ============================================================

        // Fundo do título das colunas
        _grid.ColumnHeadersDefaultCellStyle.BackColor =
            Color.FromArgb(230, 230, 230);

        // Texto do título
        _grid.ColumnHeadersDefaultCellStyle.ForeColor =
            Color.Black;

        // Fonte do título
        _grid.ColumnHeadersDefaultCellStyle.Font =
            new Font("Segoe UI Semibold", 9F);

        // Alinhamento
        _grid.ColumnHeadersDefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleLeft;

        // Mantém a mesma cor quando o cabeçalho receber foco
        _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor =
            Color.FromArgb(230, 230, 230);

        _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor =
            Color.Black;

        // ============================================================
        // LINHAS DA TABELA
        // ============================================================

        // Fundo principal das linhas
        _grid.DefaultCellStyle.BackColor =
            Color.White;

        // Texto das linhas
        _grid.DefaultCellStyle.ForeColor =
            Color.Black;

        // Fonte
        _grid.DefaultCellStyle.Font =
            new Font("Segoe UI", 9F);

        // ============================================================
        // LINHAS ALTERNADAS
        // ============================================================

        // Mantém um cinza muito suave para diferenciar as linhas
        _grid.AlternatingRowsDefaultCellStyle.BackColor =
            Color.FromArgb(248, 248, 248);

        _grid.AlternatingRowsDefaultCellStyle.ForeColor =
            Color.Black;

        // ============================================================
        // SELEÇÃO DE LINHA
        // ============================================================

        // Cinza mais escuro para indicar a seleção
        _grid.DefaultCellStyle.SelectionBackColor =
            Color.FromArgb(210, 210, 210);

        _grid.DefaultCellStyle.SelectionForeColor =
            Color.Black;

        // ============================================================
        // CONFIGURAÇÃO DO GUNA
        // ============================================================

        try
        {
            // Fundo das linhas
            _grid.ThemeStyle.RowsStyle.BackColor =
                Color.White;

            // Texto das linhas
            _grid.ThemeStyle.RowsStyle.ForeColor =
                Color.Black;

            // Fundo da linha selecionada
            _grid.ThemeStyle.RowsStyle.SelectionBackColor =
                Color.FromArgb(210, 210, 210);

            // Texto da linha selecionada
            _grid.ThemeStyle.RowsStyle.SelectionForeColor =
                Color.Black;

            // --------------------------------------------------------
            // CABEÇALHO GUNA
            // --------------------------------------------------------

            _grid.ThemeStyle.HeaderStyle.BackColor =
                Color.FromArgb(230, 230, 230);

            _grid.ThemeStyle.HeaderStyle.ForeColor =
                Color.Black;

            _grid.ThemeStyle.HeaderStyle.Font =
                new Font("Segoe UI Semibold", 9F);

            _grid.ThemeStyle.HeaderStyle.Height = 42;

            // --------------------------------------------------------
            // LINHA DE SEPARAÇÃO
            // --------------------------------------------------------

            _grid.ThemeStyle.GridColor =
                Color.FromArgb(205, 205, 205);
        }
        catch
        {
            // Algumas versões do Guna podem não expor
            // todas as propriedades em runtime.
        }

        // ============================================================
        // LINHAS DE SEPARAÇÃO DA TABELA
        // ============================================================

        _grid.GridColor =
            Color.FromArgb(205, 205, 205);

        _grid.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;

        // ============================================================
        // TAMANHOS
        // ============================================================

        _grid.RowHeadersVisible = false;

        _grid.ColumnHeadersHeight = 42;

        _grid.RowTemplate.Height = 42;

        // ============================================================
        // CONFIGURAÇÕES DOS BOTÕES
        // NÃO ALTERADO
        // ============================================================

        _newButton.Visible = SessionManager.IsAdmin;
        _editButton.Visible = SessionManager.IsAdmin;
        _deleteButton.Visible = SessionManager.IsAdmin;

        _refreshButton.Click += async (_, _) =>
            await LoadAsync();

        _newButton.Click += async (_, _) =>
            await EditAsync(null);

        _editButton.Click += async (_, _) =>
            await EditSelectedAsync();

        _deleteButton.Click += async (_, _) =>
            await DeleteSelectedAsync();

        _grid.CellDoubleClick += async (_, _) =>
            await EditSelectedAsync();

        Load += async (_, _) =>
            await LoadAsync();
    }

    // ================================================================
    // CARREGAR CATEGORIAS
    // ================================================================

    private async Task LoadAsync()
    {
        try
        {
            _items = await _service.GetAllAsync() ?? new();

            _grid.DataSource = _items
                .Select(x => new
                {
                    x.Id,
                    Categoria = x.Name,
                    Descrição = x.Description
                })
                .ToList();

            // Oculta o ID
            if (_grid.Columns["Id"] is not null)
                _grid.Columns["Id"].Visible = false;

            // Contadores: rodapé + card de resumo no topo.
            _countLabel.Text =
                $"{_items.Count} categoria(s) cadastrada(s)";
            _categoryCountValue.Text = _items.Count.ToString("N0");

            // ========================================================
            // GARANTE O PADRÃO VISUAL APÓS CARREGAR O DATASOURCE
            // ========================================================

            foreach (DataGridViewRow row in _grid.Rows)
            {
                // Todas as linhas ficam brancas
                row.DefaultCellStyle.BackColor =
                    Color.White;

                // Texto preto
                row.DefaultCellStyle.ForeColor =
                    Color.Black;
            }

            // ========================================================
            // CABEÇALHO
            // ========================================================

            _grid.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(230, 230, 230);

            _grid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.Black;

            _grid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI Semibold", 9F);

            _grid.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(230, 230, 230);

            _grid.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.Black;

            // ========================================================
            // LINHA DE SEPARAÇÃO
            // ========================================================

            _grid.GridColor =
                Color.FromArgb(205, 205, 205);

            try
            {
                _grid.ThemeStyle.GridColor =
                    Color.FromArgb(205, 205, 205);

                _grid.ThemeStyle.HeaderStyle.BackColor =
                    Color.FromArgb(230, 230, 230);

                _grid.ThemeStyle.HeaderStyle.ForeColor =
                    Color.Black;

                _grid.ThemeStyle.RowsStyle.BackColor =
                    Color.White;

                _grid.ThemeStyle.RowsStyle.ForeColor =
                    Color.Black;

                _grid.ThemeStyle.RowsStyle.SelectionBackColor =
                    Color.FromArgb(210, 210, 210);

                _grid.ThemeStyle.RowsStyle.SelectionForeColor =
                    Color.Black;
            }
            catch
            {
                // Ignora diferenças entre versões do Guna.
            }
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

    // ================================================================
    // ITEM SELECIONADO
    // ================================================================

    private CategoryDto? Selected()
    {
        if (_grid.CurrentRow is null ||
            _grid.CurrentRow.Cells["Id"]?.Value is null)
        {
            return null;
        }

        return _items.FirstOrDefault(
            x => x.Id ==
            Convert.ToInt32(
                _grid.CurrentRow.Cells["Id"].Value));
    }

    // ================================================================
    // EDITAR ITEM SELECIONADO
    // ================================================================

    private async Task EditSelectedAsync()
    {
        var item = Selected();

        if (item is not null)
            await EditAsync(item);
    }

    // ================================================================
    // CRIAR / EDITAR CATEGORIA
    // ================================================================

    private async Task EditAsync(CategoryDto? item)
    {
        if (!SessionManager.IsAdmin)
            return;

        using var dialog = new CategoryDialog(item);

        if (dialog.ShowDialog(FindForm()) != DialogResult.OK ||
            dialog.Result is null)
        {
            return;
        }

        try
        {
            if (item is null)
            {
                await _service.CreateAsync(dialog.Result);
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
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // ================================================================
    // EXCLUIR CATEGORIA
    // ================================================================

    private async Task DeleteSelectedAsync()
    {
        var item = Selected();

        if (item is null)
            return;

        if (MessageBox.Show(
                this,
                $"Excluir a categoria \"{item.Name}\"?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
            != DialogResult.Yes)
        {
            return;
        }

        try
        {
            await _service.DeleteAsync(item.Id);

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

    private void _deleteButton_Click(object sender, EventArgs e)
    {

    }
}