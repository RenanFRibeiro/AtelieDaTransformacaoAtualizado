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

    Task<IReadOnlyList<Order>> GetActiveForUserAsync(
        string userId);

    Task<IReadOnlyList<Order>> GetHistoryForUserAsync(
        string userId,
        OrderStatus? status = null,
        string? keyword = null,
        DateTime? startDate = null,
        DateTime? endDate = null);

    Task<IReadOnlyList<Order>> GetAllAsync();

    Task<IReadOnlyList<Order>> GetActiveAsync();

    Task<IReadOnlyList<Order>> GetHistoryAsync(
        OrderStatus? status = null,
        string? client = null,
        DateTime? startDate = null,
        DateTime? endDate = null);

    Task<IReadOnlyList<Order>> GetForAutomationAsync();

    Task AddAsync(Order order);

    Task<bool> UpdateStatusAsync(
        int id,
        OrderStatus status,
        bool? autoAdvance = null);

    Task<bool> TryAdvanceAutomaticAsync(
        int id,
        OrderStatus expectedStatus,
        OrderStatus nextStatus);

    Task<bool> SetAutoAdvanceAsync(
        int id,
        bool enabled);

    Task<bool> CancelAsync(int id);
}