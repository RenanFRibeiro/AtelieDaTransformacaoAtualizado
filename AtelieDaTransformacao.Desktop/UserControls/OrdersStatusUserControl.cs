using System.Drawing;
using System.Globalization;
using System.Text;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Forms;
using AtelieDaTransformacao.Desktop.Services;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.UserControls;

public sealed partial class OrdersStatusUserControl : UserControl
{
    private readonly OrdersApiService _orders = new();
    private readonly List<OrderListItemDto> _items = new();

    private List<OrderListItemDto> _filteredItems = new();

    private bool _loadingOrders;
    private bool _updatingStatus;

    private int _currentPage = 1;

    private const int PageSize = 7;

    private readonly Color _pageBackground =
        Color.FromArgb(43, 26, 18);

    private readonly Color _panelBackground =
        Color.FromArgb(65, 40, 27);

    private readonly Color _gold =
        Color.FromArgb(217, 168, 91);

    public OrdersStatusUserControl()
    {
        InitializeComponent();

        ConfigureEvents();
        ConfigureFilters();

        // Usuários comuns podem consultar os pedidos, mas somente administradores
        // podem alterar o status.
        _statusColumn.ReadOnly = !SessionManager.IsAdmin;
        _statusColumn.DefaultCellStyle.BackColor = !SessionManager.IsAdmin
            ? Color.FromArgb(235, 235, 235)
            : Color.White;

        Load += OrdersStatusUserControl_Load;
    }

    private void ConfigureEvents()
    {
        _refreshButton.Click += RefreshButton_Click;
        _clearButton.Click += ClearButton_Click;
        _exportButton.Click += ExportButton_Click;

        _statusComboBox.SelectedIndexChanged +=
            StatusComboBox_SelectedIndexChanged;

        _searchTextBox.KeyDown +=
            SearchTextBox_KeyDown;

        _grid.CellContentClick +=
            Grid_CellContentClick;

        _grid.CellPainting +=
            Grid_CellPainting;

        _grid.CurrentCellDirtyStateChanged +=
            Grid_CurrentCellDirtyStateChanged;

        _grid.CellValueChanged +=
            Grid_CellValueChanged;

        _previousPageButton.Click +=
            (_, _) => ChangePage(_currentPage - 1);

        _page1Button.Click +=
            (_, _) => ChangePage(1);

        _page2Button.Click +=
            (_, _) => ChangePage(2);

        _page3Button.Click +=
            (_, _) => ChangePage(3);

        _nextPageButton.Click +=
            (_, _) => ChangePage(_currentPage + 1);

        _headingPanel.Resize +=
            HeadingPanel_Resize;

        _bottomPanel.Resize +=
            BottomPanel_Resize;
    }

    private void HeadingPanel_Resize(
        object? sender,
        EventArgs e)
    {
        _refreshButton.Location = new Point(
            Math.Max(
                0,
                _headingPanel.ClientSize.Width -
                _refreshButton.Width),
            4);
    }

    private void BottomPanel_Resize(
        object? sender,
        EventArgs e)
    {
        _pageLabel.Location = new Point(
            Math.Max(
                _countLabel.Right + 10,
                _bottomPanel.ClientSize.Width -
                _paginationPanel.Width -
                _pageLabel.Width -
                12),
            13);
    }

    private void ConfigureFilters()
    {
        _startDatePicker.Value =
            DateTime.Today.AddDays(-30);

        _endDatePicker.Value =
            DateTime.Today;

        _statusComboBox.Items.Clear();

        _statusComboBox.Items.AddRange(
            new object[]
            {
                "Todos",
                "Criado",
                "Pendente",
                "Aprovado",
                "Separação",
                "Faturado",
                "Enviado",
                "Entregue"
            });

        _statusComboBox.SelectedIndex = 0;
    }

    private async void OrdersStatusUserControl_Load(
        object? sender,
        EventArgs e)
    {
        await LoadOrdersAsync();
    }

