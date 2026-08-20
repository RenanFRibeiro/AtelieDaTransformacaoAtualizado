using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.UI.Controllers;

/// <summary>
/// Controller principal da loja, responsável por exibir
/// a vitrine de produtos para os clientes.
/// </summary>
public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _categoryService;

    /// <summary>
    /// Injeta os serviços necessários para produtos e categorias.
    /// </summary>
    public HomeController(
        IProductService productService,
        IProductCategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Exibe a página inicial com todos os produtos e categorias,
    /// permitindo filtrar os produtos por categoria.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        var viewModel = new HomeViewModel
        {
            Categories = await _categoryService.GetAllAsync(),
            SelectedCategoryId = categoryId
        };

        if (categoryId.HasValue)
        {
            viewModel.Products =
                await _productService.GetByCategoryAsync(categoryId.Value);
        }
        else
        {
            viewModel.Products =
                await _productService.GetAllAsync();
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            viewModel.Products = viewModel.Products.Where(p =>
                p.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                p.CategoryName.Contains(term, StringComparison.OrdinalIgnoreCase));
            ViewBag.Search = term;
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ContactSeller(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        if (User.Identity?.IsAuthenticated != true)
        {
            TempData["RegistrationReason"] =
                "Para entrar em contato com o vendedor e iniciar a compra desta peça, você precisa criar uma conta. Assim, seu atendimento fica vinculado ao seu perfil.";

            var returnUrl = Url.Action(nameof(ContactSeller), "Home", new { id });

            return RedirectToAction("Register", "Account", new { returnUrl });
        }

        if (string.IsNullOrWhiteSpace(product.WhatsAppLink))
        {
            TempData["ErrorMessage"] =
                "O contato do vendedor não está disponível no momento.";

            return RedirectToAction(nameof(ProductDetails), new { id });
        }

        return Redirect(product.WhatsAppLink);
    }

    [HttpGet]
    public IActionResult About() => View();

    [HttpGet]
    public IActionResult Gallery() => View();

    /// <summary>
    /// Exibe os detalhes de um produto específico.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ProductDetails(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}