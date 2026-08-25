using System.Security.Claims;
using AtelieDaTransformacao.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.Controllers;

public class AccountController : Controller
{
    private const string RegistrationReasonKey = "RegistrationReason";

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = SafeReturnUrl(returnUrl);
        return View(new LoginDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = SafeReturnUrl(returnUrl);

        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email.Trim(),
            model.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var safeReturnUrl = SafeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(safeReturnUrl))
                return Redirect(safeReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty,
                "Esta conta está temporariamente bloqueada. Tente novamente mais tarde.");
        }
        else
        {
            ModelState.AddModelError(string.Empty,
                "E-mail ou senha inválidos.");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewBag.RegistrationReason = TempData.Peek(RegistrationReasonKey) as string;
        ViewBag.ReturnUrl = SafeReturnUrl(returnUrl);

        return View(new RegisterDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto model, string? returnUrl = null)
    {
        ViewBag.RegistrationReason = TempData.Peek(RegistrationReasonKey) as string;
        ViewBag.ReturnUrl = SafeReturnUrl(returnUrl);

        if (!ModelState.IsValid)
            return View(model);

        model.FirstName = model.FirstName.Trim();
        model.LastName = model.LastName.Trim();
        model.Phone = model.Phone.Trim();
        model.Address = model.Address.Trim();
        model.AddressNumber = model.AddressNumber.Trim();
        model.Complement = model.Complement.Trim();
        model.District = model.District.Trim();
        model.City = model.City.Trim();
        model.State = model.State.Trim().ToUpperInvariant();
        model.PostalCode = model.PostalCode.Trim();
        model.Email = model.Email.Trim().ToLowerInvariant();

        var existing = await _userManager.FindByEmailAsync(model.Email);
        if (existing != null)
        {
            ModelState.AddModelError(nameof(model.Email),
                "Já existe uma conta com este e-mail. Faça login para continuar.");
            return View(model);
        }

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.Phone
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, FriendlyIdentityError(error));

            return View(model);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.GivenName, model.FirstName),
            new Claim(ClaimTypes.Surname, model.LastName),
            new Claim(ClaimTypes.MobilePhone, model.Phone),
            new Claim(ClaimTypes.StreetAddress, model.Address),
            new Claim("Atelie:AddressNumber", model.AddressNumber),
            new Claim("Atelie:Complement", model.Complement),
            new Claim("Atelie:District", model.District),
            new Claim(ClaimTypes.Locality, model.City),
            new Claim(ClaimTypes.StateOrProvince, model.State),
            new Claim(ClaimTypes.PostalCode, model.PostalCode)
        };

        var claimResult = await _userManager.AddClaimsAsync(user, claims);
        if (!claimResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);

            foreach (var error in claimResult.Errors)
                ModelState.AddModelError(string.Empty, FriendlyIdentityError(error));

            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        TempData.Remove(RegistrationReasonKey);

        var safeUrl = SafeReturnUrl(returnUrl);
        if (!string.IsNullOrWhiteSpace(safeUrl))
            return Redirect(safeUrl);

        TempData["SuccessMessage"] =
            $"Bem-vindo(a), {model.FirstName}! Sua conta foi criada com sucesso.";

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();

    private string? SafeReturnUrl(string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl) || !Url.IsLocalUrl(returnUrl))
            return null;

        var path = returnUrl.Split('?', '#')[0];
        if (path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase) &&
            !User.IsInRole("Admin"))
        {
            return null;
        }

        return returnUrl;
    }

    private static string FriendlyIdentityError(IdentityError error)
    {
        return error.Code switch
        {
            "DuplicateUserName" or "DuplicateEmail" =>
                "Já existe uma conta cadastrada com este e-mail.",
            "PasswordTooShort" =>
                "A senha precisa ter pelo menos 6 caracteres.",
            "PasswordRequiresDigit" =>
                "A senha precisa conter pelo menos um número.",
            "PasswordRequiresUpper" =>
                "A senha precisa conter pelo menos uma letra maiúscula.",
            "PasswordRequiresLower" =>
                "A senha precisa conter pelo menos uma letra minúscula.",
            "PasswordRequiresNonAlphanumeric" =>
                "A senha precisa conter pelo menos um caractere especial.",
            _ => error.Description
        };
    }
}
