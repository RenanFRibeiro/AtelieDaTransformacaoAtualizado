using System.Security.Claims;
using System.Text.Json;
using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize]
public sealed class FeedbackController : Controller
{
    private const long MaxImageBytes =
        5 * 1024 * 1024;

    private static readonly string[] AllowedExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

    private readonly AtelieDaTransformacaoDbContext _context;

    private readonly IFeedbackRepository
        _feedbackRepository;

    private readonly IWebHostEnvironment
        _environment;

    public FeedbackController(
        AtelieDaTransformacaoDbContext context,
        IFeedbackRepository feedbackRepository,
        IWebHostEnvironment environment)
    {
        _context = context;
        _feedbackRepository = feedbackRepository;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        int pedidoId,
        int produtoId)
    {
        var userId = CurrentUserId();

        var eligible =
            await GetEligiblePurchaseAsync(
                userId,
                pedidoId,
                produtoId);

        if (eligible is null)
        {
            TempData["ErrorMessage"] =
                "Este produto não pode ser avaliado. " +
                "A avaliação exige uma compra do produto exato " +
                "em um pedido entregue.";

            return RedirectToAction(
                "Index",
                "Order");
        }

        var existing =
            await _feedbackRepository
                .GetByUserOrderProductAsync(
                    userId,
                    pedidoId,
                    produtoId);

        if (existing is not null)
        {
            TempData["ErrorMessage"] =
                "Este produto já foi avaliado neste pedido.";

            return RedirectToAction(
                "Index",
                "Order");
        }

        return View(
            new FeedbackFormViewModel
            {
                PedidoId = pedidoId,

                ProdutoId = produtoId,

                PedidoNumero =
                    eligible.Value.OrderNumber,

                ProdutoNome =
                    eligible.Value.Product.Title,

                ProdutoImagem =
                    eligible.Value.Product.Image
            });
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        FeedbackFormViewModel model)
    {
        var userId = CurrentUserId();

        var eligible =
            await GetEligiblePurchaseAsync(
                userId,
                model.PedidoId,
                model.ProdutoId);

        if (eligible is null)
        {
            TempData["ErrorMessage"] =
                "A avaliação foi recusada porque o produto informado " +
                "não pertence a um pedido entregue da sua conta.";

            return RedirectToAction(
                "Index",
                "Order");
        }

        model.PedidoNumero =
            eligible.Value.OrderNumber;

        model.ProdutoNome =
            eligible.Value.Product.Title;

        model.ProdutoImagem =
            eligible.Value.Product.Image;

        var existing =
            await _feedbackRepository
                .GetByUserOrderProductAsync(
                    userId,
                    model.PedidoId,
                    model.ProdutoId);

        if (existing is not null)
        {
            TempData["ErrorMessage"] =
                "Este produto já foi avaliado neste pedido.";

            return RedirectToAction(
                "Index",
                "Order");
        }

        if (model.Imagem is not null)
        {
            ValidateImage(model.Imagem);

            if (!ModelState.IsValid)
                return View(model);
        }

        if (!ModelState.IsValid)
            return View(model);

        string? imageUrl = null;

        try
        {
            if (model.Imagem is not null &&
                model.Imagem.Length > 0)
            {
                imageUrl =
                    await SaveImageAsync(
                        model.Imagem);
            }

            var feedback = new Feedback
            {
                UsuarioId = userId,

                ProdutoId =
                    model.ProdutoId,

                PedidoId =
                    model.PedidoId,

                Nota =
                    model.Nota,

                Comentario =
                    model.Comentario.Trim(),

                ImagemUrl =
                    imageUrl,

                IsAnonimo =
                    model.IsAnonimo,

                DataCriacao =
                    DateTime.UtcNow
            };

            await _feedbackRepository
                .AddAsync(feedback);

            TempData["SuccessMessage"] =
                "Obrigado! Sua avaliação foi enviada para análise. Ela ficará visível no site após a aprovação da nossa equipe.";

            return RedirectToAction(
                "Index",
                "Order");
        }
        catch (InvalidOperationException ex)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
                DeleteImage(imageUrl);

            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            return View(model);
        }
        catch
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
                DeleteImage(imageUrl);

            ModelState.AddModelError(
                string.Empty,
                "Não foi possível salvar sua avaliação. " +
                "Tente novamente.");

