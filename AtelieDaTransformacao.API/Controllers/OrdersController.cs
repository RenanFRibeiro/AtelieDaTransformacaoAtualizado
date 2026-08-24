using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public sealed class OrdersController : ControllerBase
{
    private readonly AtelieDaTransformacaoDbContext _db;

    public OrdersController(AtelieDaTransformacaoDbContext db)
        => _db = db;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderListItemResponse>>> GetAll(CancellationToken ct)
    {
        var orders = await _db.Orders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new OrderListItemResponse
            {
                Id = x.Id,
                Number = $"#{x.Id:000000}",
                Date = x.CreatedAt,
                Customer = x.CustomerName,
                Total = x.Total,
                Status = x.Status,
                LastUpdate = x.UpdatedAt
            })
            .ToListAsync(ct);

        return Ok(orders);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<OrderListItemResponse>> UpdateStatus(
        int id,
        UpdateOrderStatusRequest request,
        CancellationToken ct)
    {
        if (!Enum.IsDefined(request.Status))
            return BadRequest(new { message = "Status inválido." });

        var order = await _db.Orders.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (order is null)
            return NotFound(new { message = "Pedido não encontrado." });

        order.Status = request.Status;
        order.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        return Ok(new OrderListItemResponse
        {
            Id = order.Id,
            Number = $"#{order.Id:000000}",
            Date = order.CreatedAt,
            Customer = order.CustomerName,
            Total = order.Total,
            Status = order.Status,
            LastUpdate = order.UpdatedAt
        });
    }

    public sealed class UpdateOrderStatusRequest
    {
        public OrderStatus Status { get; set; }
    }

    public sealed class OrderListItemResponse
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Customer { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
