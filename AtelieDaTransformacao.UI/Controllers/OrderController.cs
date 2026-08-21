using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Application.ViewModels;

public class CheckoutViewModel
{
    public int? DirectProductId { get; set; }

    public int DirectQuantity { get; set; } = 1;

    public List<CheckoutItemViewModel> Items { get; set; } = new();

    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(150)]
    public string CustomerName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Informe um telefone válido.")]
    [StringLength(30)]
    public string CustomerPhone { get; set; } = string.Empty;

    [StringLength(500)]
    public string ShippingAddress { get; set; } = string.Empty;

    [StringLength(100)]
    public string PaymentMethod { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Notes { get; set; } = string.Empty;

    public decimal Total
    {
        get
        {
            decimal total = 0;

            foreach (var item in Items)
            {
                total += item.Subtotal;
            }

            return total;
        }
    }
}

public class CheckoutItemViewModel
{
    public int ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Image { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; } = 1;

    public decimal Subtotal => UnitPrice * Quantity;
}

public class OrderListItemViewModel
{
    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    public decimal Total { get; set; }

    public int ItemsCount { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class OrderDetailsViewModel
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

public class OrderItemViewModel
{
    public int ProductId { get; set; }

    public string ProductTitle { get; set; } = string.Empty;

    public string? ProductImage { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }
}