using AtelieDaTransformacao.UI.Models;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AtelieDaTransformacao.UI.Controllers;

public class QuoteController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IWhatsAppService _whatsAppService;

    public QuoteController(UserManager<IdentityUser> userManager, IWhatsAppService whatsAppService)
    {
        _userManager = userManager;
        _whatsAppService = whatsAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new QuoteRequestViewModel();

        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            var claims = user == null ? new List<Claim>() : await _userManager.GetClaimsAsync(user);
            model.Name = BuildFullName(claims);
            model.Phone = user?.PhoneNumber
                ?? User.FindFirstValue(ClaimTypes.MobilePhone)
                ?? string.Empty;
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(QuoteRequestViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var url = _whatsAppService.GenerateQuoteLink(
            model.Name, model.Phone, model.ProductType, model.Measurements, model.Material, model.Description);
        return Redirect(url);
    }

    private static string BuildFullName(IList<Claim> claims)
    {
        var first = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value?.Trim();
        var last = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value?.Trim();
        return string.Join(" ", new[] { first, last }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }
}
