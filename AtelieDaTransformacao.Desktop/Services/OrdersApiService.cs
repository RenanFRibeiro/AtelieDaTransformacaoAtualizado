using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services;

public sealed class OrdersApiService
{
    public Task<List<OrderListItemDto>?> GetAllAsync(
        CancellationToken ct = default)
        => HttpClientHelper.GetAsync<List<OrderListItemDto>>(
            "orders",
            ct);

    public Task<OrderListItemDto?> UpdateStatusAsync(
        int id,
        OrderStatus status,
        CancellationToken ct = default)
        => HttpClientHelper.PutAsync<OrderListItemDto>(
            $"orders/{id}/status",
            new UpdateOrderStatusRequest
            {
                Status = status
            },
            ct);

    private sealed class UpdateOrderStatusRequest
    {
        public OrderStatus Status { get; set; }
    }
}