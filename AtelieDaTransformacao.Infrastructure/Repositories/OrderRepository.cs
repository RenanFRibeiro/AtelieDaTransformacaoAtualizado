using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly AtelieDaTransformacaoDbContext _context;

    public OrderRepository(AtelieDaTransformacaoDbContext context)
    {
        _context = context;
    }

    public Task<Order?> GetByIdAsync(int id)
        => _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public Task<Order?> GetByIdForUserAsync(int id, string userId)
        => _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

    public async Task<IReadOnlyList<Order>> GetByUserIdAsync(string userId)
        => await _context.Orders.AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyList<Order>> GetAllAsync()
        => await _context.Orders.AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyList<Order>> GetForAutomationAsync()
        => await _context.Orders
            .Where(x => x.AutoAdvance && x.Status != OrderStatus.Entregue)
            .OrderBy(x => x.StatusChangedAt)
            .ToListAsync();

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateStatusAsync(int id, OrderStatus status, bool? autoAdvance = null)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order is null) return false;

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;
        order.StatusChangedAt = DateTime.UtcNow;
        if (autoAdvance.HasValue)
            order.AutoAdvance = autoAdvance.Value;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SetAutoAdvanceAsync(int id, bool enabled)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        if (order is null) return false;

        order.AutoAdvance = enabled;
        order.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}