            return View(model);
        }
    }

    private async Task<
        (string OrderNumber, Product Product)?>
        GetEligiblePurchaseAsync(
            string userId,
            int pedidoId,
            int produtoId)
    {
        if (pedidoId <= 0 ||
            produtoId <= 0)
        {
            return null;
        }

        var order =
            await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == pedidoId &&
                        x.UserId == userId);

        if (order is null)
            return null;

        if (order.Status !=
            OrderStatus.Entregue)
        {
            return null;
        }

        var items =
            DeserializeItems(
                order.ItemsJson);

        var purchasedExactProduct =
            items.Any(
                x =>
                    x.ProductId == produtoId &&
                    x.Quantity > 0);

        if (!purchasedExactProduct)
            return null;

        var product =
            await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == produtoId);

        if (product is null)
            return null;

        return (
            order.OrderNumber,
            product);
    }

    private void ValidateImage(
        IFormFile image)
    {
        if (image.Length <= 0)
        {
            ModelState.AddModelError(
                nameof(
                    FeedbackFormViewModel.Imagem),
                "A imagem selecionada está vazia.");

            return;
        }

        if (image.Length >
            MaxImageBytes)
        {
            ModelState.AddModelError(
                nameof(
                    FeedbackFormViewModel.Imagem),
                "A imagem deve ter no máximo 5 MB.");

            return;
        }

        var extension =
            Path.GetExtension(
                image.FileName)
                .ToLowerInvariant();

        if (!AllowedExtensions.Contains(
                extension))
        {
            ModelState.AddModelError(
                nameof(
                    FeedbackFormViewModel.Imagem),
                "Envie uma imagem JPG, JPEG, PNG ou WEBP.");

            return;
        }

        var contentType =
            image.ContentType
                .ToLowerInvariant();

        if (contentType is not "image/jpeg"
            and not "image/png"
            and not "image/webp")
        {
            ModelState.AddModelError(
                nameof(
                    FeedbackFormViewModel.Imagem),
                "O arquivo enviado não possui " +
                "um formato de imagem permitido.");

            return;
        }

        if (!HasValidImageSignature(image))
        {
            ModelState.AddModelError(
                nameof(
                    FeedbackFormViewModel.Imagem),
                "O arquivo enviado não parece " +
                "ser uma imagem válida.");
        }
    }

    private static bool HasValidImageSignature(
        IFormFile image)
    {
        using var stream =
            image.OpenReadStream();

        Span<byte> header =
            stackalloc byte[12];

        var read =
            stream.Read(header);

        if (read >= 3 &&
            header[0] == 0xFF &&
            header[1] == 0xD8 &&
            header[2] == 0xFF)
        {
            return true;
        }

        if (read >= 8 &&
            header[0] == 0x89 &&
            header[1] == 0x50 &&
            header[2] == 0x4E &&
            header[3] == 0x47 &&
            header[4] == 0x0D &&
            header[5] == 0x0A &&
            header[6] == 0x1A &&
            header[7] == 0x0A)
        {
            return true;
        }

        return read >= 12 &&
               header[0] == 0x52 &&
               header[1] == 0x49 &&
               header[2] == 0x46 &&
               header[3] == 0x46 &&
               header[8] == 0x57 &&
               header[9] == 0x45 &&
               header[10] == 0x42 &&
               header[11] == 0x50;
    }

    private async Task<string> SaveImageAsync(
        IFormFile image)
    {
        var extension =
            Path.GetExtension(
                image.FileName)
                .ToLowerInvariant();

        var uploadsPath =
            Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "feedbacks");

        Directory.CreateDirectory(
            uploadsPath);

        var fileName =
            $"{Guid.NewGuid():N}{extension}";

        var physicalPath =
            Path.Combine(
                uploadsPath,
                fileName);

        await using var stream =
            new FileStream(
                physicalPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                64 * 1024,
                useAsync: true);

        await image.CopyToAsync(
            stream);

        return
            $"/uploads/feedbacks/{fileName}";
    }

    private void DeleteImage(
        string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(
                imageUrl))
        {
            return;
        }

        var relativePath =
            imageUrl
                .TrimStart('/')
                .Replace(
                    '/',
                    Path.DirectorySeparatorChar);

        var physicalPath =
            Path.Combine(
                _environment.WebRootPath,
                relativePath);

        if (System.IO.File.Exists(
                physicalPath))
        {
            System.IO.File.Delete(
                physicalPath);
        }
    }

    private string CurrentUserId()
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(
                userId))
        {
            throw new InvalidOperationException(
                "Usuário autenticado não encontrado.");
        }

        return userId;
    }

    private static List<OrderItemSnapshot>
        DeserializeItems(
            string? json)
    {
        if (string.IsNullOrWhiteSpace(
                json))
        {
            return new List<OrderItemSnapshot>();
        }

        try
        {
            return
                JsonSerializer.Deserialize<
                    List<OrderItemSnapshot>>(
                    json)
                ?? new List<OrderItemSnapshot>();
        }
        catch (JsonException)
        {
            return new List<OrderItemSnapshot>();
        }
    }
}