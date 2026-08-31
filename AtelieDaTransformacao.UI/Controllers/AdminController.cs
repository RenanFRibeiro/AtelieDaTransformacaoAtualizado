using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminController(
        IProductService productService,
        IProductCategoryService categoryService,
        IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment; // Injetado para pegar o caminho do wwwroot
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var categories = (await _categoryService.GetAllAsync())
            .Where(c => !IsResinCategory(c.Name))
            .ToList();

        var viewModel = new ProductFormViewModel
        {
            Categories = categories ?? new List<ProductCategoryDto>(),
            IsAvailable = true,
            IsFeatured = false,
            StockQuantity = 1
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductFormViewModel model, IFormFile? uploadImage)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = (await _categoryService.GetAllAsync())
                .Where(c => !IsResinCategory(c.Name))
                .ToList();
            return View(model);
        }

        try
        {
            // Pega a URL de cobertura se foi preenchida
            string finalImage = string.IsNullOrWhiteSpace(model.Image)
                ? model.CoverImageUrl
                : model.Image;

            // Se o usuário também anexou um arquivo, salvamos e sobreescrevemos o caminho
            if (uploadImage != null && uploadImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Cria um nome único usando Guid para evitar duplicidades
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadImage.CopyToAsync(fileStream);
                }

                // Define que o caminho persistido no banco será a rota interna da imagem
                finalImage = "/uploads/" + uniqueFileName;
            }

            var product = new CreateProductDto
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Image = finalImage, // Salva URL ou a imagem importada
                CategoryId = model.CategoryId,
                IsFeatured = model.IsFeatured,
                StockQuantity = model.IsAvailable ? model.StockQuantity : 0
            };

            await _productService.AddAsync(product);

            TempData["SuccessMessage"] = "Peça cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.Categories = (await _categoryService.GetAllAsync())
                .Where(c => !IsResinCategory(c.Name))
                .ToList();
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        var categories = (await _categoryService.GetAllAsync())
            .Where(c => !IsResinCategory(c.Name))
            .ToList();

        var viewModel = new ProductFormViewModel
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            CoverImageUrl = product.Image,
            Image = product.Image,
            CategoryId = product.CategoryId,
            Price = product.Price,
            IsFeatured = product.IsFeatured,
            IsAvailable = product.IsAvailable,
            StockQuantity = product.StockQuantity,
            Categories = categories ?? new List<ProductCategoryDto>()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = (await _categoryService.GetAllAsync())
                .Where(c => !IsResinCategory(c.Name))
                .ToList();
            return View(model);
        }

        try
        {
            var dto = new UpdateProductDto
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Image = string.IsNullOrWhiteSpace(model.Image) ? model.CoverImageUrl : model.Image,
                CategoryId = model.CategoryId,
                IsFeatured = model.IsFeatured,
                StockQuantity = model.IsAvailable ? model.StockQuantity : 0
            };

            await _productService.UpdateAsync(model.Id, dto);

            TempData["SuccessMessage"] = "Peça atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.Categories = (await _categoryService.GetAllAsync())
                .Where(c => !IsResinCategory(c.Name))
                .ToList();
            return View(model);
        }
    }

    // Nota: Removi o "EditProducts" que estava duplicado na versão anterior, deixando apenas "EditProduct" que é o correto!

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(new CreateProductCategoryDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
    {
        if (IsResinCategory(model.Name))
        {
            ModelState.AddModelError(nameof(model.Name), "Essa categoria não está disponível.");
        }

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            await _categoryService.AddAsync(model);

            TempData["SuccessMessage"] = "Categoria criada com sucesso!";
            return RedirectToAction(nameof(Categories));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Categories()
    {
        var categories = (await _categoryService.GetAllAsync())
            .Where(c => !IsResinCategory(c.Name))
            .ToList();
        return View(categories);
    }

    private static bool IsResinCategory(string? categoryName) =>
        !string.IsNullOrWhiteSpace(categoryName) &&
        categoryName.Contains("resina", StringComparison.OrdinalIgnoreCase);

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var deleted = await _categoryService.DeleteAsync(id);

            if (!deleted)
            {
                TempData["ErrorMessage"] = "Categoria não encontrada.";
            }
            else
            {
                TempData["SuccessMessage"] = "Categoria removida com sucesso!";
            }
        }
        catch
        {
            TempData["ErrorMessage"] = "Não é possível apagar esta categoria porque existem produtos associados a ela.";
        }

        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var deleted = await _productService.DeleteAsync(id);

            if (!deleted)
            {
                TempData["ErrorMessage"] = "Peça não encontrada.";
            }
            else
            {
                TempData["SuccessMessage"] = "Peça removida com sucesso!";
            }
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Não foi possível remover esta peça.";
        }

        return RedirectToAction(nameof(Index));
    }
}