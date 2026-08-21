using System.Globalization;
using System.Text;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Forms;
using Guna.UI2.WinForms;

namespace AtelieDaTransformacao.Desktop.UserControls;

public sealed partial class OrdersStatusUserControl : UserControl
{
    private readonly List<OrderListItemDto> _items = CreateMockItems();
    private readonly Color _pageBackground = Color.FromArgb(43, 26, 18);
    private readonly Color _panelBackground = Color.FromArgb(65, 40, 27);
    private readonly Color _gold = Color.FromArgb(217, 168, 91);
    private List<OrderListItemDto> _filteredItems = new();
    private int _currentPage = 1;
    private const int PageSize = 7;

    public OrdersStatusUserControl()
    {
        InitializeComponent();
        ConfigureEvents();
        ConfigureFilters();
        ApplyFilters();
    }

    private void ConfigureEvents()
    {
        _refreshButton.Click += RefreshButton_Click;
        _clearButton.Click += ClearButton_Click;
        _exportButton.Click += ExportButton_Click;
        _statusComboBox.SelectedIndexChanged += StatusComboBox_SelectedIndexChanged;
        _searchTextBox.KeyDown += SearchTextBox_KeyDown;
        _grid.CellContentClick += Grid_CellContentClick;
        _grid.CellPainting += Grid_CellPainting;
        _grid.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;
        _grid.CellValueChanged += Grid_CellValueChanged;

        _previousPageButton.Click += (_, _) => ChangePage(_currentPage - 1);
        _page1Button.Click += (_, _) => ChangePage(1);
        _page2Button.Click += (_, _) => ChangePage(2);
        _page3Button.Click += (_, _) => ChangePage(3);
        _nextPageButton.Click += (_, _) => ChangePage(_currentPage + 1);
        _headingPanel.Resize += HeadingPanel_Resize;
        _bottomPanel.Resize += BottomPanel_Resize;
    }

    private void HeadingPanel_Resize(object? sender, EventArgs e)
    {
        _refreshButton.Location = new Point(
            Math.Max(0, _headingPanel.ClientSize.Width - _refreshButton.Width), 4);
    }

    private void BottomPanel_Resize(object? sender, EventArgs e)
    {
        _pageLabel.Location = new Point(
            Math.Max(_countLabel.Right + 10,
                _bottomPanel.ClientSize.Width - _paginationPanel.Width - _pageLabel.Width - 12), 13);
    }

    private void ConfigureFilters()
    {
        _startDatePicker.Value = DateTime.Today.AddDays(-30);
        _endDatePicker.Value = DateTime.Today;
        _statusComboBox.Items.Clear();
        _statusComboBox.Items.AddRange(new object[]
        {
            "Todos", "Criado", "Pendente", "Aprovado", "Separação", "Faturado", "Enviado", "Entregue"
        });
        _statusComboBox.SelectedIndex = 0;
    }

    private void RefreshButton_Click(object? sender, EventArgs e) => ApplyFilters();

    private void ClearButton_Click(object? sender, EventArgs e)
    {
        _startDatePicker.Value = DateTime.Today.AddDays(-30);
        _endDatePicker.Value = DateTime.Today;
        _statusComboBox.SelectedIndex = 0;
        _searchTextBox.Clear();
        _currentPage = 1;
        ApplyFilters();
    }

    private void ExportButton_Click(object? sender, EventArgs e) => ExportCsv();

