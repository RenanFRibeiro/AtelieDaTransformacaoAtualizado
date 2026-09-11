using System.Text.Json;

using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly AtelieDaTransformacaoDbContext _context;

    public OrderRepository(
        AtelieDaTransformacaoDbContext context)
    {
        _context = context;
    }

    public Task<Order?> GetByIdAsync(int id)
    {
        return _context.Orders
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Order?> GetByIdForUserAsync(
        int id,
        string userId)
    {
        return _context.Orders
            .FirstOrDefaultAsync(
                x =>
                    x.Id == id &&
                    x.UserId == userId);
    }

    public async Task<IReadOnlyList<Order>>
        GetByUserIdAsync(
            string userId)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>>
        GetActiveForUserAsync(string userId)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                x.Status != OrderStatus.Enviado &&
                x.Status != OrderStatus.Entregue &&
                x.Status != OrderStatus.Cancelado)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>>
        GetHistoryForUserAsync(
            string userId,
            OrderStatus? status = null,
            string? keyword = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
    {
        var query = _context.Orders
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                (x.Status == OrderStatus.Enviado ||
                 x.Status == OrderStatus.Entregue ||
                 x.Status == OrderStatus.Cancelado));

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var term = keyword.Trim();
            query = query.Where(x =>
                x.OrderNumber.Contains(term) ||
                (x.CustomerName ?? string.Empty).Contains(term) ||
                (x.UserEmail ?? string.Empty).Contains(term));
        }

        if (startDate.HasValue)
        {
            var start = startDate.Value.Date.ToUniversalTime();
            query = query.Where(x => x.StatusChangedAt >= start);
        }

        if (endDate.HasValue)
        {
            var end = endDate.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();
            query = query.Where(x => x.StatusChangedAt <= end);
        }

        return await query
            .OrderByDescending(x => x.StatusChangedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>>
        GetAllAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>>
        GetActiveAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(x =>
                x.Status != OrderStatus.Enviado &&
                x.Status != OrderStatus.Entregue &&
                x.Status != OrderStatus.Cancelado)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>>
        GetHistoryAsync(
            OrderStatus? status = null,
            string? client = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
    {
        var query = _context.Orders
            .AsNoTracking()
            .Where(x =>
                x.Status == OrderStatus.Enviado ||
                x.Status == OrderStatus.Entregue ||
                x.Status == OrderStatus.Cancelado);

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(client))
        {
            var term = client.Trim();
            query = query.Where(x =>
                (x.UserEmail ?? string.Empty).Contains(term) ||
                (x.CustomerName ?? string.Empty).Contains(term) ||
                x.OrderNumber.Contains(term));
        }

        if (startDate.HasValue)
        {
            var start = startDate.Value.Date.ToUniversalTime();
            query = query.Where(x => x.StatusChangedAt >= start);
        }

        if (endDate.HasValue)
        {
            var end = endDate.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();
            query = query.Where(x => x.StatusChangedAt <= end);
        }

        return await query
            .OrderByDescending(x => x.StatusChangedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Order>>
        GetForAutomationAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(
                x =>
                    x.AutoAdvance &&
                    x.Status != OrderStatus.Entregue &&
                    x.Status != OrderStatus.Cancelado)
            .OrderBy(x => x.StatusChangedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateStatusAsync(
        int id,
        OrderStatus status,
        bool? autoAdvance = null)
    {
        var order =
            await _context.Orders
                .FirstOrDefaultAsync(
                    x => x.Id == id);

        if (order == null)
            return false;

        if (order.Status == OrderStatus.Cancelado)
            return false;

        order.Status = status;

        order.UpdatedAt =
            DateTime.UtcNow;

        order.StatusChangedAt =
            DateTime.UtcNow;

        if (autoAdvance.HasValue)
        {
            order.AutoAdvance =
                autoAdvance.Value;
        }

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> TryAdvanceAutomaticAsync(
        int id,
        OrderStatus expectedStatus,
        OrderStatus nextStatus)
    {
        if (!Enum.IsDefined(expectedStatus) || !Enum.IsDefined(nextStatus))
            return false;

        var now = DateTime.UtcNow;

        var affected = await _context.Orders
            .Where(x =>
                x.Id == id &&
                x.AutoAdvance &&
                x.Status == expectedStatus &&
                x.Status != OrderStatus.Cancelado &&
                x.Status != OrderStatus.Entregue)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Status, nextStatus)
                .SetProperty(x => x.UpdatedAt, now)
                .SetProperty(x => x.StatusChangedAt, now));

        return affected == 1;
    }

    public async Task<bool> SetAutoAdvanceAsync(
        int id,
        bool enabled)
    {
        var order =
            await _context.Orders
                .FirstOrDefaultAsync(
                    x => x.Id == id);

        if (order == null)
            return false;

        if (order.Status == OrderStatus.Cancelado)
            return false;

        order.AutoAdvance = enabled;

        order.UpdatedAt =
            DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelAsync(int id)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                System.Data.IsolationLevel.Serializable);

        try
        {
            var order =
                await _context.Orders
                    .FirstOrDefaultAsync(
                        x => x.Id == id);

            if (order == null)
                return false;

            if (!order.Status.CanCancel())
                return false;

            var items =
                DeserializeItems(
                    order.ItemsJson);

            if (items.Count > 0)
            {
                var productIds =
                    items
                        .Select(x => x.ProductId)
                        .Distinct()
                        .ToList();

                var products =
                    await _context.Products
                        .Where(x =>
                            productIds.Contains(x.Id))
                        .ToDictionaryAsync(
                            x => x.Id);

                foreach (var item in items)
                {
                    if (products.TryGetValue(
                            item.ProductId,
                            out var product))
                    {
                        product.StockQuantity +=
                            item.Quantity;
                    }
                }
            }

            order.Status =
                OrderStatus.Cancelado;

            order.AutoAdvance =
                false;

            order.UpdatedAt =
                DateTime.UtcNow;

            order.StatusChangedAt =
                DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    private static List<OrderItemSnapshot>
        DeserializeItems(
            string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return new List<OrderItemSnapshot>();

        try
        {
            return JsonSerializer.Deserialize<
                       List<OrderItemSnapshot>
                   >(json)
                   ?? new List<OrderItemSnapshot>();
        }
        catch
        {
            return new List<OrderItemSnapshot>();
        }
    }
}