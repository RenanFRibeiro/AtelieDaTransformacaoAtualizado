using System.ComponentModel.DataAnnotations;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.Application.ViewModels;

public sealed class CheckoutViewModel
{
    public int? DirectProductId { get; set; }
    public int DirectQuantity { get; set; } = 1;

    public List<CheckoutItemViewModel> Items { get; set; } = new();

    [Required(ErrorMessage = "Informe seu nome completo.")]
    [StringLength(150)]
    [Display(Name = "Nome completo")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(180)]
    [Display(Name = "E-mail")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu telefone.")]
    [Phone(ErrorMessage = "Informe um telefone válido.")]
    [StringLength(30)]
    [Display(Name = "Telefone / WhatsApp")]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CEP.")]
    [StringLength(9)]
    [RegularExpression("^\\d{5}-?\\d{3}$", ErrorMessage = "Informe um CEP válido.")]
    [Display(Name = "CEP")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o endereço de entrega.")]
    [StringLength(180)]
    [Display(Name = "Endereço")]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o número.")]
    [StringLength(20)]
    [Display(Name = "Número")]
    public string AddressNumber { get; set; } = string.Empty;

    [StringLength(80)]
    [Display(Name = "Complemento")]
    public string Complement { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o bairro.")]
    [StringLength(100)]
    [Display(Name = "Bairro")]
    public string District { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(100)]
    [Display(Name = "Cidade")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(2, MinimumLength = 2)]
    [RegularExpression("^[A-Za-z]{2}$", ErrorMessage = "Use a UF com 2 letras.")]
    [Display(Name = "Estado")]
    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "Escolha como deseja receber o pedido.")]
    [Display(Name = "Entrega ou retirada")]
    public string DeliveryMethod { get; set; } = "Entrega";

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
