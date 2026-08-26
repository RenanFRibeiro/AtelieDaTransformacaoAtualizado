using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.Domain.Entities;

public sealed class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    // Dados informados no checkout
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }

    public string ItemsJson { get; set; } = "[]";

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