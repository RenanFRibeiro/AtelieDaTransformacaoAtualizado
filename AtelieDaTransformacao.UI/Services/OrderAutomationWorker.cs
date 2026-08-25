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
    {
        var orders = await _repository.GetByUserIdAsync(userId);

        return orders
            .Select(ToListDto)
            .ToList();
    }

    public async Task<IReadOnlyList<OrderListDto>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();

        return orders
            .Select(ToListDto)
            .ToList();
    }

    public async Task<OrderDetailsDto?> GetByIdForUserAsync(
        int id,
        string userId)
    {
        var order = await _repository.GetByIdForUserAsync(id, userId);

        if (order == null)
            return null;

        return ToDetailsDto(order);
    }

    public async Task<OrderDetailsDto?> GetByIdAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            return null;

        return ToDetailsDto(order);
    }

    private static OrderListDto ToListDto(Order order)
    {
        return new OrderListDto
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
    }

    private static OrderDetailsDto ToDetailsDto(Order order)
    {
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
            Items = DeserializeItems(order.ItemsJson)
        };
    }

    private static List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>
        DeserializeItems(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>();
        }

        try
        {
            return JsonSerializer.Deserialize<
                List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>
            >(json)
            ?? new List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>();
        }
        catch (JsonException)
        {
            return new List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>();
        }
    }
}