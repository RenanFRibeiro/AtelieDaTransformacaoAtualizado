using System.Security.Claims;
using System.Text.Json;
using AtelieDaTransformacao.Application.ViewModels;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI.ViewComponents;

public sealed class FeedbacksViewComponent : ViewComponent
{
    private readonly AtelieDaTransformacaoDbContext _context;

    private readonly IFeedbackRepository
        _feedbackRepository;

    public FeedbacksViewComponent(
        AtelieDaTransformacaoDbContext context,
        IFeedbackRepository feedbackRepository)
    {
        _context = context;
        _feedbackRepository = feedbackRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(
        int? orderId = null)
    {
        if (orderId.HasValue)
        {
            var orderItems =
                await BuildOrderItemsAsync(
                    orderId.Value);

            return View(
                "OrderItems",
                orderItems);
        }

        var feedbacks =
            await _feedbackRepository
                .GetPublishedAsync(50);

        var cards =
            new List<FeedbackCardViewModel>();

        foreach (var feedback in feedbacks)
        {
            var productTitle =
                await _context.Products
                    .AsNoTracking()
                    .Where(
                        x =>
                            x.Id ==
                            feedback.ProdutoId)
                    .Select(
                        x =>
                            x.Title)
                    .FirstOrDefaultAsync();

            cards.Add(
                new FeedbackCardViewModel
                {
                    Id =
                        feedback.Id,

                    PublicName =
                        string.IsNullOrWhiteSpace(
                            feedback.PublicName)
                            ? "Cliente"
                            : feedback.PublicName,

                    ProdutoNome =
                        productTitle ??
                        "Produto",

                    Nota =
                        feedback.Nota,

                    Comentario =
                        feedback.Comentario,

                    ImagemUrl =
                        feedback.ImagemUrl,

                    DataCriacao =
                        feedback.DataCriacao
                });
        }

        return View(
            "Home",
            cards);
    }

    private async Task<
        IReadOnlyList<OrderFeedbackItemViewModel>>
        BuildOrderItemsAsync(
            int orderId)
    {
        var claimsPrincipal =
            User as ClaimsPrincipal;

        var userId =
            claimsPrincipal?.FindFirstValue(
                ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(
                userId))
        {
            return Array.Empty<
                OrderFeedbackItemViewModel>();
        }

        var order =
            await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == orderId &&
                        x.UserId == userId);

        if (order is null ||
            order.Status !=
            OrderStatus.Entregue)
        {
            return Array.Empty<
                OrderFeedbackItemViewModel>();
        }

        var items =
            DeserializeItems(
                order.ItemsJson);

        var feedbacks =
            await _feedbackRepository
                .GetForOrderAsync(
                    userId,
                    orderId);

        return items
            .GroupBy(
                x =>
                    x.ProductId)
            .Select(
                group =>
                {
                    var item =
                        group.First();

                    var feedback =
                        feedbacks.FirstOrDefault(
                            x =>
                                x.ProdutoId ==
                                item.ProductId);

                    return
                        new OrderFeedbackItemViewModel
                        {
                            PedidoId =
                                order.Id,

                            PedidoNumero =
                                order.OrderNumber,

                            ProdutoId =
                                item.ProductId,

                            ProdutoNome =
                                item.Title,

                            Quantidade =
                                group.Sum(
                                    x =>
                                        x.Quantity),

                            PodeAvaliar =
                                feedback is null,

                            Feedback =
                                feedback is null
                                    ? null
                                    : new FeedbackDisplayViewModel
                                    {
                                        Id =
                                            feedback.Id,

                                        Nota =
                                            feedback.Nota,

                                        Comentario =
                                            feedback.Comentario,

                                        ImagemUrl =
                                            feedback.ImagemUrl,

                                        IsAnonimo =
                                            feedback.IsAnonimo,

                                        DataCriacao =
                                            feedback.DataCriacao
                                    }
                        };
                })
            .ToList();
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