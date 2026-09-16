using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AtelieDaTransformacao.UI.Services;

/// <summary>
/// Performs pre-registration checks. DNS/MX can confirm that the domain is configured
/// to receive mail, but only the confirmation link sent to the mailbox can prove ownership.
/// Uses Cloudflare DNS-over-HTTPS instead of any Google service.
/// </summary>
public sealed class EmailAddressVerifier : IEmailAddressVerifier
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<EmailAddressVerifier> _logger;

    private static readonly Regex BasicEmail = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public EmailAddressVerifier(
        IHttpClientFactory httpClientFactory,
        ILogger<EmailAddressVerifier> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<EmailAddressVerificationResult> VerifyAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        email = email.Trim().ToLowerInvariant();

        if (email.Length > 180 || !BasicEmail.IsMatch(email))
            return new(false, "Informe um endereço de e-mail válido.");

        if (!MailAddress.TryCreate(email, out var address) ||
            !address.Address.Equals(email, StringComparison.OrdinalIgnoreCase))
            return new(false, "Informe um endereço de e-mail válido.");

        var domain = address.Host.TrimEnd('.');
        if (string.IsNullOrWhiteSpace(domain) || !domain.Contains('.'))
            return new(false, "O domínio do e-mail é inválido.");

        try
        {
            var client = _httpClientFactory.CreateClient("EmailDns");
            var url = $"dns-query?name={Uri.EscapeDataString(domain)}&type=MX";
            using var response = await client.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Falha ao consultar MX para {Domain}: HTTP {StatusCode}.",
                    domain,
                    response.StatusCode);

                // DNS is a pre-check, not proof of mailbox ownership. Do not block a
                // legitimate registration solely because the DNS resolver is unavailable.
                return new(true, "Endereço válido. Enviaremos um link para confirmar a caixa postal.");
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

            var status = document.RootElement.TryGetProperty("Status", out var statusElement)
                ? statusElement.GetInt32()
                : -1;

            if (status != 0)
                return new(true, "Endereço válido. Enviaremos um link para confirmar a caixa postal.");

            var hasMx = document.RootElement.TryGetProperty("Answer", out var answers) &&
                        answers.ValueKind == JsonValueKind.Array &&
                        answers.EnumerateArray().Any(x =>
                            x.TryGetProperty("type", out var type) && type.GetInt32() == 15);

            if (!hasMx)
            {
                // RFC-compliant domains may accept mail through their A/AAAA record when
                // no MX exists. The final confirmation email remains the ownership proof.
                _logger.LogWarning("Domínio {Domain} não apresentou registro MX.", domain);
            }

            return new(true, "Domínio analisado. Enviaremos um link para confirmar a caixa postal.");
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Não foi possível verificar o MX do e-mail {Email}.", email);
            return new(true, "Endereço válido. Enviaremos um link para confirmar a caixa postal.");
        }
    }
}
