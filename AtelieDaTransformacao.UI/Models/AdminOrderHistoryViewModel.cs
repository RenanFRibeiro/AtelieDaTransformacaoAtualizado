using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Domain.Enums;

namespace AtelieDaTransformacao.UI.Models;

public sealed class AdminOrderHistoryViewModel
{
    public IReadOnlyList<OrderListDto> Orders { get; init; } = Array.Empty<OrderListDto>();

    public string? Status { get; init; }
    public string? Customer { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }

    public int TotalCount => Orders.Count;
    public int CancelledCount => Orders.Count(x => x.Status == OrderStatus.Cancelado);
    public int ShippedCount => Orders.Count(x => x.Status == OrderStatus.Enviado);
    public int DeliveredCount => Orders.Count(x => x.Status == OrderStatus.Entregue);
    public decimal TotalValue => Orders.Sum(x => x.Total);
}
