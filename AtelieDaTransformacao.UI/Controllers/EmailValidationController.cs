using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.Controllers;

[ApiController]
[Route("api/email-validation")]
public sealed class EmailValidationController : ControllerBase
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
    private static readonly ConcurrentDictionary<string, (DateTimeOffset ExpiresAt, bool Valid, string Message)> Cache = new();

    [AllowAnonymous]
    [HttpGet("validate")]
    public async Task<IActionResult> Validate(string? email, CancellationToken cancellationToken)
    {
        email = email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email))
            return Ok(new { valid = false, message = "Informe um e-mail." });

        if (email.Length > 180 || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Ok(new { valid = false, message = "Formato de e-mail inválido." });

        if (Cache.TryGetValue(email, out var cached) && cached.ExpiresAt > DateTimeOffset.UtcNow)
            return Ok(new { valid = cached.Valid, message = cached.Message });

        try
        {
            var address = new MailAddress(email);
            var domain = address.Host.TrimEnd('.');
            if (string.IsNullOrWhiteSpace(domain) || !domain.Contains('.'))
                return Ok(new { valid = false, message = "Domínio de e-mail inválido." });

            var addresses = await Dns.GetHostAddressesAsync(domain, cancellationToken);
            var valid = addresses.Length > 0;
            var message = valid ? "Domínio de e-mail válido." : "O domínio do e-mail não foi encontrado.";

            Cache[email] = (DateTimeOffset.UtcNow.Add(CacheDuration), valid, message);
            return Ok(new { valid, message });
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch
        {
            return Ok(new { valid = false, message = "Não foi possível validar o domínio deste e-mail agora." });
        }
    }
}
