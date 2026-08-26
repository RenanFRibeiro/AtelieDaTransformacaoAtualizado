using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<OrderListDto>> GetByUserIdAsync(string userId)
        => (await _repository.GetByUserIdAsync(userId)).Select(ToListDto).ToList();

    public async Task<IReadOnlyList<OrderListDto>> GetAllAsync()
        => (await _repository.GetAllAsync()).Select(ToListDto).ToList();

    public async Task<OrderDetailsDto?> GetByIdForUserAsync(int id, string userId)
    {
        var order = await _repository.GetByIdForUserAsync(id, userId);
        return order is null ? null : ToDetailsDto(order);
    }

    public async Task<OrderDetailsDto?> GetByIdAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);
        return order is null ? null : ToDetailsDto(order);
    }

    private static OrderListDto ToListDto(Order order) => new()
    {
        Id = order.Id,
        OrderNumber = order.OrderNumber,
        UserId = order.UserId,
        UserEmail = order.UserEmail,
        Total = order.Total,
        Status = order.Status,
        StatusName = order.Status.ToDisplayName(),
        AutoAdvance = order.AutoAdvance,
        CreatedAt = order.CreatedAt,
        UpdatedAt = order.UpdatedAt,
        StatusChangedAt = order.StatusChangedAt
    };

    private static OrderDetailsDto ToDetailsDto(Order order)
    {
        var checkout = DeserializeCheckout(order.CheckoutJson);

        return new OrderDetailsDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            UserId = order.UserId,
            UserEmail = order.UserEmail,
            Total = order.Total,
            Status = order.Status,
            StatusName = order.Status.ToDisplayName(),
            AutoAdvance = order.AutoAdvance,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            StatusChangedAt = order.StatusChangedAt,
            Items = DeserializeItems(order.ItemsJson),
            CustomerName = checkout.CustomerName,
            CustomerEmail = checkout.CustomerEmail,
            CustomerPhone = checkout.CustomerPhone,
            ShippingAddress = BuildShippingAddress(checkout),
            PaymentMethod = checkout.PaymentMethod,
            DeliveryMethod = checkout.DeliveryMethod,
            Notes = checkout.Notes
        };
    }

    private static List<OrderItemSnapshot> DeserializeItems(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return new();

        try
        {
            return JsonSerializer.Deserialize<List<OrderItemSnapshot>>(json) ?? new();
        }
        catch (JsonException)
        {
            return new();
        }
    }

    private static OrderCheckoutSnapshot DeserializeCheckout(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return new();

        try
        {
            return JsonSerializer.Deserialize<OrderCheckoutSnapshot>(json) ?? new();
        }
        catch (JsonException)
        {
            return new();
        }
    }

    private static string BuildShippingAddress(OrderCheckoutSnapshot snapshot)
    {
        var lines = new List<string>();
        var first = string.Join(", ", new[] { snapshot.ShippingAddress, snapshot.AddressNumber }
            .Where(x => !string.IsNullOrWhiteSpace(x)));

        if (!string.IsNullOrWhiteSpace(first)) lines.Add(first);
        if (!string.IsNullOrWhiteSpace(snapshot.Complement)) lines.Add(snapshot.Complement);
        if (!string.IsNullOrWhiteSpace(snapshot.District)) lines.Add(snapshot.District);

        var city = string.Join(" - ", new[] { snapshot.City, snapshot.State }
            .Where(x => !string.IsNullOrWhiteSpace(x)));

        if (!string.IsNullOrWhiteSpace(city)) lines.Add(city);
        if (!string.IsNullOrWhiteSpace(snapshot.PostalCode)) lines.Add($"CEP {snapshot.PostalCode}");

        return string.Join(" | ", lines);
    }

    public async Task<bool> CancelAsync(
    int id,
    string userId)
    {
        var order =
            await _repository.GetByIdForUserAsync(
                id,
                userId);

        if (order == null)
            return false;

        if (!order.Status.CanCancel())
            return false;

        return await _repository.CancelAsync(id);
    }
}
