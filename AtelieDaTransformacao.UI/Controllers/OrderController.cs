using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Infrastructure.Context;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize]
public sealed class OrderController : Controller
{
    private const string CartSessionKey = "AtelieDaTransformacao.Cart";

    private readonly AtelieDaTransformacaoDbContext _db;
    private readonly IWhatsAppService _whatsAppService;

    public OrderController(
        AtelieDaTransformacaoDbContext db,
        IWhatsAppService whatsAppService)
    {
        _db = db;
        _whatsAppService = whatsAppService;
    }

    // =========================================================
    // COMPRAR AGORA
    // =========================================================

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> BuyNow(int id, int quantity = 1)
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            TempData["RegistrationReason"] =
                "Para comprar diretamente pelo site e acompanhar o pedido em tempo real, você precisa criar uma conta.";

            var returnUrl = Url.Action(
                nameof(BuyNow),
                "Order",
                new
                {
                    id,
                    quantity
                });

            return RedirectToAction(
                "Register",
                "Account",
                new
                {
                    returnUrl
                });
        }

        var product = await _db.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            return NotFound();

        if (product.StockQuantity <= 0)
        {
            TempData["ErrorMessage"] =
                "Esta peça está sem estoque.";

            return RedirectToAction(
                "ProductDetails",
                "Home",
                new
                {
                    id
                });
        }

        quantity = Math.Clamp(
            quantity,
            1,
            product.StockQuantity);

        var model = new CheckoutViewModel
        {
            DirectProductId = product.Id,

            DirectQuantity = quantity,

            Items = new List<CheckoutItemViewModel>
            {
                new CheckoutItemViewModel
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Image = product.Image,
                    UnitPrice = product.Price,
                    Quantity = quantity
                }
            }
        };

        ViewBag.WhatsAppLink =
            _whatsAppService.GenerateProductInquiryLink(
                product.Title,
                product.Price);

        return View(
            "Checkout",
            model);
    }

    // =========================================================
    // CHECKOUT
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var model = await BuildCartCheckoutAsync();

        if (model == null)
        {
            TempData["ErrorMessage"] =
                "Seu carrinho está vazio.";

            return RedirectToAction(
                "Index",
                "Cart");
        }

        ViewBag.WhatsAppLink =
            BuildWhatsAppLink(model);

        return View(model);
    }

    // =========================================================
    // FINALIZAR PEDIDO
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(
        CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await RefreshCheckoutItemsAsync(model);

            ViewBag.WhatsAppLink =
                BuildWhatsAppLink(model);

            return View(model);
        }

        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Challenge();

        var requestedItems =
            await GetRequestedItemsAsync(model);

        if (requestedItems.Count == 0)
        {
            ModelState.AddModelError(
                string.Empty,
                "Não há produtos para finalizar.");

            await RefreshCheckoutItemsAsync(model);

            ViewBag.WhatsAppLink =
                BuildWhatsAppLink(model);

            return View(model);
        }

        await using var transaction =
            await _db.Database.BeginTransactionAsync();

        try
        {
            var productIds =
                requestedItems
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList();

            var products =
                await _db.Products
                    .Where(x =>
                        productIds.Contains(x.Id))
                    .ToDictionaryAsync(
                        x => x.Id);

            var userEmail =
                User.Identity?.Name ?? string.Empty;

            var order = new Order
            {
                UserId = userId,

                Status = OrderStatus.Criado,

                CustomerName =
                    model.CustomerName?.Trim()
                    ?? string.Empty,

                CustomerEmail = userEmail,

                CustomerPhone =
                    model.CustomerPhone?.Trim()
                    ?? string.Empty,

                ShippingAddress =
                    model.ShippingAddress?.Trim()
                    ?? string.Empty,

                PaymentMethod =
                    model.PaymentMethod?.Trim()
                    ?? string.Empty,

                Notes =
                    model.Notes?.Trim()
                    ?? string.Empty,

                CreatedAt = DateTime.UtcNow,

                UpdatedAt = DateTime.UtcNow,

                Items = new List<OrderItem>()
            };

            decimal total = 0m;

            foreach (var requested in requestedItems)
            {
                if (!products.TryGetValue(
                        requested.ProductId,
                        out var product))
                {
                    throw new InvalidOperationException(
                        $"A peça #{requested.ProductId} não está mais disponível.");
                }

                if (product.StockQuantity <
                    requested.Quantity)
                {
                    throw new InvalidOperationException(
                        $"O estoque de \"{product.Title}\" mudou. Disponível: {product.StockQuantity}.");
                }

                var subtotal =
                    product.Price *
                    requested.Quantity;

                order.Items.Add(
                    new OrderItem
                    {
                        ProductId = product.Id,

                        ProductTitle =
                            product.Title,

                        ProductImage =
                            product.Image,

                        UnitPrice =
                            product.Price,

                        Quantity =
                            requested.Quantity,

                        Subtotal =
                            subtotal
                    });

                product.StockQuantity -=
                    requested.Quantity;

                total += subtotal;
            }

            order.Total = total;

            _db.Orders.Add(order);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            ClearCart();

            TempData["SuccessMessage"] =
                "Pedido criado com sucesso!";

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = order.Id
                });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            await RefreshCheckoutItemsAsync(model);

            ViewBag.WhatsAppLink =
                BuildWhatsAppLink(model);

            return View(model);
        }
    }

    // =========================================================
    // MEUS PEDIDOS
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> MyOrders()
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Challenge();

        var orders =
            await _db.Orders
                .AsNoTracking()
                .Where(x =>
                    x.UserId == userId)
                .OrderByDescending(
                    x => x.CreatedAt)
                .Select(x =>
                    new OrderListItemViewModel
                    {
                        Id = x.Id,

                        Status = x.Status,

                        Total = x.Total,

                        ItemsCount =
                            x.Items.Count,

                        CreatedAt =
                            x.CreatedAt
                    })
                .ToListAsync();

        return View(orders);
    }

    // =========================================================
    // DETALHES
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Details(
        int id)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Challenge();

        var order =
            await _db.Orders
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == id &&
                        x.UserId == userId);

        if (order == null)
            return NotFound();

        return View(
            MapDetails(order));
    }

    // =========================================================
    // STATUS
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Status(
        int id)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Challenge();

        var order =
            await _db.Orders
                .AsNoTracking()
                .Where(x =>
                    x.Id == id &&
                    x.UserId == userId)
                .Select(x => new
                {
                    x.Id,

                    Status =
                        (int)x.Status,

                    StatusLabel =
                        x.Status.ToString(),

                    x.UpdatedAt
                })
                .FirstOrDefaultAsync();

        if (order == null)
            return NotFound();

        return Json(order);
    }

    // =========================================================
    // CONSTRUIR CHECKOUT DO CARRINHO
    // =========================================================

    private async Task<CheckoutViewModel?>
        BuildCartCheckoutAsync()
    {
        var cart = GetCart();

        if (cart.Count == 0)
            return null;

        var ids =
            cart
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

        var products =
            await _db.Products
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToDictionaryAsync(
                    x => x.Id);

        var items =
            new List<CheckoutItemViewModel>();

        foreach (var cartItem in cart)
        {
            if (!products.TryGetValue(
                    cartItem.ProductId,
                    out var product))
            {
                continue;
            }

            if (product.StockQuantity <= 0)
                continue;

            var quantity =
                Math.Clamp(
                    cartItem.Quantity,
                    1,
                    product.StockQuantity);

            items.Add(
                new CheckoutItemViewModel
                {
                    ProductId =
                        product.Id,

                    Title =
                        product.Title,

                    Image =
                        product.Image,

                    UnitPrice =
                        product.Price,

                    Quantity =
                        quantity
                });
        }

        if (items.Count == 0)
            return null;

        return new CheckoutViewModel
        {
            Items = items
        };
    }

    // =========================================================
    // ITENS SOLICITADOS
    // =========================================================

    private Task<List<CheckoutItemViewModel>>
        GetRequestedItemsAsync(
            CheckoutViewModel model)
    {
        if (model.DirectProductId.HasValue)
        {
            return Task.FromResult(
                new List<CheckoutItemViewModel>
                {
                    new CheckoutItemViewModel
                    {
                        ProductId =
                            model.DirectProductId.Value,

                        Quantity =
                            Math.Max(
                                model.DirectQuantity,
                                1)
                    }
                });
        }

        var items =
            GetCart()
                .Where(x => x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g =>
                    new CheckoutItemViewModel
                    {
                        ProductId =
                            g.Key,

                        Quantity =
                            g.Sum(x =>
                                x.Quantity)
                    })
                .ToList();

        return Task.FromResult(items);
    }

    // =========================================================
    // ATUALIZAR CHECKOUT
    // =========================================================

    private async Task RefreshCheckoutItemsAsync(
        CheckoutViewModel model)
    {
        var requested =
            await GetRequestedItemsAsync(model);

        if (requested.Count == 0)
        {
            model.Items = new();

            return;
        }

        var ids =
            requested
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

        var products =
            await _db.Products
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToDictionaryAsync(
                    x => x.Id);

        model.Items =
            requested
                .Where(x =>
                    products.ContainsKey(
                        x.ProductId))
                .Select(x =>
                {
                    var p =
                        products[x.ProductId];

                    return new CheckoutItemViewModel
                    {
                        ProductId =
                            p.Id,

                        Title =
                            p.Title,

                        Image =
                            p.Image,

                        UnitPrice =
                            p.Price,

                        Quantity =
                            Math.Max(
                                1,
                                Math.Min(
                                    x.Quantity,
                                    Math.Max(
                                        p.StockQuantity,
                                        1)))
                    };
                })
                .ToList();
    }

    // =========================================================
    // CARRINHO
    // =========================================================

    private List<CartItemViewModel> GetCart()
    {
        var json =
            HttpContext.Session.GetString(
                CartSessionKey);

        if (string.IsNullOrWhiteSpace(json))
            return new();

        try
        {
            return JsonSerializer
                .Deserialize<
                    List<CartItemViewModel>>(
                    json)
                ?? new();
        }
        catch (JsonException)
        {
            return new();
        }
    }

    private void ClearCart()
    {
        HttpContext.Session.Remove(
            CartSessionKey);
    }

    // =========================================================
    // WHATSAPP
    // =========================================================

    private string BuildWhatsAppLink(
        CheckoutViewModel model)
    {
        if (model.Items == null ||
            model.Items.Count == 0)
        {
            return string.Empty;
        }

        if (model.Items.Count == 1)
        {
            var item =
                model.Items[0];

            return _whatsAppService
                .GenerateProductInquiryLink(
                    item.Title,
                    item.UnitPrice);
        }

        var cart =
            new CartViewModel
            {
                Items =
                    model.Items
                        .Select(x =>
                            new CartItemViewModel
                            {
                                ProductId =
                                    x.ProductId,

                                Title =
                                    x.Title,

                                Image =
                                    x.Image,

                                Price =
                                    x.UnitPrice,

                                Quantity =
                                    x.Quantity
                            })
                        .ToList()
            };

        return _whatsAppService
            .GenerateCartLink(cart);
    }

    // =========================================================
    // MAPEAMENTO
    // =========================================================

    private static OrderDetailsViewModel
        MapDetails(Order order)
    {
        return new OrderDetailsViewModel
        {
            Id =
                order.Id,

            Status =
                order.Status,

            Total =
                order.Total,

            CustomerName =
                order.CustomerName,

            CustomerEmail =
                order.CustomerEmail,

            CustomerPhone =
                order.CustomerPhone,

            ShippingAddress =
                order.ShippingAddress,

            PaymentMethod =
                order.PaymentMethod,

            Notes =
                order.Notes,

            CreatedAt =
                order.CreatedAt,

            UpdatedAt =
                order.UpdatedAt,

            Items =
                order.Items
                    .Select(x =>
                        new OrderItemViewModel
                        {
                            ProductId =
                                x.ProductId,

                            ProductTitle =
                                x.ProductTitle,

                            ProductImage =
                                x.ProductImage,

                            UnitPrice =
                                x.UnitPrice,

                            Quantity =
                                x.Quantity,

                            Subtotal =
                                x.Subtotal
                        })
                    .ToList()
        };
    }
}