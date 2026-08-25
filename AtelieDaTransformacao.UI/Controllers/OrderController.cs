using System.Data;
using System.Security.Claims;
using System.Text.Json;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
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
    private readonly IOrderService _orderService;

    public OrderController(
        AtelieDaTransformacaoDbContext context,
        IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetByUserIdAsync(CurrentUserId());
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

    // Exibe a tela de checkout com os dados do usuário já preenchidos.
    [HttpGet]
    public async Task<IActionResult> Checkout(int? productId, int quantity = 1)
    {
        quantity = Math.Max(1, quantity);

        var model = new CheckoutViewModel
        {
            DirectProductId = productId,
            DirectQuantity = quantity,
            CustomerName = BuildCustomerName(),
            CustomerEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty,
            CustomerPhone = User.FindFirstValue(ClaimTypes.MobilePhone) ?? string.Empty,
            PostalCode = User.FindFirstValue(ClaimTypes.PostalCode) ?? string.Empty,
            ShippingAddress = User.FindFirstValue(ClaimTypes.StreetAddress) ?? string.Empty,
            AddressNumber = User.FindFirstValue("Atelie:AddressNumber") ?? string.Empty,
            Complement = User.FindFirstValue("Atelie:Complement") ?? string.Empty,
            District = User.FindFirstValue("Atelie:District") ?? string.Empty,
            City = User.FindFirstValue(ClaimTypes.Locality) ?? string.Empty,
            State = User.FindFirstValue(ClaimTypes.StateOrProvince) ?? string.Empty,
            DeliveryMethod = "Entrega",
            PaymentMethod = "Pix"
        };

        var items = await ResolveCheckoutItemsAsync(productId, quantity);
        if (items is null || items.Count == 0)
        {
            TempData["ErrorMessage"] = "Não há itens disponíveis para finalizar a compra.";
            return RedirectToAction("Index", "Cart");
        }

        model.Items = items;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        var items = await ResolveCheckoutItemsAsync(model.DirectProductId, model.DirectQuantity);
        model.Items = items ?? new();

        if (model.Items.Count == 0)
        {
            TempData["ErrorMessage"] = "Seu pedido não possui itens disponíveis.";
            return RedirectToAction("Index", "Cart");
        }

        NormalizeCheckout(model);

        var pickup = model.DeliveryMethod.Equals("Retirada no ateliê", StringComparison.OrdinalIgnoreCase);

        if (pickup)
        {
            ModelState.Remove(nameof(model.PostalCode));
            ModelState.Remove(nameof(model.ShippingAddress));
            ModelState.Remove(nameof(model.AddressNumber));
            ModelState.Remove(nameof(model.District));
            ModelState.Remove(nameof(model.City));
            ModelState.Remove(nameof(model.State));
        }
        else if (string.IsNullOrWhiteSpace(model.ShippingAddress) ||
                 string.IsNullOrWhiteSpace(model.AddressNumber) ||
                 string.IsNullOrWhiteSpace(model.District) ||
                 string.IsNullOrWhiteSpace(model.City) ||
                 string.IsNullOrWhiteSpace(model.State) ||
                 string.IsNullOrWhiteSpace(model.PostalCode))
        {
            ModelState.AddModelError(string.Empty, "Preencha o endereço completo para entrega.");
        }

        if (!ModelState.IsValid)
            return View(model);

        var order = await CreateOrderAsync(model);
        if (order is null)
            return View(model);

        if (model.DirectProductId is null)
            ClearCart();

        TempData["SuccessMessage"] = $"Pedido {order.OrderNumber} criado com sucesso.";
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

    // Compatibilidade com links antigos: agora leva ao checkout.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BuyNow(int id, int quantity = 1)
    {
        return RedirectToAction(nameof(Checkout), new { productId = id, quantity = Math.Max(1, quantity) });
    }

    private async Task<List<CheckoutItemViewModel>?> ResolveCheckoutItemsAsync(int? productId, int quantity)
    {
        if (productId.HasValue)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId.Value);
            if (product is null || !product.IsAvailable || product.StockQuantity <= 0) return null;

            var requested = Math.Min(Math.Max(quantity, 1), product.StockQuantity);
            return new List<CheckoutItemViewModel>
            {
                new()
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Image = product.Image,
                    UnitPrice = product.Price,
                    Quantity = requested
                }
            };
        }

        var cart = GetCart();
        if (cart.Count == 0) return new List<CheckoutItemViewModel>();

        var productIds = cart.Select(x => x.ProductId).Distinct().ToList();
        var products = await _context.Products
            .Where(x => productIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id);

        var items = new List<CheckoutItemViewModel>();
        foreach (var item in cart)
        {
            if (!products.TryGetValue(item.ProductId, out var product)) continue;
            if (!product.IsAvailable || product.StockQuantity <= 0) continue;

            items.Add(new CheckoutItemViewModel
            {
                ProductId = product.Id,
                Title = product.Title,
                Image = product.Image,
                UnitPrice = product.Price,
                Quantity = Math.Min(Math.Max(item.Quantity, 1), product.StockQuantity)
            });
        }

        return items;
    }

    private async Task<Order?> CreateOrderAsync(CheckoutViewModel model)
    {
        var userId = CurrentUserId();
        var utcNow = DateTime.UtcNow;

        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        try
        {
            var productIds = model.Items.Select(x => x.ProductId).Distinct().ToList();
            var products = await _context.Products
                .Where(x => productIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id);

            var snapshots = new List<OrderItemSnapshot>();

            foreach (var item in model.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    TempData["ErrorMessage"] = "Um dos produtos não existe mais.";
                    await transaction.RollbackAsync();
                    return null;
                }

                if (!product.IsAvailable || product.StockQuantity < item.Quantity)
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

            var checkoutSnapshot = new OrderCheckoutSnapshot
            {
                CustomerName = model.CustomerName,
                CustomerEmail = model.CustomerEmail,
                CustomerPhone = model.CustomerPhone,
                PostalCode = model.PostalCode,
                ShippingAddress = model.ShippingAddress,
                AddressNumber = model.AddressNumber,
                Complement = model.Complement,
                District = model.District,
                City = model.City,
                State = model.State,
                DeliveryMethod = model.DeliveryMethod,
                PaymentMethod = model.PaymentMethod,
                Notes = model.Notes
            };

            var order = new Order
            {
                OrderNumber = GenerateOrderNumber(),
                UserId = userId,
                UserEmail = model.CustomerEmail,
                ItemsJson = JsonSerializer.Serialize(snapshots),
                CheckoutJson = JsonSerializer.Serialize(checkoutSnapshot),
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

    private void NormalizeCheckout(CheckoutViewModel model)
    {
        model.CustomerName = model.CustomerName.Trim();
        model.CustomerEmail = model.CustomerEmail.Trim().ToLowerInvariant();
        model.CustomerPhone = model.CustomerPhone.Trim();
        model.PostalCode = model.PostalCode.Trim();
        model.ShippingAddress = model.ShippingAddress.Trim();
        model.AddressNumber = model.AddressNumber.Trim();
        model.Complement = model.Complement.Trim();
        model.District = model.District.Trim();
        model.City = model.City.Trim();
        model.State = model.State.Trim().ToUpperInvariant();
        model.DeliveryMethod = model.DeliveryMethod.Trim();
        model.PaymentMethod = model.PaymentMethod.Trim();
        model.Notes = model.Notes.Trim();
    }

    private string BuildCustomerName()
    {
        var first = User.FindFirstValue(ClaimTypes.GivenName);
        var last = User.FindFirstValue(ClaimTypes.Surname);
        return string.Join(" ", new[] { first, last }.Where(x => !string.IsNullOrWhiteSpace(x)));
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
