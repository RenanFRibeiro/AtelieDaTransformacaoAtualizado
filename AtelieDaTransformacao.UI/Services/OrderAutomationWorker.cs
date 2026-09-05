using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.UI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace AtelieDaTransformacao.UI.Services;

public sealed class OrderAutomationWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmailService _emailService;
    private readonly OrderAutomationOptions _options;
    private readonly ILogger<OrderAutomationWorker> _logger;

    public OrderAutomationWorker(
        IServiceProvider serviceProvider,
        IEmailService emailService,
        IOptions<OrderAutomationOptions> options,
        ILogger<OrderAutomationWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _emailService = emailService;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = TimeSpan.FromSeconds(Math.Clamp(_options.IntervalSeconds, 10, 3600));
        var minimumAge = TimeSpan.FromMinutes(Math.Max(0, _options.MinimumStatusAgeMinutes));

        // Evita uma transição imediata logo após a criação do pedido.
        await Task.Delay(interval, stoppingToken);

        using var timer = new PeriodicTimer(interval);

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await AdvanceEligibleOrdersAsync(minimumAge, stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Encerramento normal da aplicação.
        }
    }

    private async Task AdvanceEligibleOrdersAsync(TimeSpan minimumAge, CancellationToken stoppingToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var hub = scope.ServiceProvider.GetRequiredService<IHubContext<OrderStatusHub>>();
            var cutoff = DateTime.UtcNow - minimumAge;

            var orders = await repository.GetForAutomationAsync();

            foreach (var order in orders)
            {
                stoppingToken.ThrowIfCancellationRequested();

                if (order.StatusChangedAt > cutoff)
                    continue;

                var next = order.Status.GetNext();
                if (!next.HasValue)
                    continue;

                // Atualização condicional: se outra instância já avançou o pedido,
                // nenhuma segunda transição será executada.
                var changed = await repository.TryAdvanceAutomaticAsync(
                    order.Id, order.Status, next.Value);

                if (!changed)
                    continue;

                var now = DateTime.UtcNow;
                order.Status = next.Value;
                order.UpdatedAt = now;
                order.StatusChangedAt = now;

                try
                {
                    await _emailService.SendOrderStatusAsync(order, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Não foi possível enviar a atualização por e-mail do pedido {OrderNumber}.",
                        order.OrderNumber);
                }

                try
                {
                    await hub.Clients
                        .Group(OrderStatusHub.GroupName(order.UserId))
                        .SendAsync(
                            "StatusUpdated",
                            new
                            {
                                orderId = order.Id,
                                orderNumber = order.OrderNumber,
                                status = (int)order.Status,
                                statusName = order.Status.ToDisplayName(),
                                message = order.Status switch
                                {
                                    OrderStatus.Entregue => $"O pedido {order.OrderNumber} foi entregue. Você já pode avaliar o produto.",
                                    OrderStatus.Cancelado => $"O pedido {order.OrderNumber} foi cancelado.",
                                    _ => $"O pedido {order.OrderNumber} foi atualizado para {order.Status.ToDisplayName()}."
                                },
                                autoAdvance = true,
                                updatedAt = now.ToString("O")
                            },
                            stoppingToken);

                    await hub.Clients
                        .Group(OrderStatusHub.AdminGroupName)
                        .SendAsync(
                            "AdminOrderStatusUpdated",
                            new
                            {
                                orderId = order.Id,
                                orderNumber = order.OrderNumber,
                                status = (int)order.Status,
                                statusName = order.Status.ToDisplayName(),
                                isHistory = order.Status is OrderStatus.Enviado
                                    or OrderStatus.Entregue
                                    or OrderStatus.Cancelado,
                                updatedAt = now.ToString("O")
                            },
                            stoppingToken);
                }
                catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogWarning(
                        ex,
                        "Não foi possível enviar a notificação em tempo real do pedido {OrderNumber}.",
                        order.OrderNumber);
                }

            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro na automação dos pedidos.");
        }
    }
}
