using System.Text;
using AtelieDaTransformacao.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AtelieDaTransformacao.UI.Controllers;

public class QuoteController : Controller
{
    private const string WhatsAppNumber = "5511999999999"; // TROQUE pelo número real da empresa.

    private readonly UserManager<IdentityUser> _userManager;

    public QuoteController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new QuoteRequestViewModel();

        // Quando o cliente está autenticado, aproveitamos os dados já
        // cadastrados no perfil para evitar que ele precise digitá-los novamente.
        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);

                var firstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                var lastName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                var name = string.Join(" ", new[] { firstName, lastName }
                    .Where(value => !string.IsNullOrWhiteSpace(value)))
                    .Trim();

                model.Name = !string.IsNullOrWhiteSpace(name)
                    ? name
                    : (user.UserName ?? string.Empty);

                var phone = claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value;
                model.Phone = !string.IsNullOrWhiteSpace(phone)
                    ? phone
                    : (user.PhoneNumber ?? string.Empty);
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(QuoteRequestViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var message = new StringBuilder();
        message.AppendLine("Olá! Gostaria de solicitar um orçamento personalizado.");
        message.AppendLine();
        message.AppendLine($"*Nome:* {model.Name}");
        message.AppendLine($"*WhatsApp:* {model.Phone}");
        message.AppendLine($"*Peça desejada:* {model.ProductType}");
        if (!string.IsNullOrWhiteSpace(model.Measurements)) message.AppendLine($"*Medidas:* {model.Measurements}");
        if (!string.IsNullOrWhiteSpace(model.Material)) message.AppendLine($"*Material/estilo:* {model.Material}");
        message.AppendLine($"*Descrição:* {model.Description}");

        var url = $"https://wa.me/{WhatsAppNumber}?text={Uri.EscapeDataString(message.ToString())}";
        return Redirect(url);
    }
}
