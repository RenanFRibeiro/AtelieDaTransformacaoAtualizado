using System.Security.Claims;
using System.Text.Json;
using System.Data;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.UI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize]
public sealed class OrderController : Controller
{
    private const string CartSessionKey = "AtelieDaTransformacao.Cart";
    private readonly AtelieDaTransformacaoDbContext _context;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    private readonly IHubContext<OrderStatusHub> _hub;

    public OrderController(
        AtelieDaTransformacaoDbContext context,
        IOrderRepository orderRepository,
        IOrderService orderService,
        IHubContext<OrderStatusHub> hub)
    {
        _context = context;
        _orderRepository = orderRepository;
        _orderService = orderService;
        _hub = hub;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = CurrentUserId();
        var orders = await _orderService.GetByUserIdAsync(userId);
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetByIdForUserAsync(id, CurrentUserId());
        return order is null ? NotFound() : View(order);
    }

    [HttpGet]
    public async Task<IActionResult> Status(int id)
    {
        var order = await _orderService.GetByIdForUserAsync(id, CurrentUserId());
        if (order is null) return NotFound();

        return Json(new
        {
            orderId = order.Id,
            orderNumber = order.OrderNumber,
            status = (int)order.Status,
            statusName = order.StatusName,
            autoAdvance = order.AutoAdvance,
            updatedAt = order.UpdatedAt.ToString("O")
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout()
    {
        var cart = GetCart();
        if (cart.Count == 0)
        {
            TempData["ErrorMessage"] = "Seu carrinho está vazio.";
            return RedirectToAction("Index", "Cart");
        }

        var order = await CreateOrderAsync(cart);
        if (order is null)
            return RedirectToAction("Index", "Cart");

        ClearCart();
        TempData["SuccessMessage"] = $"Pedido {order.OrderNumber} criado com sucesso.";
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuyNow(int id, int quantity = 1)
    {
        quantity = Math.Max(1, quantity);
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is null) return NotFound();

        if (product.StockQuantity < quantity)
        {
            TempData["ErrorMessage"] = "A quantidade solicitada não está disponível em estoque.";
            return RedirectToAction("ProductDetails", "Home", new { id });
        }

        var cart = new List<CartItemViewModel>
        {
            new()
            {
                ProductId = product.Id,
                Title = product.Title,
                Image = product.Image,
                Price = product.Price,
                Quantity = quantity
            }
        };

        var order = await CreateOrderAsync(cart);
        if (order is null)
            return RedirectToAction("ProductDetails", "Home", new { id });

        TempData["SuccessMessage"] = $"Pedido {order.OrderNumber} criado com sucesso.";
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

    private async Task<Order?> CreateOrderAsync(List<CartItemViewModel> cart)
    {
        var userId = CurrentUserId();
        var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? "cliente";
        var utcNow = DateTime.UtcNow;

        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        try
        {
            var productIds = cart.Select(x => x.ProductId).Distinct().ToList();
            var products = await _context.Products
                .Where(x => productIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);

            var snapshots = new List<OrderItemSnapshot>();

            foreach (var item in cart)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    TempData["ErrorMessage"] = "Um dos produtos do carrinho não existe mais.";
                    await transaction.RollbackAsync();
                    return null;
                }

                if (product.StockQuantity < item.Quantity || product.StockQuantity <= 0)
                {
                    TempData["ErrorMessage"] = $"O estoque de '{product.Title}' não é suficiente para finalizar o pedido.";
                    await transaction.RollbackAsync();
                    return null;
                }

                product.StockQuantity -= item.Quantity;
                snapshots.Add(new OrderItemSnapshot
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    UnitPrice = product.Price,
                    Quantity = item.Quantity
                });
            }

            var order = new Order
            {
                OrderNumber = GenerateOrderNumber(),
                UserId = userId,
                UserEmail = email,
                ItemsJson = JsonSerializer.Serialize(snapshots),
                Total = snapshots.Sum(x => x.Subtotal),
                Status = OrderStatus.Criado,
                AutoAdvance = false,
                CreatedAt = utcNow,
                UpdatedAt = utcNow,
                StatusChangedAt = utcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            TempData["ErrorMessage"] = $"Não foi possível criar o pedido: {ex.Message}";
            return null;
        }
    }

    private string CurrentUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? throw new InvalidOperationException("Usuário autenticado sem identificador.");

    private static string GenerateOrderNumber()
        => $"AT-{DateTime.Now:yyyyMMddHHmmss}-{Random.Shared.Next(10000, 99999)}";

    private List<CartItemViewModel> GetCart()
    {
        var json = HttpContext.Session.GetString(CartSessionKey);
        if (string.IsNullOrWhiteSpace(json)) return new();
        try
        {
            return JsonSerializer.Deserialize<List<CartItemViewModel>>(json) ?? new();
        }
        catch (JsonException)
        {
            return new();
        }
    }

    private void ClearCart() => HttpContext.Session.Remove(CartSessionKey);
}