    private void StatusComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (IsHandleCreated)
        {
            _currentPage = 1;
            ApplyFilters();
        }
    }

    private void SearchTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        e.SuppressKeyPress = true;
        _currentPage = 1;
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var start = _startDatePicker.Value.Date;
        var end = _endDatePicker.Value.Date.AddDays(1).AddTicks(-1);
        var selectedStatus = _statusComboBox.SelectedItem?.ToString() ?? "Todos";
        var search = _searchTextBox.Text.Trim();

        _filteredItems = _items
            .Where(x => x.Date >= start && x.Date <= end)
            .Where(x => selectedStatus == "Todos" || GetStatusText(x.Status) == selectedStatus)
            .Where(x => string.IsNullOrWhiteSpace(search)
                        || x.Number.Contains(search, StringComparison.OrdinalIgnoreCase)
                        || x.Customer.Contains(search, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.Date)
            .ToList();

        var totalPages = Math.Max(1, (int)Math.Ceiling(_filteredItems.Count / (double)PageSize));
        _currentPage = Math.Min(Math.Max(1, _currentPage), totalPages);
        RenderCurrentPage();
    }

    private void RenderCurrentPage()
    {
        _grid.Rows.Clear();

        var pageItems = _filteredItems
            .Skip((_currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        foreach (var item in pageItems)
        {
            _grid.Rows.Add(
                item.Number,
                item.Date.ToString("dd/MM/yyyy HH:mm"),
                item.Customer,
                item.Total.ToString("C2", new CultureInfo("pt-BR")),
                GetStatusText(item.Status),
                item.LastUpdate.ToString("dd/MM/yyyy HH:mm"),
                "Detalhes");
        }

        var total = _filteredItems.Count;
        var first = total == 0 ? 0 : ((_currentPage - 1) * PageSize) + 1;
        var last = Math.Min(_currentPage * PageSize, total);
        _countLabel.Text = $"Exibindo {first} a {last} de {total} registros";
        _pageLabel.Text = $"Página {_currentPage} de {Math.Max(1, GetTotalPages())}";

        UpdatePaginationState();
    }

    private int GetTotalPages() => Math.Max(1, (int)Math.Ceiling(_filteredItems.Count / (double)PageSize));

    private void ChangePage(int page)
    {
        var totalPages = GetTotalPages();
        if (page < 1 || page > totalPages) return;
        _currentPage = page;
        RenderCurrentPage();
    }

    private void UpdatePaginationState()
    {
        var totalPages = GetTotalPages();
        _previousPageButton.Enabled = _currentPage > 1;
        _nextPageButton.Enabled = _currentPage < totalPages;
        _page1Button.Enabled = totalPages >= 1;
        _page2Button.Visible = totalPages >= 2;
        _page3Button.Visible = totalPages >= 3;
        _page1Button.FillColor = _currentPage == 1 ? _gold : Color.FromArgb(82, 55, 38);
        _page2Button.FillColor = _currentPage == 2 ? _gold : Color.FromArgb(82, 55, 38);
        _page3Button.FillColor = _currentPage == 3 ? _gold : Color.FromArgb(82, 55, 38);
    }

    private void Grid_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (_grid.IsCurrentCellDirty && _grid.CurrentCell is DataGridViewComboBoxCell)
            _grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void Grid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Status") return;
        if (e.RowIndex >= _grid.Rows.Count) return;

        var selectedText = Convert.ToString(_grid.Rows[e.RowIndex].Cells["Status"].Value);
        if (!TryParseStatus(selectedText, out var newStatus)) return;

        var absoluteIndex = ((_currentPage - 1) * PageSize) + e.RowIndex;
        if (absoluteIndex < 0 || absoluteIndex >= _filteredItems.Count) return;

        var item = _filteredItems[absoluteIndex];
        item.Status = newStatus;
        item.LastUpdate = DateTime.Now;
        _grid.Rows[e.RowIndex].Cells["LastUpdate"].Value = item.LastUpdate.ToString("dd/MM/yyyy HH:mm");
        _grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
    }

    private void Grid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Actions") return;

        var index = ((_currentPage - 1) * PageSize) + e.RowIndex;
        if (index < 0 || index >= _filteredItems.Count) return;

        using var dialog = new OrderDetailsDialog(_filteredItems[index]);
        dialog.ShowDialog(FindForm());
    }

    private void Grid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 || _grid.Columns[e.ColumnIndex].Name != "Status") return;
        if (_grid.CurrentCell?.RowIndex == e.RowIndex &&
            _grid.CurrentCell.ColumnIndex == e.ColumnIndex &&
            _grid.IsCurrentCellInEditMode) return;

        e.Handled = true;
        e.PaintBackground(e.CellBounds, true);
        var text = e.FormattedValue?.ToString() ?? string.Empty;
        var bounds = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 7,
            Math.Max(20, e.CellBounds.Width - 16), Math.Max(18, e.CellBounds.Height - 14));

        using var path = RoundedRect(bounds, 7);
        using var brush = new SolidBrush(StatusColor(text));
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

    private void ExportCsv()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = $"pedidos_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;

        var sb = new StringBuilder();
        sb.AppendLine("Nº Pedido;Data;Cliente;Valor Total;Status;Última Atualização");

        foreach (var item in _filteredItems)
        {
            sb.AppendLine(
                $"{item.Number};{item.Date:dd/MM/yyyy HH:mm};{item.Customer};" +
                $"{item.Total.ToString("F2", CultureInfo.InvariantCulture)};" +
                $"{GetStatusText(item.Status)};{item.LastUpdate:dd/MM/yyyy HH:mm}");
        }

        File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));
        MessageBox.Show(FindForm(), "Pedidos exportados com sucesso.", "Exportação",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

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

    private static Color StatusColor(string text) => text switch
    {
        "Criado" => Color.FromArgb(120, 120, 120),
        "Pendente" => Color.FromArgb(205, 145, 24),
        "Aprovado" => Color.FromArgb(46, 112, 205),
        "Separação" => Color.FromArgb(16, 163, 181),
        "Faturado" => Color.FromArgb(123, 69, 205),
        "Enviado" => Color.FromArgb(42, 94, 190),
        "Entregue" => Color.FromArgb(35, 164, 64),
        _ => Color.FromArgb(120, 120, 120)
    };

    private static bool TryParseStatus(string? text, out OrderStatus status)
    {
        status = text switch
        {
            "Criado" => OrderStatus.Created,
            "Pendente" => OrderStatus.Pending,
            "Aprovado" => OrderStatus.Approved,
            "Separação" => OrderStatus.Separation,
            "Faturado" => OrderStatus.Invoiced,
            "Enviado" => OrderStatus.Shipped,
            "Entregue" => OrderStatus.Delivered,
            _ => OrderStatus.Created
        };

        return text is "Criado" or "Pendente" or "Aprovado" or "Separação" or "Faturado" or "Enviado" or "Entregue";
    }

    private static string GetStatusText(OrderStatus status) => status switch
    {
        OrderStatus.Created => "Criado",
        OrderStatus.Pending => "Pendente",
        OrderStatus.Approved => "Aprovado",
        OrderStatus.Separation => "Separação",
        OrderStatus.Invoiced => "Faturado",
        OrderStatus.Shipped => "Enviado",
        OrderStatus.Delivered => "Entregue",
        _ => status.ToString()
    };

    private static List<OrderListItemDto> CreateMockItems()
    {
        var customers = new[]
        {
            "João da Silva", "Maria Oliveira", "Carlos Santos", "Ana Paula Lima",
            "Lucas Ferreira", "Juliana Costa", "Roberto Almeida", "Fernanda Souza",
            "Marcos Ribeiro", "Camila Martins", "Bruno Alves", "Patrícia Gomes"
        };
        var statuses = new[]
        {
            OrderStatus.Created, OrderStatus.Pending, OrderStatus.Approved, OrderStatus.Separation,
            OrderStatus.Invoiced, OrderStatus.Shipped, OrderStatus.Delivered, OrderStatus.Pending,
            OrderStatus.Approved, OrderStatus.Shipped, OrderStatus.Created, OrderStatus.Delivered
        };
        var values = new[]
        {
            259.90m, 189.50m, 329.00m, 149.90m, 499.00m, 279.90m,
            159.90m, 620.00m, 89.90m, 345.00m, 219.90m, 780.00m
        };

        var list = new List<OrderListItemDto>();
        for (var i = 0; i < customers.Length; i++)
        {
            var date = DateTime.Now.AddDays(-(i + 1)).AddMinutes(-(i * 13));
            list.Add(new OrderListItemDto
            {
                Id = i + 1,
                Number = $"#{125 - i:000000}",
                Date = date,
                Customer = customers[i],
                Total = values[i],
                Status = statuses[i],
                LastUpdate = date.AddMinutes(12 + i)
            });
        }

        return list;
    }
}