    private async void RefreshButton_Click(
        object? sender,
        EventArgs e)
    {
        await LoadOrdersAsync();
    }

    private async Task LoadOrdersAsync()
    {
        if (_loadingOrders)
            return;

        try
        {
            _loadingOrders = true;

            SetLoadingState(true);

            var orders =
                await _orders.GetAllAsync();

            _items.Clear();

            if (orders is not null)
            {
                _items.AddRange(orders);
            }

            _currentPage = 1;

            ApplyFilters();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                FindForm(),
                $"Não foi possível carregar os pedidos.\n\n{ex.Message}",
                "Pedidos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            _loadingOrders = false;

            SetLoadingState(false);
        }
    }

    private void SetLoadingState(
        bool loading)
    {
        _refreshButton.Enabled = !loading;
        _clearButton.Enabled = !loading;
        _exportButton.Enabled = !loading;

        _statusComboBox.Enabled = !loading;
        _searchTextBox.Enabled = !loading;

        _startDatePicker.Enabled = !loading;
        _endDatePicker.Enabled = !loading;
    }

    private void ClearButton_Click(
        object? sender,
        EventArgs e)
    {
        _startDatePicker.Value =
            DateTime.Today.AddDays(-30);

        _endDatePicker.Value =
            DateTime.Today;

        _statusComboBox.SelectedIndex = 0;

        _searchTextBox.Clear();

        _currentPage = 1;

        ApplyFilters();
    }

    private void ExportButton_Click(
        object? sender,
        EventArgs e)
    {
        ExportCsv();
    }

    private void StatusComboBox_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        if (!IsHandleCreated)
            return;

        _currentPage = 1;

