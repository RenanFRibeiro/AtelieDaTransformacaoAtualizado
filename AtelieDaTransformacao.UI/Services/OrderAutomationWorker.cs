using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.UI.Hubs;

using Microsoft.AspNetCore.SignalR;

namespace AtelieDaTransformacao.UI.Services;

public sealed class OrderAutomationWorker
    : BackgroundService
{
    private readonly IEmailService _emailService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderAutomationWorker> _logger;

    public OrderAutomationWorker(
        IServiceProvider serviceProvider,
        ILogger<OrderAutomationWorker> logger,
        IEmailService emailService)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var timer =
            new PeriodicTimer(
                TimeSpan.FromSeconds(30));

        while (
            await timer.WaitForNextTickAsync(
                stoppingToken))
        {
            try
            {
                using var scope =
                    _serviceProvider.CreateScope();

                var repository =
                    scope.ServiceProvider
                        .GetRequiredService<
                            IOrderRepository>();

                var hub =
                    scope.ServiceProvider
                        .GetRequiredService<
                            IHubContext<OrderStatusHub>>();

                var orders =
                    await repository
                        .GetForAutomationAsync();

                foreach (var order in orders)
                {
                    if (
                        order.Status ==
                        OrderStatus.Cancelado)
                    {
                        continue;
                    }

                    var next =
                        order.Status.GetNext();

                    if (!next.HasValue)
                        continue;

                    var changed =
                        await repository
                            .UpdateStatusAsync(
                                order.Id,
                                next.Value);

                    if (!changed)
                        continue;

                    order.Status = next.Value;
                    try
                    {
                        await _emailService.SendOrderStatusAsync(order, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Não foi possível enviar a atualização por e-mail do pedido {OrderNumber}.", order.OrderNumber);
                    }

                    await hub.Clients
                        .Group(
                            OrderStatusHub.GroupName(
                                order.UserId))
                        .SendAsync(
                            "StatusUpdated",
                            new
                            {
                                orderId = order.Id,
                                orderNumber =
                                    order.OrderNumber,
                                status =
                                    (int)next.Value,
                                statusName =
                                    next.Value
                                        .ToDisplayName(),
                                autoAdvance = true,
                                updatedAt =
                                    DateTime.UtcNow
                                        .ToString("O")
                            },
                            stoppingToken);
                }
            }
            catch (
                OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro na automação dos pedidos.");
            }
        }
    }
}