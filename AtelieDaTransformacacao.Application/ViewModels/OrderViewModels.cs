using System.ComponentModel.DataAnnotations;
using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Application.ViewModels;

public sealed class CheckoutViewModel
{
    public int? DirectProductId { get; set; }
    public int DirectQuantity { get; set; } = 1;

    public List<CheckoutItemViewModel> Items { get; set; } = new();

    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(150)]
    [Display(Name = "Nome")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu telefone.")]
    [StringLength(30)]
    [Display(Name = "Telefone")]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o endereço de entrega.")]
    [StringLength(500)]
    [Display(Name = "Endereço de entrega")]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione uma forma de pagamento.")]
    [Display(Name = "Forma de pagamento")]
    public string PaymentMethod { get; set; } = string.Empty;

    [StringLength(1000)]
    [Display(Name = "Observações")]
    public string Notes { get; set; } = string.Empty;

    public decimal Total => Items.Sum(x => x.Subtotal);
}


public sealed class CheckoutItemViewModel
{
    public int ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => UnitPrice * Quantity;
}

public sealed class OrderListItemViewModel
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public int ItemsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class OrderDetailsViewModel
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<OrderItemViewModel> Items { get; set; } = new();
}

public sealed class OrderItemViewModel
{
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}

public static class OrderStatusExtensions
{
    public static string Label(this OrderStatus status) => status switch
    {
        OrderStatus.Criado => "Criado",
        OrderStatus.Pendente => "Pendente",
        OrderStatus.Aprovado => "Aprovado",
        OrderStatus.Separacao => "Separação",
        OrderStatus.Faturado => "Faturado",
        OrderStatus.Enviado => "Enviado",
        OrderStatus.Entregue => "Entregue",
        _ => status.ToString()
    };

    public static string Icon(this OrderStatus status) => status switch
    {
        OrderStatus.Criado => "bi-file-earmark-plus",
        OrderStatus.Pendente => "bi-hourglass-split",
        OrderStatus.Aprovado => "bi-check-circle",
        OrderStatus.Separacao => "bi-box-seam",
        OrderStatus.Faturado => "bi-receipt",
        OrderStatus.Enviado => "bi-truck",
        OrderStatus.Entregue => "bi-house-check",
        _ => "bi-circle"
    };
}
