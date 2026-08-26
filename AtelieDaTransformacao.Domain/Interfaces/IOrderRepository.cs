using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);

    Task<Order?> GetByIdForUserAsync(
        int id,
        string userId);

    Task<IReadOnlyList<Order>> GetByUserIdAsync(
        string userId);

    Task<IReadOnlyList<Order>> GetAllAsync();

    Task<IReadOnlyList<Order>> GetForAutomationAsync();

    Task AddAsync(Order order);

    Task<bool> UpdateStatusAsync(
        int id,
        OrderStatus status,
        bool? autoAdvance = null);

    Task<bool> SetAutoAdvanceAsync(
        int id,
        bool enabled);

    Task<bool> CancelAsync(int id);
}