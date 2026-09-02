using System.Security.Claims;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.Controllers;

[Authorize(Roles = "Admin")]
[Route("AdminFeedback")]
public sealed class AdminFeedbackController : Controller
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly AtelieDaTransformacaoDbContext _context;

    public AdminFeedbackController(
        IFeedbackRepository feedbackRepository,
        AtelieDaTransformacaoDbContext context)
    {
        _feedbackRepository = feedbackRepository;
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(
        string? status = null,
        string? cliente = null,
        string? produto = null)
    {
        bool? approved = status?.ToLowerInvariant() switch
        {
            "pendentes" => false,
            "aprovados" => true,
            _ => null
        };

        var feedbacks = await _feedbackRepository.GetAllForAdminAsync(approved);
        var users = await _context.Users.AsNoTracking().ToDictionaryAsync(x => x.Id);
        var products = await _context.Products.AsNoTracking().ToDictionaryAsync(x => x.Id, x => x.Title);
        var orders = await _context.Orders.AsNoTracking().ToDictionaryAsync(x => x.Id, x => x.OrderNumber);

        var items = feedbacks.Select(feedback =>
        {
            users.TryGetValue(feedback.UsuarioId, out var user);
            products.TryGetValue(feedback.ProdutoId, out var productName);
            orders.TryGetValue(feedback.PedidoId, out _);

            var clientName = feedback.IsAnonimo
                ? "Cliente Anônimo"
                : BuildUserName(user);

            return new AdminFeedbackItemViewModel
            {
                Id = feedback.Id,
                Cliente = clientName,
                Email = user?.Email ?? string.Empty,
                Produto = productName ?? "Produto",
                PedidoId = feedback.PedidoId,
                Nota = feedback.Nota,
                Comentario = feedback.Comentario,
                ImagemUrl = feedback.ImagemUrl,
                IsAnonimo = feedback.IsAnonimo,
                IsAprovado = feedback.IsAprovado,
                DataCriacao = feedback.DataCriacao,
                AprovadoEm = feedback.AprovadoEm
            };
        });

        if (!string.IsNullOrWhiteSpace(cliente))
        {
            items = items.Where(x =>
                x.Cliente.Contains(cliente, StringComparison.OrdinalIgnoreCase) ||
                x.Email.Contains(cliente, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(produto))
        {
            items = items.Where(x =>
                x.Produto.Contains(produto, StringComparison.OrdinalIgnoreCase));
        }

        var model = new AdminFeedbackViewModel
        {
            Status = status,
            Cliente = cliente,
            Produto = produto,
            Feedbacks = items.ToList()
        };

        return View(model);
    }

    [HttpPost("Approve")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
    {
        var adminUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(adminUserId))
        {
            TempData["ErrorMessage"] = "Administrador não identificado.";
            return RedirectToAction(nameof(Index));
        }

        var changed = await _feedbackRepository.SetApprovalAsync(
            id,
            approved: true,
            adminUserId);

        TempData[changed ? "SuccessMessage" : "ErrorMessage"] = changed
            ? "Feedback aprovado e liberado para o site."
            : "Feedback não encontrado ou já alterado.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Revoke")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Revoke(int id)
    {
        var adminUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        var changed = await _feedbackRepository.SetApprovalAsync(
            id,
            approved: false,
            adminUserId);

        TempData[changed ? "SuccessMessage" : "ErrorMessage"] = changed
            ? "Feedback retirado da publicação pública."
            : "Feedback não encontrado ou já alterado.";

        return RedirectToAction(nameof(Index));
    }

    private static string BuildUserName(IdentityUser? user)
    {
        if (user is null)
            return "Cliente";

        return string.IsNullOrWhiteSpace(user.UserName)
            ? "Cliente"
            : user.UserName;
    }
}
