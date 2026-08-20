using System.Text.Json;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize]
public sealed class CartController : Controller
{
    private const string CartSessionKey = "AtelieDaTransformacao.Cart";
    private readonly IProductService _productService;
    private readonly IWhatsAppService _whatsAppService;

    public CartController(IProductService productService, IWhatsAppService whatsAppService)
    {
        _productService = productService;
        _whatsAppService = whatsAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cart = GetCart();
        var validItems = new List<CartItemViewModel>();

        foreach (var item in cart)
        {
            var product = await _productService.GetByIdAsync(item.ProductId);
            if (product is null || !product.IsAvailable || product.StockQuantity <= 0)
                continue;

            item.Title = product.Title;
            item.Image = product.Image;
            item.Price = product.Price;
            item.Quantity = Math.Min(Math.Max(item.Quantity, 1), product.StockQuantity);
            validItems.Add(item);
        }

        SaveCart(validItems);

        var model = new CartViewModel { Items = validItems };
        ViewBag.WhatsAppLink = _whatsAppService.GenerateCartLink(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int id, int quantity = 1)
    {
        quantity = Math.Max(quantity, 1);
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            TempData["ErrorMessage"] = "Produto não encontrado.";
            return RedirectToAction("Index", "Home");
        }

        if (!product.IsAvailable || product.StockQuantity <= 0)
        {
            TempData["ErrorMessage"] = "Este produto está indisponível ou sem estoque.";
            return RedirectToAction("Index", "Home");
        }

        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == id);

        if (item is null)
        {
            cart.Add(new CartItemViewModel
            {
                ProductId = product.Id,
                Title = product.Title,
                Image = product.Image,
                Price = product.Price,
                Quantity = Math.Min(quantity, product.StockQuantity)
            });
        }
        else
        {
            item.Quantity = Math.Min(item.Quantity + quantity, product.StockQuantity);
            item.Title = product.Title;
            item.Image = product.Image;
            item.Price = product.Price;
        }

        SaveCart(cart);
        TempData["SuccessMessage"] = "Produto adicionado ao carrinho!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var cart = GetCart();
        cart.RemoveAll(x => x.ProductId == id);
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Increase(int id)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item is null) return RedirectToAction(nameof(Index));

        var product = await _productService.GetByIdAsync(id);
        if (product is null || !product.IsAvailable || product.StockQuantity <= 0)
        {
            cart.Remove(item);
            SaveCart(cart);
            TempData["ErrorMessage"] = "Este produto não está mais disponível.";
            return RedirectToAction(nameof(Index));
        }

        if (item.Quantity < product.StockQuantity)
            item.Quantity++;
        else
            TempData["ErrorMessage"] = "Você já atingiu o limite disponível em estoque.";

        item.Title = product.Title;
        item.Image = product.Image;
        item.Price = product.Price;
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Decrease(int id)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item is null) return RedirectToAction(nameof(Index));

        item.Quantity--;
        if (item.Quantity <= 0) cart.Remove(item);
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        SaveCart(new List<CartItemViewModel>());
        TempData["SuccessMessage"] = "Carrinho esvaziado.";
        return RedirectToAction(nameof(Index));
    }

    private List<CartItemViewModel> GetCart()
    {
        var json = HttpContext.Session.GetString(CartSessionKey);
        if (string.IsNullOrWhiteSpace(json)) return new List<CartItemViewModel>();

        try
        {
            return JsonSerializer.Deserialize<List<CartItemViewModel>>(json) ?? new List<CartItemViewModel>();
        }
        catch (JsonException)
        {
            return new List<CartItemViewModel>();
        }
    }

    private void SaveCart(List<CartItemViewModel> cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
    }
}
