using System.Security.Claims;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.UI.Models;
using AtelieDaTransformacao.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AtelieDaTransformacao.UI.Controllers;

[EnableRateLimiting("auth")]
public sealed class AccountController : Controller
{
    private const string RegistrationReasonKey = "RegistrationReason";

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IEmailAddressVerifier _emailAddressVerifier;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IEmailService emailService,
        IEmailAddressVerifier emailAddressVerifier)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
        _emailAddressVerifier = emailAddressVerifier;
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
        else if (result.IsNotAllowed)
        {
            ModelState.AddModelError(string.Empty,
                "Confirme seu e-mail antes de entrar. Verifique sua caixa de entrada ou solicite um novo link de confirmação.");
        }
        else
        {
            ModelState.AddModelError(string.Empty,
                "E-mail ou senha inválidos.");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPassword() => View(new ForgotPasswordDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = model.Email.Trim().ToLowerInvariant();
        var user = await _userManager.FindByEmailAsync(email);

        // Resposta genérica evita revelar se um e-mail está cadastrado.
        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = Url.Action(nameof(ResetPassword), "Account",
                new { userId = user.Id, token, email }, Request.Scheme);

            if (!string.IsNullOrWhiteSpace(resetUrl))
                await _emailService.SendPasswordResetAsync(email, resetUrl);
        }

        TempData["PasswordResetMessage"] =
            "Se o e-mail estiver cadastrado, enviaremos as instruções para redefinir sua senha.";
        return RedirectToAction(nameof(ForgotPassword));
    }

    [HttpGet]
    public IActionResult ResetPassword(string? userId, string? token, string? email)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return RedirectToAction(nameof(ForgotPassword));

        return View(new ResetPasswordDto
        {
            UserId = userId,
            Token = token,
            Email = email ?? string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null || !string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(string.Empty, "Não foi possível validar a solicitação de redefinição.");
            return View(model);
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, FriendlyIdentityError(error));
            return View(model);
        }

        TempData["SuccessMessage"] = "Sua senha foi redefinida. Agora você já pode entrar.";
        return RedirectToAction(nameof(Login));
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

        var emailVerification = await _emailAddressVerifier.VerifyAsync(model.Email, HttpContext.RequestAborted);
        if (!emailVerification.IsValid)
        {
            ModelState.AddModelError(nameof(model.Email), emailVerification.Message);
            return View(model);
        }

        var existing = await _userManager.FindByEmailAsync(model.Email);
        if (existing != null)
        {
            if (await _userManager.IsEmailConfirmedAsync(existing))
            {
                ModelState.AddModelError(nameof(model.Email),
                    "Já existe uma conta com este e-mail. Faça login para continuar.");
                return View(model);
            }

            // Se o cliente iniciou um cadastro anteriormente, não o obrigamos
            // a preencher tudo novamente. Reenviamos a confirmação para a
            // conta pendente e preservamos a experiência de cadastro.
            TempData["ConfirmationEmail"] = existing.Email;
            try
            {
                var pendingConfirmationSent = await TrySendConfirmationEmailAsync(existing);
                if (pendingConfirmationSent)
                {
                    TempData["ConfirmationResendMessage"] =
                        "Já existe um cadastro pendente para este e-mail. Enviamos um novo link de confirmação.";
                }
                else
                {
                    TempData["ConfirmationResendError"] =
                        "Sua conta já foi criada, mas não conseguimos enviar o e-mail de confirmação. Verifique a configuração SMTP do site e tente reenviar.";
                }
            }
            catch (Exception ex)
            {
                TempData["ConfirmationResendError"] =
                    "Sua conta já foi criada, mas o e-mail de confirmação não pôde ser enviado. Verifique a configuração SMTP e tente reenviar.";
                HttpContext.RequestServices.GetRequiredService<ILogger<AccountController>>()
                    .LogError(ex, "Falha ao reenviar confirmação de e-mail para {Email}.", existing.Email);
            }

            return RedirectToAction(nameof(EmailConfirmationSent), new { returnUrl = SafeReturnUrl(returnUrl) });
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

        // A criação da conta só termina com sucesso depois que o sistema
        // consegue enviar o link para o endereço informado. A confirmação
        // do link é a prova de posse da caixa postal (Gmail, Outlook, Yahoo
        // ou outro provedor), e não apenas uma validação de formato/domínio.
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationUrl = Url.Action(
            nameof(ConfirmEmail),
            "Account",
            new { userId = user.Id, token },
            Request.Scheme);

        if (string.IsNullOrWhiteSpace(confirmationUrl))
        {
            await _userManager.DeleteAsync(user);
            ModelState.AddModelError(string.Empty, "Não foi possível gerar a confirmação do e-mail.");
            return View(model);
        }

        try
        {
            await _emailService.SendEmailConfirmationAsync(user.Email!, confirmationUrl);
        }
        catch (Exception ex)
        {
            // Não apagamos a conta quando o SMTP está indisponível.
            // O cadastro fica pendente de confirmação e o cliente pode
            // corrigir a configuração/repetir o envio sem perder os dados.
            TempData["ConfirmationEmail"] = user.Email;
            TempData["ConfirmationResendError"] =
                "Sua conta foi criada, mas o e-mail de confirmação não pôde ser enviado agora. Verifique a caixa de configuração de e-mail e tente reenviar.";
            HttpContext.RequestServices.GetRequiredService<ILogger<AccountController>>()
                .LogError(ex, "Falha ao enviar confirmação de e-mail para {Email}.", user.Email);
            return RedirectToAction(nameof(EmailConfirmationSent), new { returnUrl = SafeReturnUrl(returnUrl) });
        }

        TempData.Remove(RegistrationReasonKey);
        TempData["ConfirmationEmail"] = user.Email;
        return RedirectToAction(nameof(EmailConfirmationSent), new { returnUrl = SafeReturnUrl(returnUrl) });
    }

    [HttpGet]
    public IActionResult EmailConfirmationSent(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = SafeReturnUrl(returnUrl);
        ViewBag.ConfirmationEmail = TempData.Peek("ConfirmationEmail") as string ?? string.Empty;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResendConfirmationEmail(string? email, string? returnUrl = null)
    {
        var normalizedEmail = email?.Trim().ToLowerInvariant();
        var safeReturnUrl = SafeReturnUrl(returnUrl);

        // Resposta genérica: não revela se o endereço existe ou se a conta já foi confirmada.
        TempData["ConfirmationResendMessage"] =
            "Se houver uma conta pendente para esse e-mail, enviaremos um novo link de confirmação.";

        if (string.IsNullOrWhiteSpace(normalizedEmail) ||
            !System.Net.Mail.MailAddress.TryCreate(normalizedEmail, out var parsedEmail) ||
            !parsedEmail.Address.Equals(normalizedEmail, StringComparison.OrdinalIgnoreCase))
            return RedirectToAction(nameof(EmailConfirmationSent), new { returnUrl = safeReturnUrl });

        var user = await _userManager.FindByEmailAsync(normalizedEmail);
        if (user is null || await _userManager.IsEmailConfirmedAsync(user))
            return RedirectToAction(nameof(EmailConfirmationSent), new { returnUrl = safeReturnUrl });

        try
        {
            var sent = await TrySendConfirmationEmailAsync(user);
            if (!sent)
            {
                TempData["ConfirmationResendError"] =
                    "Não conseguimos enviar o e-mail de confirmação agora. Verifique a configuração SMTP e tente novamente.";
            }
        }
        catch (Exception ex)
        {
            TempData["ConfirmationResendError"] =
                "Não conseguimos enviar o e-mail de confirmação agora. Verifique a configuração SMTP e tente novamente.";
            HttpContext.RequestServices.GetRequiredService<ILogger<AccountController>>()
                .LogError(ex, "Falha ao reenviar confirmação de e-mail para {Email}.", normalizedEmail);
        }

        return RedirectToAction(nameof(EmailConfirmationSent), new { returnUrl = safeReturnUrl });
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string? userId, string? token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            TempData["ErrorMessage"] = "O link de confirmação é inválido ou está incompleto.";
            return RedirectToAction(nameof(Login));
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            TempData["ErrorMessage"] = "Não foi possível localizar a conta.";
            return RedirectToAction(nameof(Login));
        }

        if (await _userManager.IsEmailConfirmedAsync(user))
        {
            TempData["SuccessMessage"] = "Este e-mail já foi confirmado. Agora você já pode entrar.";
            return RedirectToAction(nameof(Login));
        }

        return View(new ConfirmEmailViewModel
        {
            UserId = userId,
            Token = token,
            Email = user.Email ?? string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmEmailPost(ConfirmEmailViewModel model)
    {
        if (!ModelState.IsValid)
            return View(nameof(ConfirmEmail), model);

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null)
        {
            TempData["ErrorMessage"] = "Não foi possível localizar a conta.";
            return RedirectToAction(nameof(Login));
        }

        var result = await _userManager.ConfirmEmailAsync(user, model.Token);
        TempData[result.Succeeded ? "SuccessMessage" : "ErrorMessage"] = result.Succeeded
            ? "E-mail confirmado com sucesso. Agora você já pode entrar."
            : "O link de confirmação é inválido ou expirou. Solicite um novo link de confirmação.";

        return RedirectToAction(nameof(Login));
    }

    private async Task<bool> TrySendConfirmationEmailAsync(IdentityUser user)
    {
        if (string.IsNullOrWhiteSpace(user.Email))
            return false;

        await _userManager.UpdateSecurityStampAsync(user);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationUrl = Url.Action(
            nameof(ConfirmEmail),
            "Account",
            new { userId = user.Id, token },
            Request.Scheme);

        if (string.IsNullOrWhiteSpace(confirmationUrl))
            return false;

        await _emailService.SendEmailConfirmationAsync(user.Email, confirmationUrl);
        return true;
    }

    // Favoritos e vistos recentemente usam armazenamento local do navegador,
    // portanto ficam disponíveis mesmo para visitantes não autenticados.
    [HttpGet]
    public IActionResult Favorites() => View();

    [HttpGet]
    public IActionResult RecentlyViewed() => View();

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
    [DisableRateLimiting]
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
