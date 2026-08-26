using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<OrderListDto>>
        GetByUserIdAsync(string userId);

    Task<IReadOnlyList<OrderListDto>>
        GetAllAsync();

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