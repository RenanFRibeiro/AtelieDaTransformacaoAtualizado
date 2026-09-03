using System.Security.Claims;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.UI.Models;
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
            lockoutOnFailure: true);

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

        if (!model.AcceptTerms)
        {
            ModelState.AddModelError(nameof(model.AcceptTerms),
                "Aceite os Termos de Uso e a Política de Privacidade para continuar.");
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

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
            return RedirectToAction(nameof(Login), new { returnUrl = Url.Action(nameof(Profile), "Account") });

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction(nameof(Login));

        var claims = await _userManager.GetClaimsAsync(user);
        var model = new ProfileViewModel
        {
            Email = user.Email ?? string.Empty,
            FirstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty,
            LastName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value ?? string.Empty,
            Phone = user.PhoneNumber ?? string.Empty,
            Address = claims.FirstOrDefault(c => c.Type == ClaimTypes.StreetAddress)?.Value ?? string.Empty,
            AddressNumber = claims.FirstOrDefault(c => c.Type == "Atelie:AddressNumber")?.Value ?? string.Empty,
            Complement = claims.FirstOrDefault(c => c.Type == "Atelie:Complement")?.Value ?? string.Empty,
            District = claims.FirstOrDefault(c => c.Type == "Atelie:District")?.Value ?? string.Empty,
            City = claims.FirstOrDefault(c => c.Type == ClaimTypes.Locality)?.Value ?? string.Empty,
            State = claims.FirstOrDefault(c => c.Type == ClaimTypes.StateOrProvince)?.Value ?? string.Empty,
            PostalCode = claims.FirstOrDefault(c => c.Type == ClaimTypes.PostalCode)?.Value ?? string.Empty,
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
            return RedirectToAction(nameof(Login));

        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction(nameof(Login));

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

        user.PhoneNumber = model.Phone;
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
                ModelState.AddModelError(string.Empty, FriendlyIdentityError(error));
            return View(model);
        }

        var oldClaims = await _userManager.GetClaimsAsync(user);
        var profileClaimTypes = new[]
        {
            ClaimTypes.GivenName, ClaimTypes.Surname, ClaimTypes.MobilePhone,
            ClaimTypes.StreetAddress, "Atelie:AddressNumber", "Atelie:Complement",
            "Atelie:District", ClaimTypes.Locality, ClaimTypes.StateOrProvince,
            ClaimTypes.PostalCode
        };
        var claimsToRemove = oldClaims.Where(c => profileClaimTypes.Contains(c.Type)).ToList();
        if (claimsToRemove.Count > 0)
            await _userManager.RemoveClaimsAsync(user, claimsToRemove);

        var newClaims = new[]
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
        await _userManager.AddClaimsAsync(user, newClaims);

        await _signInManager.RefreshSignInAsync(user);
        TempData["SuccessMessage"] = "Seus dados foram atualizados com sucesso.";
        return RedirectToAction(nameof(Profile));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
            return RedirectToAction(nameof(Login));

        if (!ModelState.IsValid)
        {
            TempData["EmailError"] = string.Join(" ", ModelState.Values
                .SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                .Where(e => !string.IsNullOrWhiteSpace(e)));
            return RedirectToAction(nameof(Profile));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction(nameof(Login));

        var currentEmail = user.Email ?? string.Empty;
        var newEmail = model.NewEmail.Trim().ToLowerInvariant();
        if (string.Equals(currentEmail, newEmail, StringComparison.OrdinalIgnoreCase))
        {
            TempData["EmailError"] = "O novo e-mail precisa ser diferente do atual.";
            return RedirectToAction(nameof(Profile));
        }

        if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
        {
            TempData["EmailError"] = "A senha atual está incorreta. O e-mail não foi alterado.";
            return RedirectToAction(nameof(Profile));
        }

        var existing = await _userManager.FindByEmailAsync(newEmail);
        if (existing != null && existing.Id != user.Id)
        {
            TempData["EmailError"] = "Este e-mail já está vinculado a outra conta.";
            return RedirectToAction(nameof(Profile));
        }

        var result = await _userManager.ChangeEmailAsync(user, newEmail, await _userManager.GenerateChangeEmailTokenAsync(user, newEmail));
        if (!result.Succeeded)
        {
            TempData["EmailError"] = string.Join(" ", result.Errors.Select(FriendlyIdentityError));
            return RedirectToAction(nameof(Profile));
        }

        user.UserName = newEmail;
        var usernameResult = await _userManager.UpdateAsync(user);
        if (!usernameResult.Succeeded)
        {
            TempData["EmailError"] = "O e-mail foi alterado, mas não foi possível atualizar o usuário de login. Tente entrar novamente.";
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction(nameof(Profile));
        }

        await _signInManager.RefreshSignInAsync(user);
        TempData["EmailSuccess"] = "Seu e-mail foi alterado com sucesso.";
        return RedirectToAction(nameof(Profile));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
            return RedirectToAction(nameof(Login));

        if (!ModelState.IsValid)
        {
            TempData["PasswordError"] = string.Join(" ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .Where(e => !string.IsNullOrWhiteSpace(e)));
            return RedirectToAction(nameof(Profile));
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction(nameof(Login));

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            TempData["PasswordError"] = string.Join(" ", result.Errors.Select(FriendlyIdentityError));
            return RedirectToAction(nameof(Profile));
        }

        await _signInManager.RefreshSignInAsync(user);
        TempData["PasswordSuccess"] = "Sua senha foi alterada com sucesso.";
        return RedirectToAction(nameof(Profile));
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
