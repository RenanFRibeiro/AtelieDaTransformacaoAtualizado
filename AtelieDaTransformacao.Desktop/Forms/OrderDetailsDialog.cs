using System.Drawing;
using System.Globalization;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.Forms;

public sealed partial class OrderDetailsDialog : Form
{
    private readonly OrderListItemDto _order;

    public OrderDetailsDialog(OrderListItemDto order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));

        InitializeComponent();
        PopulateOrder();
    }

    private void PopulateOrder()
    {
        Text = $"Detalhes do Pedido {_order.Number}";
        _titleLabel.Text = $"Pedido {_order.Number}";
        _numberValueLabel.Text = _order.Number;
        _customerValueLabel.Text = _order.Customer;
        _dateValueLabel.Text = _order.Date.ToString("dd/MM/yyyy HH:mm");
        _totalValueLabel.Text = _order.Total.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));

        var statusText = GetStatusText(_order.Status);
        _statusValueLabel.Text = statusText;
        _statusValueLabel.ForeColor = GetStatusColor(_order.Status);

        _lastUpdateValueLabel.Text = _order.LastUpdate.ToString("dd/MM/yyyy HH:mm");
        _flowLabel.Text =
            $"Fluxo: Criado → Pendente → Aprovado → Separação → Faturado → Enviado → Entregue\n" +
            $"Status atual: {statusText}";
    }

    private void CloseButton_Click(object? sender, EventArgs e) => Close();

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

    private static Color GetStatusColor(OrderStatus status) => status switch
    {
        OrderStatus.Created => Color.FromArgb(170, 170, 170),
        OrderStatus.Pending => LibraryTheme.Warning,
        OrderStatus.Approved => Color.FromArgb(46, 112, 205),
        OrderStatus.Separation => Color.FromArgb(16, 163, 181),
        OrderStatus.Invoiced => Color.FromArgb(123, 69, 205),
        OrderStatus.Shipped => Color.FromArgb(42, 94, 190),
        OrderStatus.Delivered => LibraryTheme.Success,
        _ => Color.White
    };

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
