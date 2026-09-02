using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<OrderListDto>>
        GetByUserIdAsync(string userId);

    Task<IReadOnlyList<OrderListDto>>
        GetAllAsync();

    Task<IReadOnlyList<OrderListDto>>
        GetActiveAsync();

    Task<IReadOnlyList<OrderListDto>>
        GetHistoryAsync(
            OrderStatus? status = null,
            string? client = null,
            DateTime? startDate = null,
            DateTime? endDate = null);

    Task<OrderDetailsDto?>
        GetByIdForUserAsync(
            int id,
            string userId);

    Task<OrderDetailsDto?>
        GetByIdAsync(int id);

    Task<bool>
        CancelAsync(
            int id,
            string userId);
}