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
    [AllowAnonymous]
    [HttpGet("validate")]
    public async Task<IActionResult> Validate(string? email)
    {
        email = email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email))
            return Ok(new { valid = false, message = "Informe um e-mail." });

        if (email.Length > 180 || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Ok(new { valid = false, message = "Formato de e-mail inválido." });

        try
        {
            var address = new MailAddress(email);
            var domain = address.Host.TrimEnd('.');
            if (string.IsNullOrWhiteSpace(domain) || !domain.Contains('.'))
                return Ok(new { valid = false, message = "Domínio de e-mail inválido." });

            var addresses = await Dns.GetHostAddressesAsync(domain);
            return Ok(addresses.Length > 0
                ? new { valid = true, message = "E-mail válido." }
                : new { valid = false, message = "O domínio do e-mail não foi encontrado." });
        }
        catch
        {
            return Ok(new { valid = false, message = "Não foi possível validar o domínio deste e-mail." });
        }
    }
}
