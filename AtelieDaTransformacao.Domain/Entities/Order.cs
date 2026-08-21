using System;

namespace AtelieDaTransformacao.Domain.Entities;

public enum OrderStatus
{
    Criado = 0,
    Pendente = 1,
    Aprovado = 2,
    Separacao = 3,
    Faturado = 4,
    Enviado = 5,
    Entregue = 6
}

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    public OrderStatus Status { get; set; } = OrderStatus.Criado;

    public decimal Total { get; set; }

    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    // Snapshot dos dados no momento da compra.
    // Assim, alterar o produto depois não altera o pedido antigo.
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }

    public virtual Order? Order { get; set; }
}
