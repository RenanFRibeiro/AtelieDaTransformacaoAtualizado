using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using Microsoft.Extensions.Options;

namespace AtelieDaTransformacao.UI.Services;

/// <summary>
/// Transactional email service backed by Brevo's HTTP API.
/// This deliberately does not use Gmail, SMTP passwords, or Google services.
/// </summary>
public sealed class BrevoEmailService : IEmailService
{
    private const string BrevoEndpoint = "https://api.brevo.com/v3/smtp/email";

    private readonly EmailOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<BrevoEmailService> _logger;

    public BrevoEmailService(
        IOptions<EmailOptions> options,
        IHttpClientFactory httpClientFactory,
        ILogger<BrevoEmailService> logger)
    {
        _options = options.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public Task SendPasswordResetAsync(
        string to,
        string resetUrl,
        CancellationToken cancellationToken = default) =>
        SendAsync(
            to,
            "Redefinição de senha — Ateliê da Transformação",
            $"""
            <div style="font-family:Arial,sans-serif;line-height:1.6">
              <h2>Redefinição de senha</h2>
              <p>Recebemos uma solicitação para redefinir a senha da sua conta.</p>
              <p><a href="{WebUtility.HtmlEncode(resetUrl)}" style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">Criar nova senha</a></p>
              <p>Se você não fez esta solicitação, ignore este e-mail.</p>
            </div>
            """,
            cancellationToken);

    public Task SendEmailConfirmationAsync(
        string to,
        string confirmationUrl,
        CancellationToken cancellationToken = default) =>
        SendAsync(
            to,
            "Confirme seu e-mail — Ateliê da Transformação",
            $"""
            <div style="font-family:Arial,sans-serif;line-height:1.6">
              <h2>Confirme seu e-mail</h2>
              <p>Para ativar sua conta, confirme o endereço de e-mail clicando no botão abaixo.</p>
              <p><a href="{WebUtility.HtmlEncode(confirmationUrl)}" style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">Confirmar e-mail</a></p>
              <p>Se você não criou esta conta, ignore esta mensagem.</p>
            </div>
            """,
            cancellationToken);

    public Task SendOrderStatusAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        var email = order.CustomerEmail ?? order.UserEmail;
        if (string.IsNullOrWhiteSpace(email))
            return Task.CompletedTask;

        var detailsUrl = string.IsNullOrWhiteSpace(_options.BaseUrl)
            ? $"/Order/Details/{order.Id}"
            : $"{_options.BaseUrl.TrimEnd('/')}/Order/Details/{order.Id}";

        return SendAsync(
            email,
            $"Atualização do pedido {order.OrderNumber}",
            $"""
            <div style="font-family:Arial,sans-serif;line-height:1.6">
              <h2>Seu pedido foi atualizado</h2>
              <p>O pedido <strong>{WebUtility.HtmlEncode(order.OrderNumber)}</strong> agora está em <strong>{WebUtility.HtmlEncode(order.Status.ToDisplayName())}</strong>.</p>
              <p><a href="{WebUtility.HtmlEncode(detailsUrl)}" style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">Acompanhar pedido</a></p>
            </div>
            """,
            cancellationToken);
    }

    public async Task SendAsync(
        string to,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(to))
            throw new ArgumentException("O destinatário do e-mail é obrigatório.", nameof(to));

        if (!MailAddress.TryCreate(to.Trim(), out var recipient) ||
            !recipient.Address.Equals(to.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("O destinatário do e-mail é inválido.", nameof(to));
        }

        var apiKey = (_options.ApiKey ?? string.Empty).Trim();
        var from = (_options.From ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException(
                "Brevo não está configurado. Defina Email:ApiKey (variável de ambiente: Email__ApiKey) com uma chave de API válida da Brevo.");

        if (!MailAddress.TryCreate(from, out var sender) ||
            !sender.Address.Equals(from, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Remetente Brevo inválido. Verifique o remetente na Brevo e configure exatamente o mesmo endereço em Email:From (variável: Email__From).");
        }

        var payload = new
        {
            sender = new
            {
                email = sender.Address,
                name = string.IsNullOrWhiteSpace(_options.FromName)
                    ? "Ateliê da Transformação"
                    : _options.FromName.Trim()
            },
            to = new[]
            {
                new
                {
                    email = recipient.Address
                }
            },
            subject,
            htmlContent = htmlBody,
            textContent = StripHtml(htmlBody)
        };

        var client = _httpClientFactory.CreateClient("BrevoEmail");
        using var request = new HttpRequestMessage(HttpMethod.Post, BrevoEndpoint);
        request.Headers.Add("api-key", apiKey);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        cancellationToken.ThrowIfCancellationRequested();

        using var response = await client.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation(
                "E-mail transacional enviado pela Brevo para {Recipient}. Assunto: {Subject}.",
                recipient.Address,
                subject);
            return;
        }

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        var safeDetails = responseBody.Length > 1000
            ? responseBody[..1000]
            : responseBody;

        _logger.LogError(
            "Brevo recusou o envio para {Recipient}. HTTP {StatusCode}. Resposta: {Response}",
            recipient.Address,
            (int)response.StatusCode,
            safeDetails);

        var message = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized =>
                "A chave da API Brevo é inválida ou não foi configurada.",
            HttpStatusCode.Forbidden =>
                "A Brevo recusou o envio. Verifique se o remetente/domínio está validado e autorizado.",
            HttpStatusCode.TooManyRequests =>
                "O limite de envio da Brevo foi atingido. Tente novamente mais tarde.",
            _ =>
                $"A Brevo não aceitou o envio do e-mail (HTTP {(int)response.StatusCode}). Verifique o remetente e a configuração da conta Brevo."
        };

        throw new InvalidOperationException(message);
    }

    private static string StripHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
            return string.Empty;

        var text = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", " ");
        return WebUtility.HtmlDecode(text).Trim();
    }
}
