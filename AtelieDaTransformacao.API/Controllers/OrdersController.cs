using System.Security.Claims;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class OrdersController : ControllerBase
{
    private readonly AtelieDaTransformacaoDbContext _db;
    private readonly IOrderRepository _orders;

    public OrdersController(
        AtelieDaTransformacaoDbContext db,
        IOrderRepository orders)
    {
        _db = db;
        _orders = orders;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderListItemResponse>>> GetAll(
        CancellationToken ct)
    {
        var query = _db.Orders.AsNoTracking();

        // Administradores podem acompanhar todos os pedidos.
        // Usuários comuns devem receber somente os pedidos pertencentes
        // à própria conta autenticada. A filtragem acontece na API.
        if (!User.IsInRole("Admin"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { message = "Usuário autenticado sem identificador." });

            query = query.Where(x => x.UserId == userId);
        }

        var orders = await query
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new OrderListItemResponse
            {
                Id = x.Id,
                Number = string.IsNullOrWhiteSpace(x.OrderNumber)
                    ? $"#{x.Id:000000}"
                    : x.OrderNumber,

                Date = x.CreatedAt,

                Customer = string.IsNullOrWhiteSpace(x.CustomerName) ? x.UserEmail ?? string.Empty : x.CustomerName,

                Total = x.Total,

                Status = x.Status,

                LastUpdate = x.UpdatedAt
            })
            .ToListAsync(ct);

        return Ok(orders);
    }

    [HttpPut("{id:int}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderListItemResponse>> UpdateStatus(
        int id,
        UpdateOrderStatusRequest request,
        CancellationToken ct)
    {
        if (!Enum.IsDefined(request.Status))
        {
            return BadRequest(new
            {
                message = "Status inválido."
            });
        }

        var order =
            await _db.Orders
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    ct);

        if (order is null)
        {
            return NotFound(new
            {
                message = "Pedido não encontrado."
            });
        }

        if (!order.Status.CanTransitionTo(request.Status))
        {
            return Conflict(new
            {
                message = $"Não é permitido alterar o pedido de {order.Status.ToDisplayName()} para {request.Status.ToDisplayName()}."
            });
        }

        bool changed;
        if (request.Status == OrderStatus.Cancelado)
        {
            changed = await _orders.CancelAsync(id);
        }
        else
        {
            order.Status = request.Status;
            order.UpdatedAt = DateTime.UtcNow;
            order.StatusChangedAt = DateTime.UtcNow;
            changed = await _db.SaveChangesAsync(ct) > 0;
        }

        if (!changed)
            return Conflict(new { message = "Não foi possível atualizar o pedido." });

        order = await _db.Orders.FirstAsync(x => x.Id == id, ct);

        return Ok(
            new OrderListItemResponse
            {
                Id = order.Id,

                Number =
                    string.IsNullOrWhiteSpace(order.OrderNumber)
                        ? $"#{order.Id:000000}"
                        : order.OrderNumber,

                Date =
                    order.CreatedAt,

                Customer =
                    string.IsNullOrWhiteSpace(order.CustomerName) ? order.UserEmail ?? string.Empty : order.CustomerName,

                Total =
                    order.Total,

                Status =
                    order.Status,

                LastUpdate =
                    order.UpdatedAt
            });
    }

    public sealed class UpdateOrderStatusRequest
    {
        public OrderStatus Status { get; set; }
    }

    public sealed class OrderListItemResponse
    {
        public int Id { get; set; }

        public string Number { get; set; } =
            string.Empty;

        public DateTime Date { get; set; }

        public string Customer { get; set; } =
            string.Empty;

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}