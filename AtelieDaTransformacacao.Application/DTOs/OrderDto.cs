using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.Application.DTOs;

public class OrderListDto
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public OrderStatus Status { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public bool AutoAdvance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime StatusChangedAt { get; set; }
}

public class OrderDetailsDto : OrderListDto
{
    public List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot> Items { get; set; } = new();

    // Propriedades adicionadas para a view Details.cshtml
    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string? CustomerPhone { get; set; }

    public string ShippingAddress { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;

    public string DeliveryMethod { get; set; } = string.Empty;

    public string? Notes { get; set; }
}