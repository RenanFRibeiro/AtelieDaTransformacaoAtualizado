using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.UI.Hubs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize(Roles = "Admin")]
public sealed class AdminOrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _repository;
    private readonly IHubContext<OrderStatusHub> _hub;

    public AdminOrdersController(
        IOrderService orderService,
        IOrderRepository repository,
        IHubContext<OrderStatusHub> hub)
    {
        _orderService = orderService;
        _repository = repository;
        _hub = hub;
    }

    // =========================================================
    // LISTA DE PEDIDOS
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // O painel principal mostra somente pedidos ainda em processamento.
        // Enviado, Entregue e Cancelado pertencem exclusivamente ao Histórico.
        var orders =
            await _orderService.GetActiveAsync();

        return View(orders);
    }

    // =========================================================
    // HISTÓRICO
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> History(
        OrderStatus? status = null,
        string? client = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var orders = await _orderService.GetHistoryAsync(
            status, client, startDate, endDate);

        ViewBag.Status = status;
        ViewBag.Client = client;
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> ExportHistory(
        OrderStatus? status = null,
        string? client = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var orders = await _orderService.GetHistoryAsync(
            status, client, startDate, endDate);

        static string Csv(string? value)
        {
            var text = value ?? string.Empty;
            return $"\"{text.Replace("\"", "\"\"")}\"";
        }

        var csv = new StringBuilder();
        csv.AppendLine("Pedido;Cliente;E-mail;Data de criação;Data da alteração;Status;Total");

        foreach (var order in orders)
        {
            csv.AppendLine(string.Join(";",
                Csv(order.OrderNumber),
                Csv(order.CustomerName),
                Csv(order.UserEmail),
                Csv(order.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")),
                Csv(order.StatusChangedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")),
                Csv(order.StatusName),
                Csv(order.Total.ToString("N2"))));
        }

        var bytes = Encoding.UTF8.GetPreamble()
            .Concat(Encoding.UTF8.GetBytes(csv.ToString()))
            .ToArray();

        return File(bytes, "text/csv; charset=utf-8",
            $"historico-pedidos-{DateTime.Now:yyyyMMdd-HHmm}.csv");
    }

    // =========================================================
    // DETALHES DO PEDIDO
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Details(
        int id)
    {
        var order =
            await _orderService.GetByIdAsync(id);

        if (order == null)
        {
            TempData["ErrorMessage"] =
                "Pedido não encontrado.";

            return RedirectToAction(
                nameof(Index));
        }

        return View(order);
    }

    // =========================================================
    // ALTERAR STATUS MANUALMENTE
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(
        int id,
        OrderStatus status,
        bool autoAdvance = false)
    {
        var order =
            await _repository.GetByIdAsync(id);

        if (order == null)
        {
            TempData["ErrorMessage"] =
                "Pedido não encontrado.";

            return RedirectToAction(
                nameof(Index));
        }

        var changed = status == OrderStatus.Cancelado
            ? await _repository.CancelAsync(id)
            : await _repository.UpdateStatusAsync(
                id,
                status,
                autoAdvance);

        if (!changed)
        {
            TempData["ErrorMessage"] =
                "Não foi possível atualizar o pedido.";

            return RedirectToAction(
                nameof(Index));
        }

        await NotifyStatusAsync(
            id,
            order.UserId);

        TempData["SuccessMessage"] =
            $"Pedido {order.OrderNumber} atualizado para {status.ToDisplayName()}.";

        return RedirectToAction(
            nameof(Details),
            new
            {
                id
            });
    }

    // =========================================================
    // MANTÉM SetStatus PARA COMPATIBILIDADE
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<IActionResult> SetStatus(
        int id,
        OrderStatus status,
        bool autoAdvance = false)
    {
        return UpdateStatus(
            id,
            status,
            autoAdvance);
    }

    // =========================================================
    // ATIVAR / DESATIVAR AUTOMAÇÃO
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleAutomation(
        int id,
        bool enabled)
    {
        var order =
            await _repository.GetByIdAsync(id);

        if (order == null)
        {
            TempData["ErrorMessage"] =
                "Pedido não encontrado.";

            return RedirectToAction(
                nameof(Index));
        }

        var changed =
            await _repository.SetAutoAdvanceAsync(
                id,
                enabled);

        if (changed)
        {
            await NotifyStatusAsync(
                id,
                order.UserId);

            TempData["SuccessMessage"] =
                enabled
                    ? $"Automação ativada para {order.OrderNumber}."
                    : $"Automação desativada para {order.OrderNumber}.";
        }

        return RedirectToAction(
            nameof(Details),
            new
            {
                id
            });
    }

    // =========================================================
    // AVANÇAR ETAPA
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Advance(
        int id)
    {
        var order =
            await _repository.GetByIdAsync(id);

        if (order == null)
        {
            TempData["ErrorMessage"] =
                "Pedido não encontrado.";

            return RedirectToAction(
                nameof(Index));
        }

        var next =
            order.Status.GetNext();

        if (!next.HasValue)
        {
            TempData["ErrorMessage"] =
                "O pedido já está entregue.";

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id
                });
        }

        var changed =
            await _repository.UpdateStatusAsync(
                id,
                next.Value);

        if (!changed)
        {
            TempData["ErrorMessage"] =
                "Não foi possível avançar o pedido.";

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id
                });
        }

        await NotifyStatusAsync(
            id,
            order.UserId);

        TempData["SuccessMessage"] =
            $"Pedido {order.OrderNumber} avançou para {next.Value.ToDisplayName()}.";

        return RedirectToAction(
            nameof(Details),
            new
            {
                id
            });
    }

    // =========================================================
    // CANCELAR PEDIDO
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var order =
            await _repository.GetByIdAsync(id);

        if (order == null)
        {
            TempData["ErrorMessage"] =
                "Pedido não encontrado.";

            return RedirectToAction(
                nameof(Index));
        }

        var cancelled =
            await _repository.CancelAsync(id);

        if (!cancelled)
        {
            TempData["ErrorMessage"] =
                "Não foi possível cancelar o pedido.";

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id
                });
        }

        await NotifyStatusAsync(
            id,
            order.UserId);

        TempData["SuccessMessage"] =
            $"Pedido {order.OrderNumber} foi cancelado com sucesso.";

        return RedirectToAction(
            nameof(Details),
            new
            {
                id
            });
    }

    // =========================================================
    // SIGNALR
    // =========================================================

    private async Task NotifyStatusAsync(
        int orderId,
        string userId)
    {
        var order =
            await _orderService.GetByIdAsync(
                orderId);

        if (order == null)
            return;

        await _hub
            .Clients
            .Group(
                OrderStatusHub.GroupName(userId))
            .SendAsync(
                "StatusUpdated",
                new
                {
                    orderId = order.Id,
                    orderNumber = order.OrderNumber,
                    status = (int)order.Status,
                    statusName = order.StatusName,
                    autoAdvance = order.AutoAdvance,
                    updatedAt = order.UpdatedAt.ToString("O")
                });

        await _hub.Clients
            .Group(OrderStatusHub.AdminGroupName)
            .SendAsync(
                "AdminOrderStatusUpdated",
                new
                {
                    orderId = order.Id,
                    orderNumber = order.OrderNumber,
                    status = (int)order.Status,
                    statusName = order.StatusName,
                    isHistory = order.Status is OrderStatus.Enviado
                        or OrderStatus.Entregue
                        or OrderStatus.Cancelado,
                    updatedAt = order.UpdatedAt.ToString("O")
                });
    }

    // =========================================================
    // AVANÇAR AUTOMÁTICO - TODOS OS PEDIDOS ELEGÍVEIS
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdvanceAutomatic()
    {
        var orders = await _repository.GetForAutomationAsync();

        var results = new List<int>();

        foreach (var order in orders)
        {
            var next = order.Status.GetNext();

            if (next.HasValue)
            {
                var changed = await _repository.UpdateStatusAsync(
                    order.Id,
                    next.Value);

                if (changed)
                {
                    results.Add(order.Id);
                    await NotifyStatusAsync(order.Id, order.UserId);
                }
            }
        }

        return Json(new
        {
            success = true,
            message = $"{results.Count} pedido(s) avançado(s) com sucesso.",
            advancedCount = results.Count
        });
    }
}