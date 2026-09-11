using System.Net;
using System.Net.Mail;
using System.Text;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Enums;
using Microsoft.Extensions.Options;

namespace AtelieDaTransformacao.UI.Services;

public sealed class SmtpEmailService : IEmailService
{
    private readonly EmailOptions _options;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IOptions<EmailOptions> options, ILogger<SmtpEmailService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(to))
            return;

        if (string.IsNullOrWhiteSpace(_options.Host) || string.IsNullOrWhiteSpace(_options.From))
        {
            _logger.LogWarning("E-mail não enviado: SMTP não configurado.");
            return;
        }

        if (!MailAddress.TryCreate(to.Trim(), out var recipient))
        {
            _logger.LogWarning("E-mail não enviado: destinatário inválido.");
            return;
        }

        if (!MailAddress.TryCreate(_options.From.Trim(), out var sender))
        {
            _logger.LogError("E-mail não enviado: remetente SMTP inválido.");
            return;
        }

        using var message = new MailMessage
        {
            From = new MailAddress(sender.Address, _options.FromName, Encoding.UTF8),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true,
            BodyEncoding = Encoding.UTF8,
            SubjectEncoding = Encoding.UTF8
        };
        message.To.Add(recipient);

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Timeout = Math.Clamp(_options.TimeoutSeconds, 3, 60) * 1000
        };

        if (!string.IsNullOrWhiteSpace(_options.UserName))
            client.Credentials = new NetworkCredential(_options.UserName, _options.Password);

        cancellationToken.ThrowIfCancellationRequested();
        await client.SendMailAsync(message, cancellationToken);
    }

    public Task SendPasswordResetAsync(string to, string resetUrl, CancellationToken cancellationToken = default) =>
        SendAsync(to, "Redefinição de senha — Ateliê da Transformação",
            $"""
            <div style="font-family:Arial,sans-serif;line-height:1.6">
              <h2>Redefinição de senha</h2>
              <p>Recebemos uma solicitação para redefinir a senha da sua conta.</p>
              <p><a href="{WebUtility.HtmlEncode(resetUrl)}" style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">Criar nova senha</a></p>
              <p>Se você não fez esta solicitação, ignore este e-mail.</p>
            </div>
            """, cancellationToken);

    public Task SendOrderStatusAsync(Order order, CancellationToken cancellationToken = default)
    {
        var email = order.CustomerEmail ?? order.UserEmail;
        if (string.IsNullOrWhiteSpace(email))
            return Task.CompletedTask;

        var detailsUrl = string.IsNullOrWhiteSpace(_options.BaseUrl)
            ? $"/Order/Details/{order.Id}"
            : $"{_options.BaseUrl.TrimEnd('/')}/Order/Details/{order.Id}";
        return SendAsync(email, $"Atualização do pedido {order.OrderNumber}",
            $"""
            <div style="font-family:Arial,sans-serif;line-height:1.6">
              <h2>Seu pedido foi atualizado</h2>
              <p>O pedido <strong>{WebUtility.HtmlEncode(order.OrderNumber)}</strong> agora está em <strong>{WebUtility.HtmlEncode(order.Status.ToDisplayName())}</strong>.</p>
              <p><a href="{WebUtility.HtmlEncode(detailsUrl)}" style="display:inline-block;padding:12px 20px;background:#a85c3d;color:#fff;text-decoration:none;border-radius:8px">Acompanhar pedido</a></p>
            </div>
            """, cancellationToken);
    }
}
