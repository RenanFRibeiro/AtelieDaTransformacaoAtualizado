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
/// Serviço de e-mail transacional utilizando a API HTTP da Brevo.
/// Não utiliza Gmail, senha SMTP ou serviços do Google.
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

                <p>
                    <a href="{WebUtility.HtmlEncode(resetUrl)}"
                       style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">
                        Criar nova senha
                    </a>
                </p>

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

                <p>
                    Para ativar sua conta, confirme o endereço de e-mail
                    clicando no botão abaixo.
                </p>

                <p>
                    <a href="{WebUtility.HtmlEncode(confirmationUrl)}"
                       style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">
                        Confirmar e-mail
                    </a>
                </p>

                <p>
                    Se você não criou esta conta, ignore esta mensagem.
                </p>
            </div>
            """,
            cancellationToken);

    public Task SendOrderStatusAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        var email = order.CustomerEmail ?? order.UserEmail;

        if (string.IsNullOrWhiteSpace(email))
        {
            _logger.LogWarning(
                "Não foi possível enviar atualização do pedido {OrderId}: o pedido não possui e-mail.",
                order.Id);

            return Task.CompletedTask;
        }

        var detailsUrl = string.IsNullOrWhiteSpace(_options.BaseUrl)
            ? $"/Order/Details/{order.Id}"
            : $"{_options.BaseUrl.TrimEnd('/')}/Order/Details/{order.Id}";

        return SendAsync(
            email,
            $"Atualização do pedido {order.OrderNumber}",
            $"""
            <div style="font-family:Arial,sans-serif;line-height:1.6">
                <h2>Seu pedido foi atualizado</h2>

                <p>
                    O pedido
                    <strong>{WebUtility.HtmlEncode(order.OrderNumber)}</strong>
                    agora está em
                    <strong>{WebUtility.HtmlEncode(order.Status.ToDisplayName())}</strong>.
                </p>

                <p>
                    <a href="{WebUtility.HtmlEncode(detailsUrl)}"
                       style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">
                        Acompanhar pedido
                    </a>
                </p>
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
        // ============================================================
        // 1. VALIDAR DESTINATÁRIO
        // ============================================================

        if (string.IsNullOrWhiteSpace(to))
        {
            throw new ArgumentException(
                "O destinatário do e-mail é obrigatório.",
                nameof(to));
        }

        var recipientValue = to.Trim();

        if (!MailAddress.TryCreate(recipientValue, out var recipient) ||
            !recipient.Address.Equals(
                recipientValue,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                "O destinatário do e-mail é inválido.",
                nameof(to));
        }

        // ============================================================
        // 2. LER CONFIGURAÇÕES DA BREVO
        // ============================================================

        var apiKey = (_options.ApiKey ?? string.Empty).Trim();
        var from = (_options.From ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException(
                "Brevo não está configurado. " +
                "No Azure, defina Email__ApiKey em Environment variables/Application settings. " +
                "Localmente, use Email:ApiKey em User Secrets ou appsettings.Development.json.");
        }

        if (string.IsNullOrWhiteSpace(from))
        {
            throw new InvalidOperationException(
                "O remetente da Brevo não está configurado. " +
                "No Azure, defina Email__From com um endereço de remetente autorizado na Brevo.");
        }

        // ============================================================
        // 3. VALIDAR REMETENTE
        // ============================================================

        if (!MailAddress.TryCreate(from, out var sender) ||
            !sender.Address.Equals(
                from,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Remetente Brevo inválido. " +
                "Verifique o sender na Brevo e use exatamente o mesmo endereço em Email__From.");
        }

        // ============================================================
        // 4. MONTAR PAYLOAD DA BREVO
        // ============================================================

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

            subject = subject ?? string.Empty,

            htmlContent = htmlBody ?? string.Empty,

            textContent = StripHtml(htmlBody)
        };

        // ============================================================
        // 5. SERIALIZAR JSON
        // ============================================================

        var json = JsonSerializer.Serialize(payload);

        // ============================================================
        // 6. CRIAR CLIENTE HTTP
        // ============================================================

        var client = _httpClientFactory.CreateClient("BrevoEmail");

        // ============================================================
        // 7. CRIAR REQUISIÇÃO
        // ============================================================

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            BrevoEndpoint);

        request.Headers.Add("api-key", apiKey);

        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        request.Content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        // ============================================================
        // 8. CANCELAMENTO
        // ============================================================

        cancellationToken.ThrowIfCancellationRequested();

        // ============================================================
        // 9. ENVIAR PARA A BREVO
        // ============================================================

        using var response = await client.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        // ============================================================
        // 10. LER A RESPOSTA UMA ÚNICA VEZ
        // ============================================================

        var responseBody =
            await response.Content.ReadAsStringAsync(cancellationToken);

        // Limita o conteúdo registrado no log para evitar logs enormes.
        var safeResponseBody = responseBody.Length > 1000
            ? responseBody[..1000]
            : responseBody;

        // ============================================================
        // 11. TRATAR ERRO
        // ============================================================

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(
                "Brevo recusou o envio do e-mail para {Recipient}. " +
                "HTTP {StatusCode}. Resposta: {ResponseBody}",
                recipient.Address,
                (int)response.StatusCode,
                safeResponseBody);

            var message = response.StatusCode switch
            {
                HttpStatusCode.Unauthorized =>
                    "A chave da API Brevo é inválida ou não foi configurada.",

                HttpStatusCode.Forbidden =>
                    "A Brevo recusou o envio. " +
                    "Verifique se o remetente ou domínio está validado e autorizado.",

                HttpStatusCode.TooManyRequests =>
                    "O limite de envio da Brevo foi atingido. " +
                    "Tente novamente mais tarde.",

                _ =>
                    $"A Brevo não aceitou o envio do e-mail " +
                    $"(HTTP {(int)response.StatusCode}). " +
                    "Verifique o remetente e a configuração da conta Brevo."
            };

            throw new InvalidOperationException(message);
        }

        // ============================================================
        // 12. SUCESSO
        // ============================================================

        _logger.LogInformation(
            "E-mail aceito pela Brevo para {Recipient}. " +
            "HTTP {StatusCode}. Resposta: {ResponseBody}",
            recipient.Address,
            (int)response.StatusCode,
            safeResponseBody);
    }

    // ================================================================
    // REMOVE TAGS HTML PARA GERAR A VERSÃO TEXTO DO E-MAIL
    // ================================================================

    private static string StripHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        var text = System.Text.RegularExpressions.Regex.Replace(
            html,
            "<[^>]+>",
            " ");

        return WebUtility.HtmlDecode(text).Trim();
    }
}