using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.Domain.Entities;

public sealed class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    // Compatibilidade com bancos existentes que ainda possuem os campos
    // obrigatórios do checkout como colunas da tabela Orders.
    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public string ShippingAddress { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public string ItemsJson { get; set; } = "[]";

    // Snapshot dos dados informados no checkout. Mantém histórico sem depender
    // das informações que o cliente possa alterar futuramente no Identity.
    public string CheckoutJson { get; set; } = "{}";

    public decimal Total { get; set; }

    public OrderStatus Status { get; set; } =
        OrderStatus.Criado;

    public bool AutoAdvance { get; set; }

    public DateTime CreatedAt { get; set; } =
        DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } =
        DateTime.UtcNow;

    public DateTime StatusChangedAt { get; set; } =
        DateTime.UtcNow;
}

public sealed class OrderItemSnapshot
{
    public int ProductId { get; set; }

    public string Title { get; set; } =
        string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal =>
        UnitPrice * Quantity;
}

public sealed class OrderCheckoutSnapshot
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string AddressNumber { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string DeliveryMethod { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
