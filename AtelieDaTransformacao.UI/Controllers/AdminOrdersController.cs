using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.UI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize(Roles = "Admin")]
public sealed class AdminOrdersController : Controller
{
    private readonly AtelieDaTransformacaoDbContext _db;
    private readonly IHubContext<OrderHub> _hub;

    public AdminOrdersController(
        AtelieDaTransformacaoDbContext db,
        IHubContext<OrderHub> hub)
    {
        _db = db;
        _hub = hub;
    }

    [HttpGet]
    public async Task<IActionResult> Index(OrderStatus? status)
    {
        var query = _db.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAt)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value)
                         .OrderByDescending(x => x.CreatedAt);

        var orders = await query.ToListAsync();

        ViewBag.SelectedStatus = status;

        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var order = await _db.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order is null)
            return NotFound();

        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(
        int id,
        OrderStatus status)
    {
        var order = await _db.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order is null)
        {
            TempData["ErrorMessage"] = "Pedido não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        if (!Enum.IsDefined(status))
        {
            TempData["ErrorMessage"] = "Status inválido.";
            return RedirectToAction(nameof(Details), new { id });
        }

        order.Status = status;
        order.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        await NotifyCustomerAsync(order);

        TempData["SuccessMessage"] =
            $"Pedido #{order.Id} atualizado para \"{status.Label()}\".";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Advance(int id)
    {
        var order = await _db.Orders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order is null)
        {
            TempData["ErrorMessage"] = "Pedido não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        if (order.Status == OrderStatus.Entregue)
        {
            TempData["ErrorMessage"] =
                "Este pedido já está na etapa final.";
            return RedirectToAction(nameof(Details), new { id });
        }

        order.Status = (OrderStatus)((int)order.Status + 1);
        order.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        await NotifyCustomerAsync(order);

        TempData["SuccessMessage"] =
            $"Fluxo avançado automaticamente para \"{order.Status.Label()}\".";

        return RedirectToAction(nameof(Details), new { id });
    }

    private Task NotifyCustomerAsync(Order order) =>
        _hub.Clients.User(order.UserId).SendAsync(
            "OrderStatusUpdated",
            order.Id,
            (int)order.Status,
            order.Status.Label(),
            order.Status.Icon(),
            order.UpdatedAt.ToString("O"));
}
