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

    // ============================================================
    // PEDIDOS DO USUÁRIO
    // ============================================================

    public async Task<IReadOnlyList<OrderListDto>> GetByUserIdAsync(
        string userId)
    {
        var orders = await _repository.GetByUserIdAsync(userId);

        return orders
            .Select(ToListDto)
            .ToList();
    }

    // ============================================================
    // TODOS OS PEDIDOS
    // ============================================================

    public async Task<IReadOnlyList<OrderListDto>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();

        return orders
            .Select(ToListDto)
            .ToList();
    }

    // ============================================================
    // PEDIDO DO USUÁRIO POR ID
    // ============================================================

    public async Task<OrderDetailsDto?> GetByIdForUserAsync(
        int id,
        string userId)
    {
        var order = await _repository.GetByIdForUserAsync(
            id,
            userId);

        if (order == null)
            return null;

        return ToDetailsDto(order);
    }

    // ============================================================
    // PEDIDO POR ID
    // ============================================================

    public async Task<OrderDetailsDto?> GetByIdAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            return null;

        return ToDetailsDto(order);
    }

    // ============================================================
    // CONVERTE ORDER -> ORDER LIST DTO
    // ============================================================

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

    // ============================================================
    // CONVERTE ORDER -> ORDER DETAILS DTO
    // ============================================================

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

    // ============================================================
    // DESSERIALIZA OS ITENS DO PEDIDO
    // ============================================================

    private static List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>
        DeserializeItems(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<
                AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>();
        }

        try
        {
            var items =
                JsonSerializer.Deserialize<
                    List<AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>
                >(json);

            return items
                ?? new List<
                    AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>();
        }
        catch (JsonException)
        {
            return new List<
                AtelieDaTransformacao.Domain.Entities.OrderItemSnapshot>();
        }
    }
}