        ApplyFilters();
    }

    private void SearchTextBox_KeyDown(
        object? sender,
        KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
            return;

        e.SuppressKeyPress = true;

        _currentPage = 1;

        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var start =
            _startDatePicker.Value.Date;

        var end =
            _endDatePicker.Value.Date
                .AddDays(1)
                .AddTicks(-1);

        var selectedStatus =
            _statusComboBox.SelectedItem?.ToString()
            ?? "Todos";

        var search =
            _searchTextBox.Text.Trim();

        _filteredItems = _items
            .Where(x =>
                x.Date >= start &&
                x.Date <= end)

            .Where(x =>
                selectedStatus == "Todos" ||
                GetStatusText(x.Status) ==
                selectedStatus)

            .Where(x =>
                string.IsNullOrWhiteSpace(search) ||
                x.Number.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase) ||
                x.Customer.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase))

            .OrderByDescending(x => x.Date)
            .ToList();

        var totalPages =
            Math.Max(
                1,
                (int)Math.Ceiling(
                    _filteredItems.Count /
                    (double)PageSize));

        _currentPage =
            Math.Min(
                Math.Max(1, _currentPage),
                totalPages);

        RenderCurrentPage();
    }

    private void RenderCurrentPage()
    {
        _grid.Rows.Clear();

        var pageItems =
            _filteredItems
                .Skip(
                    (_currentPage - 1) *
                    PageSize)
                .Take(PageSize)
                .ToList();

        foreach (var item in pageItems)
        {
            _grid.Rows.Add(
                item.Number,

                item.Date.ToString(
                    "dd/MM/yyyy HH:mm"),

                item.Customer,

                item.Total.ToString(
                    "C2",
                    new CultureInfo("pt-BR")),

                GetStatusText(
                    item.Status),

                "Detalhes");
        }

        var total =
            _filteredItems.Count;

        var first =
            total == 0
                ? 0
                : ((_currentPage - 1) *
                   PageSize) + 1;

        var last =
            Math.Min(
                _currentPage * PageSize,
                total);

        _countLabel.Text =
            $"Exibindo {first} a {last} de {total} registros";

        _pageLabel.Text =
            $"Página {_currentPage} de {GetTotalPages()}";

        UpdatePaginationState();
    }

    private int GetTotalPages()
    {
        return Math.Max(
            1,
            (int)Math.Ceiling(
                _filteredItems.Count /
                (double)PageSize));
    }

    private void ChangePage(
        int page)
    {
        var totalPages =
            GetTotalPages();

        if (page < 1 ||
            page > totalPages)
        {
            return;
        }

        _currentPage = page;

        RenderCurrentPage();
    }

    private void UpdatePaginationState()
    {
        var totalPages =
            GetTotalPages();

        _previousPageButton.Enabled =
            _currentPage > 1;

        _nextPageButton.Enabled =
            _currentPage < totalPages;

        _page1Button.Enabled =
            totalPages >= 1;

        _page2Button.Visible =
            totalPages >= 2;

        _page3Button.Visible =
            totalPages >= 3;

        _page1Button.FillColor =
            _currentPage == 1
                ? _gold
                : Color.FromArgb(
                    220,
                    220,
                    220);

        _page2Button.FillColor =
            _currentPage == 2
                ? _gold
                : Color.FromArgb(
                    220,
                    220,
                    220);

        _page3Button.FillColor =
            _currentPage == 3
                ? _gold
                : Color.FromArgb(
                    220,
                    220,
                    220);
    }

    // ============================================================
    // ALTERAÇÃO DE STATUS
    // ============================================================

    private void Grid_CurrentCellDirtyStateChanged(
        object? sender,
        EventArgs e)
    {
        if (_grid.IsCurrentCellDirty &&
            _grid.CurrentCell is
                DataGridViewComboBoxCell)
        {
            _grid.CommitEdit(
                DataGridViewDataErrorContexts.Commit);
        }
    }

    private void Grid_CellValueChanged(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (e.ColumnIndex < 0)
            return;

        // IMPORTANTE:
        // O nome real da coluna no Designer é
        // _statusColumn.
        if (e.ColumnIndex !=
            _statusColumn.Index)
        {
            return;
        }

        if (e.RowIndex >=
            _grid.Rows.Count)
        {
            return;
        }

        var selectedText =
            Convert.ToString(
                _grid.Rows[e.RowIndex]
                    .Cells[
                        _statusColumn.Index
                    ]
                    .Value);

        if (!TryParseStatus(
                selectedText,
                out var newStatus))
        {
            return;
        }

        var absoluteIndex =
            ((_currentPage - 1) *
             PageSize) +
            e.RowIndex;

        if (absoluteIndex < 0 ||
            absoluteIndex >=
            _filteredItems.Count)
        {
            return;
        }

        var item =
            _filteredItems[
                absoluteIndex];

        var previousStatus =
            item.Status;

        var previousLastUpdate =
            item.LastUpdate;

        if (previousStatus ==
            newStatus)
        {
            return;
        }

        _ = UpdateOrderStatusAsync(
            item,
            previousStatus,
            previousLastUpdate,
            newStatus,
            e.RowIndex);
    }

    private async Task UpdateOrderStatusAsync(
        OrderListItemDto item,
        OrderStatus previousStatus,
        DateTime previousLastUpdate,
        OrderStatus newStatus,
        int rowIndex)
    {
        if (_updatingStatus)
            return;

        try
        {
            _updatingStatus = true;

            _grid.Enabled = false;

            /*
             * DESKTOP → API
             *
             * PUT:
             *
             * orders/{id}/status
             *
             * Envia o enum OrderStatus.
             */
            var updatedOrder =
                await _orders.UpdateStatusAsync(
                    item.Id,
                    newStatus);

            /*
             * A API deve retornar o pedido
             * já atualizado.
             */
            if (updatedOrder is null)
            {
                throw new InvalidOperationException(
                    "A API não retornou o pedido atualizado.");
            }

            /*
             * Atualiza o objeto local
             * com os dados vindos da API.
             */
            item.Status =
                updatedOrder.Status;

            item.LastUpdate =
                updatedOrder.LastUpdate;

            /*
             * Atualiza a célula de Status.
             */
            _grid.Rows[rowIndex]
                .Cells[
                    _statusColumn.Index
                ]
                .Value =
                GetStatusText(
                    updatedOrder.Status);

            /*
             * Redesenha somente o status.
             * A coluna de data não é mais exibida: ela foi substituída
             * pela coluna Ações/Detalhes.
             */
            _grid.InvalidateCell(
                _statusColumn.Index,
                rowIndex);
        }
        catch (Exception ex)
        {
            /*
             * Se a API falhar,
             * volta ao status anterior.
             */
            item.Status =
                previousStatus;

            item.LastUpdate =
                previousLastUpdate;

            _grid.Rows[rowIndex]
                .Cells[
                    _statusColumn.Index
                ]
                .Value =
                GetStatusText(
                    previousStatus);

            _grid.InvalidateRow(
                rowIndex);

            MessageBox.Show(
                FindForm(),
                $"Não foi possível atualizar o status do pedido #{item.Id}.\n\n{ex.Message}",
                "Atualização do pedido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            _grid.Enabled = true;

            _updatingStatus = false;
        }
    }

    // ============================================================
    // BOTÃO DETALHES
    // ============================================================

    private void Grid_CellContentClick(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (e.ColumnIndex < 0)
            return;

        /*
         * O nome real da coluna no Designer é
         * _actionsColumn.
         */
        if (e.ColumnIndex !=
            _actionsColumn.Index)
        {
            return;
        }

        var index =
            ((_currentPage - 1) *
             PageSize) +
            e.RowIndex;

        if (index < 0 ||
            index >=
            _filteredItems.Count)
        {
            return;
        }

        using var dialog =
            new OrderDetailsDialog(
                _filteredItems[index]);

        dialog.ShowDialog(
            FindForm());
    }

    // ============================================================
    // PINTURA DO STATUS
    // ============================================================

    private void Grid_CellPainting(
        object? sender,
        DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (e.ColumnIndex < 0)
            return;

        /*
         * O nome real da coluna é
         * _statusColumn.
         */
        if (e.ColumnIndex !=
            _statusColumn.Index)
        {
            return;
        }

        /*
         * Não interfere enquanto
         * o ComboBox estiver aberto.
         */
        if (_grid.CurrentCell?.RowIndex ==
                e.RowIndex &&
            _grid.CurrentCell.ColumnIndex ==
                e.ColumnIndex &&
            _grid.IsCurrentCellInEditMode)
        {
            return;
        }

        e.Handled = true;

        e.PaintBackground(
            e.CellBounds,
            true);

        var text =
            e.FormattedValue?.ToString()
            ?? string.Empty;

        var bounds =
            new Rectangle(
                e.CellBounds.X + 8,
                e.CellBounds.Y + 7,
                Math.Max(
                    20,
                    e.CellBounds.Width - 16),
                Math.Max(
                    18,
                    e.CellBounds.Height - 14));

        using var path =
            RoundedRect(
                bounds,
                7);

        using var brush =
            new SolidBrush(
                StatusColor(text));

        e.Graphics.SmoothingMode =
            System.Drawing.Drawing2D
                .SmoothingMode.AntiAlias;

        e.Graphics.FillPath(
            brush,
            path);

        using var font =
            new Font(
                "Segoe UI Semibold",
                7.5F);

        using var textBrush =
            new SolidBrush(
                Color.White);

        var size =
            e.Graphics.MeasureString(
                text,
                font);

        e.Graphics.DrawString(
            text,
            font,
            textBrush,
            bounds.X +
                (bounds.Width -
                 size.Width) / 2,
            bounds.Y +
                (bounds.Height -
                 size.Height) / 2 +
                1);

        e.Paint(
            e.ClipBounds,
            DataGridViewPaintParts.Border);
    }

    // ============================================================
    // EXPORTAÇÃO
    // ============================================================

    private void ExportCsv()
    {
        using var dialog =
            new SaveFileDialog
            {
                Filter =
                    "CSV (*.csv)|*.csv",

                FileName =
                    $"pedidos_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

        if (dialog.ShowDialog(
                FindForm()) !=
            DialogResult.OK)
        {
            return;
        }

        var sb =
            new StringBuilder();

        sb.AppendLine(
            "Nº Pedido;Data;Cliente;Valor Total;Status;Ações");

        foreach (var item in _filteredItems)
        {
            sb.AppendLine(
                $"{item.Number};" +
                $"{item.Date:dd/MM/yyyy HH:mm};" +
                $"{item.Customer};" +
                $"{item.Total.ToString("F2", CultureInfo.InvariantCulture)};" +
                $"{GetStatusText(item.Status)};" +
                $"{item.LastUpdate:dd/MM/yyyy HH:mm}");
        }

        File.WriteAllText(
            dialog.FileName,
            sb.ToString(),
            new UTF8Encoding(true));

        MessageBox.Show(
            FindForm(),
            "Pedidos exportados com sucesso.",
            "Exportação",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    // ============================================================
    // UTILITÁRIOS
    // ============================================================

    private static System.Drawing.Drawing2D.GraphicsPath RoundedRect(
        Rectangle rect,
        int radius)
    {
        var path =
            new System.Drawing.Drawing2D
                .GraphicsPath();

        var diameter =
            radius * 2;

        path.AddArc(
            rect.X,
            rect.Y,
            diameter,
            diameter,
            180,
            90);

        path.AddArc(
            rect.Right - diameter,
            rect.Y,
            diameter,
            diameter,
            270,
            90);

        path.AddArc(
            rect.Right - diameter,
            rect.Bottom - diameter,
            diameter,
            diameter,
            0,
            90);

        path.AddArc(
            rect.X,
            rect.Bottom - diameter,
            diameter,
            diameter,
            90,
            90);

        path.CloseFigure();

        return path;
    }

    private static Color StatusColor(
        string text)
    {
        return text switch
        {
            "Criado" =>
                Color.FromArgb(
                    120,
                    120,
                    120),

            "Pendente" =>
                Color.FromArgb(
                    205,
                    145,
                    24),

            "Aprovado" =>
                Color.FromArgb(
                    46,
                    112,
                    205),

            "Separação" =>
                Color.FromArgb(
                    16,
                    163,
                    181),

            "Faturado" =>
                Color.FromArgb(
                    123,
                    69,
                    205),

            "Enviado" =>
                Color.FromArgb(
                    42,
                    94,
                    190),

            "Entregue" =>
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

    private static bool TryParseStatus(
        string? text,
        out OrderStatus status)
    {
        status = text switch
        {
            "Criado" =>
                OrderStatus.Created,

            "Pendente" =>
                OrderStatus.Pending,

            "Aprovado" =>
                OrderStatus.Approved,

            "Separação" =>
                OrderStatus.Separation,

            "Faturado" =>
                OrderStatus.Invoiced,

            "Enviado" =>
                OrderStatus.Shipped,

            "Entregue" =>
                OrderStatus.Delivered,

            _ =>
                OrderStatus.Created
        };

        return text is
            "Criado" or
            "Pendente" or
            "Aprovado" or
            "Separação" or
            "Faturado" or
            "Enviado" or
            "Entregue";
    }

    private static string GetStatusText(
        OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Created =>
                "Criado",

            OrderStatus.Pending =>
                "Pendente",

            OrderStatus.Approved =>
                "Aprovado",

            OrderStatus.Separation =>
                "Separação",

            OrderStatus.Invoiced =>
                "Faturado",

            OrderStatus.Shipped =>
                "Enviado",

            OrderStatus.Delivered =>
                "Entregue",

            _ =>
                status.ToString()
        };
    }
}
