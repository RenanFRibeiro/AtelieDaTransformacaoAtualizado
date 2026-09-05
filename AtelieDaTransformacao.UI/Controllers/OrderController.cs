using System.Data;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.UI.Hubs;
using AtelieDaTransformacao.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize]
public sealed class OrderController : Controller
{
    private const string CartSessionKey = "AtelieDaTransformacao.Cart";
    private static readonly HashSet<string> AllowedPaymentMethods = new(StringComparer.OrdinalIgnoreCase)
    {
        "Pix",
        "Cartão de crédito",
        "Cartão de débito",
        "Transferência bancária",
        "A combinar"
    };

    private readonly AtelieDaTransformacaoDbContext _context;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    private readonly IHubContext<OrderStatusHub> _hub;
    private readonly IEmailService _emailService;
    private readonly IFreteService _freteService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(
    AtelieDaTransformacaoDbContext context,
    IOrderService orderService,
    IOrderRepository orderRepository,
    IHubContext<OrderStatusHub> hub,
    IEmailService emailService,
    IFreteService freteService,
    ILogger<OrderController> logger)
    {
        _context = context;
        _orderService = orderService;
        _orderRepository = orderRepository;
        _hub = hub;
        _emailService = emailService;
        _freteService = freteService;
        _logger = logger;
    }

    // =========================================================
    // MEUS PEDIDOS
    // GET: /Order
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = CurrentUserId();

        var orders =
            await _orderService.GetActiveForUserAsync(userId);

