using System.Text;
using AtelieDaTransformacao.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.Controllers;

public class QuoteController : Controller
{
    private const string WhatsAppNumber = "5511999999999"; // TROQUE pelo número real da empresa.

    [HttpGet]
    public IActionResult Index() => View(new QuoteRequestViewModel());

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