        return View(orders);
    }

    // =========================================================
    // HISTÓRICO DOS PEDIDOS FINALIZADOS
    // GET: /Order/History
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> History(
        OrderStatus? status = null,
        string? keyword = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var userId = CurrentUserId();

        var orders = await _orderService.GetHistoryForUserAsync(
            userId, status, keyword, startDate, endDate);

        ViewBag.Status = status;
        ViewBag.Keyword = keyword;
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

        return View(orders);
    }

    // =========================================================
    // DETALHES DO PEDIDO
    // GET: /Order/Details/5
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var userId = CurrentUserId();

        var order =
            await _orderService.GetByIdForUserAsync(
                id,
                userId);

        if (order is null)
            return NotFound();

        return View(order);
    }

    // =========================================================
    // STATUS DO PEDIDO
    // GET: /Order/Status/5
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Notifications(DateTime? since = null)
    {
        var userId = CurrentUserId();
        var minimum = DateTime.UtcNow.AddMinutes(-2);
        var sinceUtc = since?.ToUniversalTime() ?? minimum;

        if (sinceUtc < DateTime.UtcNow.AddHours(-24))
            sinceUtc = DateTime.UtcNow.AddHours(-24);

        var orders = await _context.Orders
            .AsNoTracking()
            .Where(x =>
                x.UserId == userId &&
                x.StatusChangedAt > sinceUtc)
            .OrderBy(x => x.StatusChangedAt)
            .Take(50)
            .Select(x => new
            {
                orderId = x.Id,
                orderNumber = x.OrderNumber,
                status = (int)x.Status,
                autoAdvance = x.AutoAdvance,
                updatedAt = x.StatusChangedAt
            })
            .ToListAsync();

        return Json(orders.Select(x => new
        {
            x.orderId,
            x.orderNumber,
            x.status,
            statusName = ((OrderStatus)x.status).ToDisplayName(),
            x.autoAdvance,
            x.updatedAt
        }));
    }

    [HttpGet]
    public async Task<IActionResult> Status(int id)
    {
        var userId = CurrentUserId();

        var order =
            await _orderService.GetByIdForUserAsync(
                id,
                userId);

        if (order is null)
            return NotFound();

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

    // =========================================================
    // CHECKOUT - EXIBE DADOS, ENTREGA E PAGAMENTO
    // GET: /Order/Checkout
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Checkout(int? directProductId = null, int quantity = 1)
    {
        var cart = await GetCheckoutCartAsync(directProductId, quantity);
        if (cart.Count == 0)
        {
            TempData["ErrorMessage"] = "Seu carrinho está vazio ou o produto não está disponível.";
            return RedirectToAction("Index", "Cart");
        }

        var model = BuildCheckoutViewModel(cart, directProductId, quantity);
        return View(model);
    }

    // =========================================================
    // CONFIRMA A COMPRA APÓS DADOS E PAGAMENTO
    // POST: /Order/Checkout
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        // O e-mail exibido no checkout corresponde sempre à conta autenticada.
        ModelState.Remove(nameof(model.CustomerEmail));
        ModelState.Remove(nameof(model.ShippingCost));
        ModelState.Remove(nameof(model.ShippingEstimateDays));

        model.CustomerEmail = User.FindFirstValue(ClaimTypes.Email)
            ?? User.Identity?.Name
            ?? string.Empty;

        NormalizeCheckoutModel(model);

        var cart = await GetCheckoutCartAsync(model.DirectProductId, model.DirectQuantity);
        if (cart.Count == 0)
        {
            TempData["ErrorMessage"] = "Seu carrinho está vazio ou o produto não está mais disponível.";
            return RedirectToAction("Index", "Cart");
        }

        model.Items = ToCheckoutItems(cart);

        await ValidateAndCalculateCheckoutAsync(model);

        if (!ModelState.IsValid)
            return View(model);

        var order = await CreateOrderAsync(cart, model);
        if (order is null)
            return View(model);

        try
        {
            await _emailService.SendOrderStatusAsync(order);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Não foi possível enviar a confirmação por e-mail do pedido {OrderNumber}.", order.OrderNumber);
        }

        if (!model.DirectProductId.HasValue)
            ClearCart();

        TempData["SuccessMessage"] = $"Pedido {order.OrderNumber} criado com sucesso.";
        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

    // =========================================================
    // COMPRAR AGORA - VAI PARA O CHECKOUT, NÃO CRIA O PEDIDO
    // POST: /Order/BuyNow
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuyNow(int id, int quantity = 1)
    {
        quantity = Math.Max(1, quantity);

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
            return NotFound();

        if (!product.IsAvailable || product.StockQuantity < quantity)
        {
            TempData["ErrorMessage"] = "A quantidade solicitada não está disponível em estoque.";
            return RedirectToAction("ProductDetails", "Home", new { id });
        }

        // Importante: a compra só é criada depois que o cliente informar
        // os dados, a entrega e a forma de pagamento na tela de checkout.
        return RedirectToAction(nameof(Checkout), new
        {
            directProductId = id,
            quantity
        });
    }

    // =========================================================
    // CANCELAR PEDIDO
    //
    // POST: /Order/Cancel
    // =========================================================

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Challenge();

        var order =
            await _orderService.GetByIdForUserAsync(
                id,
                userId);

        if (order == null)
            return NotFound();

        if (!order.Status.CanCancel())
        {
            TempData["ErrorMessage"] =
                "Este pedido não pode mais ser cancelado.";

            return RedirectToAction(
                nameof(Details),
                new { id });
        }

        var cancelled =
            await _orderService.CancelAsync(
                id,
                userId);

        if (!cancelled)
        {
            TempData["ErrorMessage"] =
                "Não foi possível cancelar o pedido.";

            return RedirectToAction(
                nameof(Details),
                new { id });
        }

        TempData["SuccessMessage"] =
            $"Pedido {order.OrderNumber} cancelado com sucesso.";

        var cancelledOrderEntity = await _orderRepository.GetByIdAsync(id);
        if (cancelledOrderEntity != null)
        {
            try
            {
                await _emailService.SendOrderStatusAsync(cancelledOrderEntity);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Não foi possível enviar a atualização por e-mail do pedido {OrderNumber}.", cancelledOrderEntity.OrderNumber);
            }

            try
            {
                await BroadcastAdminStatusAsync(cancelledOrderEntity);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Não foi possível enviar a notificação administrativa do pedido {OrderNumber}.", cancelledOrderEntity.OrderNumber);
            }
        }

        return RedirectToAction(
            nameof(Details),
            new { id });
    }

    // =========================================================
    // CRIAÇÃO DO PEDIDO
    // =========================================================

    private async Task<Order?> CreateOrderAsync(
        List<CartItemViewModel> cart,
        CheckoutViewModel checkout)
    {
        if (cart is null ||
            cart.Count == 0)
        {
            TempData["ErrorMessage"] =
                "Seu carrinho está vazio.";

            return null;
        }

        var userId =
            CurrentUserId();

        var email = checkout.CustomerEmail.Trim();
        var customerName = checkout.CustomerName.Trim();
        var customerPhone = checkout.CustomerPhone.Trim();

        var utcNow =
            DateTime.UtcNow;

        await using var transaction =
            await _context.Database
                .BeginTransactionAsync(
                    IsolationLevel.Serializable);

        try
        {
            var productIds =
                cart
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList();

            var products =
                await _context.Products
                    .Where(
                        x => productIds.Contains(x.Id))
                    .ToDictionaryAsync(
                        x => x.Id);

            var snapshots =
                new List<OrderItemSnapshot>();

            foreach (var item in cart)
            {
                if (!products.TryGetValue(
                        item.ProductId,
                        out var product))
                {
                    TempData["ErrorMessage"] =
                        "Um dos produtos do carrinho não existe mais.";

                    await transaction.RollbackAsync();

                    return null;
                }

                if (item.Quantity <= 0)
                {
                    TempData["ErrorMessage"] =
                        "A quantidade de um dos produtos é inválida.";

                    await transaction.RollbackAsync();

                    return null;
                }

                if (product.StockQuantity < item.Quantity)
                {
                    TempData["ErrorMessage"] =
                        $"O estoque de '{product.Title}' não é suficiente.";

                    await transaction.RollbackAsync();

                    return null;
                }

                product.StockQuantity -=
                    item.Quantity;

                snapshots.Add(
                    new OrderItemSnapshot
                    {
                        ProductId =
                            product.Id,

                        Title =
                            product.Title,

                        UnitPrice =
                            product.Price,

                        Quantity =
                            item.Quantity
                    });
            }

            var order =
                new Order
                {
                    OrderNumber =
                        GenerateOrderNumber(),

                    UserId =
                        userId,

                    UserEmail =
                        email,

                    CustomerName =
                        customerName,

                    CustomerEmail =
                        email,

                    CustomerPhone =
                        customerPhone,

                    ShippingAddress =
                        FormatShippingAddress(checkout),

                    PaymentMethod =
                        checkout.PaymentMethod.Trim(),

                    Notes =
                        checkout.Notes?.Trim() ?? string.Empty,

                    ShippingCost =
                        checkout.ShippingCost,

                    CheckoutJson =
                        JsonSerializer.Serialize(new OrderCheckoutSnapshot
                        {
                            CustomerName = customerName,
                            CustomerEmail = email,
                            CustomerPhone = customerPhone,
                            PostalCode = checkout.PostalCode,
                            ShippingAddress = checkout.ShippingAddress,
                            AddressNumber = checkout.AddressNumber,
                            Complement = string.Empty,
                            District = checkout.District,
                            City = checkout.City,
                            State = checkout.State,
                            DeliveryMethod = checkout.DeliveryMethod,
                            SelectedFreight = checkout.SelectedFreight ?? string.Empty,
                            ShippingEstimateDays = checkout.ShippingEstimateDays,
                            PaymentMethod = checkout.PaymentMethod,
                            ShippingCost = checkout.ShippingCost,
                            Notes = checkout.Notes ?? string.Empty
                        }),

                    ItemsJson =
                        JsonSerializer.Serialize(
                            snapshots),

                    Total =
                        snapshots.Sum(
                            x => x.Subtotal) + checkout.ShippingCost,

                    Status =
                        OrderStatus.Criado,

                    AutoAdvance =
                        false,

                    CreatedAt =
                        utcNow,

                    UpdatedAt =
                        utcNow,

                    StatusChangedAt =
                        utcNow
                };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            _logger.LogError(ex, "Erro ao criar pedido para o usuário {UserId}.", userId);
            TempData["ErrorMessage"] =
                "Não foi possível finalizar o pedido agora. Seus dados não foram perdidos; revise as informações e tente novamente.";

            return null;
        }
    }

    private async Task<List<CartItemViewModel>> GetCheckoutCartAsync(int? directProductId, int quantity)
    {
        if (directProductId.HasValue)
        {
            quantity = Math.Max(1, quantity);

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == directProductId.Value);

            if (product is null || !product.IsAvailable || product.StockQuantity < quantity)
                return new();

            return new List<CartItemViewModel>
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
        }

        var sessionCart = GetCart();
        if (sessionCart.Count == 0)
            return new();

        var ids = sessionCart.Select(x => x.ProductId).Distinct().ToList();
        var products = await _context.Products
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id);

        var refreshed = new List<CartItemViewModel>();
        foreach (var item in sessionCart)
        {
            if (!products.TryGetValue(item.ProductId, out var product) || !product.IsAvailable)
                continue;

            var safeQuantity = Math.Clamp(item.Quantity, 1, product.StockQuantity);
            refreshed.Add(new CartItemViewModel
            {
                ProductId = product.Id,
                Title = product.Title,
                Image = product.Image,
                Price = product.Price,
                Quantity = safeQuantity
            });
        }

        SaveCart(refreshed);
        return refreshed;
    }

    private CheckoutViewModel BuildCheckoutViewModel(
        List<CartItemViewModel> cart,
        int? directProductId,
        int quantity)
    {
        var firstName = User.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty;
        var lastName = User.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;
        var fullName = string.Join(" ", new[] { firstName, lastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty;

        return new CheckoutViewModel
        {
            DirectProductId = directProductId,
            DirectQuantity = Math.Max(1, quantity),
            Items = ToCheckoutItems(cart),
            CustomerName = fullName,
            CustomerEmail = email,
            CustomerPhone = User.FindFirstValue(ClaimTypes.MobilePhone) ?? string.Empty,
            PostalCode = User.FindFirstValue(ClaimTypes.PostalCode) ?? string.Empty,
            ShippingAddress = User.FindFirstValue(ClaimTypes.StreetAddress) ?? string.Empty,
            AddressNumber = User.FindFirstValue("Atelie:AddressNumber") ?? string.Empty,
            Complement = User.FindFirstValue("Atelie:Complement") ?? string.Empty,
            District = User.FindFirstValue("Atelie:District") ?? string.Empty,
            City = User.FindFirstValue(ClaimTypes.Locality) ?? string.Empty,
            State = User.FindFirstValue(ClaimTypes.StateOrProvince) ?? string.Empty
        };
    }

    private static List<CheckoutItemViewModel> ToCheckoutItems(List<CartItemViewModel> cart) =>
        cart.Select(x => new CheckoutItemViewModel
        {
            ProductId = x.ProductId,
            Title = x.Title,
            Image = x.Image,
            UnitPrice = x.Price,
            Quantity = x.Quantity
        }).ToList();

    private void NormalizeCheckoutModel(CheckoutViewModel model)
    {
        model.CustomerName = model.CustomerName.Trim();
        model.CustomerPhone = model.CustomerPhone.Trim();
        model.PostalCode = model.PostalCode.Trim();
        model.ShippingAddress = model.ShippingAddress.Trim();
        model.AddressNumber = model.AddressNumber.Trim();
        model.Complement = model.Complement.Trim();
        model.District = model.District.Trim();
        model.City = model.City.Trim();
        model.State = model.State.Trim().ToUpperInvariant();
        model.DeliveryMethod = model.DeliveryMethod.Trim();
        model.SelectedFreight = model.SelectedFreight?.Trim();
        model.PaymentMethod = model.PaymentMethod.Trim();
        model.Notes = model.Notes?.Trim();

        if (!AllowedPaymentMethods.Contains(model.PaymentMethod))
            ModelState.AddModelError(nameof(model.PaymentMethod), "Selecione uma forma de pagamento válida.");

        if (string.Equals(model.DeliveryMethod, "Retirada no ateliê", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var field in new[] { "PostalCode", "ShippingAddress", "AddressNumber", "District", "City", "State", "SelectedFreight" })
                ModelState.Remove(field);

            model.PostalCode = string.Empty;
            model.ShippingAddress = "Retirada no ateliê";
            model.AddressNumber = string.Empty;
            model.District = string.Empty;
            model.City = string.Empty;
            model.State = string.Empty;
            model.SelectedFreight = string.Empty;
            model.ShippingCost = 0m;
            model.ShippingEstimateDays = 0;
            return;
        }

        if (!string.Equals(model.DeliveryMethod, "Entrega", StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError(nameof(model.DeliveryMethod), "Selecione uma modalidade de recebimento válida.");

    }

    private async Task ValidateAndCalculateCheckoutAsync(CheckoutViewModel model)
    {
        if (!string.Equals(model.DeliveryMethod, "Entrega", StringComparison.OrdinalIgnoreCase))
        {
            model.ShippingCost = 0m;
            model.ShippingEstimateDays = 0;
            return;
        }

        if (!Regex.IsMatch(model.PostalCode, @"^\d{5}-?\d{3}$"))
        {
            ModelState.AddModelError(nameof(model.PostalCode), "Informe um CEP válido para calcular o frete.");
            return;
        }

        if (string.IsNullOrWhiteSpace(model.SelectedFreight))
        {
            ModelState.AddModelError(nameof(model.SelectedFreight), "Selecione uma opção de frete.");
            return;
        }

        // O valor recebido do navegador nunca é considerado fonte de verdade.
        // O servidor recalcula as opções com os dados atuais do pedido.
        var options = await _freteService.CalcularFreteAsync(new FreteRequestDto
        {
            CepDestino = model.PostalCode,
            CepOrigem = "01000000",
            PesoKg = 1.5m,
            AlturaCm = 25m,
            LarguraCm = 25m,
            ComprimentoCm = 20m
        });

        var selected = options.FirstOrDefault(x =>
            x.Disponivel &&
            string.Equals(x.Nome, model.SelectedFreight, StringComparison.OrdinalIgnoreCase));

        if (selected is null)
        {
            ModelState.AddModelError(nameof(model.SelectedFreight), "A opção de frete selecionada não está disponível. Calcule o frete novamente.");
            model.ShippingCost = 0m;
            model.ShippingEstimateDays = 0;
            return;
        }

        model.SelectedFreight = selected.Nome;
        model.ShippingCost = selected.Valor;
        model.ShippingEstimateDays = selected.PrazoEstimadoDias;
    }

    private static string FormatShippingAddress(CheckoutViewModel checkout)
    {
        if (string.Equals(checkout.DeliveryMethod, "Retirada no ateliê", StringComparison.OrdinalIgnoreCase))
            return "Retirada no ateliê";

        var parts = new[]
        {
            checkout.ShippingAddress,
            checkout.AddressNumber,
            checkout.District,
            checkout.City,
            checkout.State,
            checkout.PostalCode
        }.Where(x => !string.IsNullOrWhiteSpace(x));

        return string.Join(", ", parts);
    }

    private Task BroadcastAdminStatusAsync(Order order)
    {
        return _hub.Clients
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
                    updatedAt = order.UpdatedAt.ToString("O")
                });
    }

    // =========================================================
    // USUÁRIO LOGADO
    // =========================================================

    private string CurrentUserId()
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException(
                "Usuário autenticado sem identificador.");
        }

        return userId;
    }

    // =========================================================
    // NÚMERO DO PEDIDO
    // =========================================================

    private static string GenerateOrderNumber()
    {
        var suffix = Guid.NewGuid().ToString("N")[..10].ToUpperInvariant();
        return $"AT-{DateTime.UtcNow:yyyyMMddHHmmss}-{suffix}";
    }

    // =========================================================
    // RECUPERAR CARRINHO DA SESSION
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
            return JsonSerializer.Deserialize<
                       List<CartItemViewModel>>(json)
                   ?? new();
        }
        catch (JsonException)
        {
            return new();
        }
    }

    // =========================================================
    // SALVAR CARRINHO NA SESSION
    // =========================================================

    private void SaveCart(List<CartItemViewModel> cart)
    {
        var json = JsonSerializer.Serialize(cart);
        HttpContext.Session.SetString(CartSessionKey, json);
    }

    // =========================================================
    // LIMPAR CARRINHO
    // =========================================================

    private void ClearCart()
    {
        HttpContext.Session.Remove(
            CartSessionKey);
    }
}