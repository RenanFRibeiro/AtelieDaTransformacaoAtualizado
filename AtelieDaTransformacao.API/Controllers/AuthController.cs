using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AtelieDaTransformacao.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _users;
    private readonly SignInManager<IdentityUser> _signIn;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> users, SignInManager<IdentityUser> signIn, IConfiguration configuration)
    {
        _users = users;
        _signIn = signIn;
        _configuration = configuration;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [EnableRateLimiting("auth")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
    {
        if (!dto.AcceptTerms)
            return BadRequest(new { message = "Aceite os Termos de Uso e a Política de Privacidade para continuar." });

        if (dto.Password != dto.ConfirmPassword)
            return BadRequest(new { message = "As senhas não coincidem." });

        var normalizedEmail = dto.Email?.Trim().ToLowerInvariant() ?? string.Empty;
        if (normalizedEmail.Length > 180 || !System.Net.Mail.MailAddress.TryCreate(normalizedEmail, out _))
            return BadRequest(new { message = "Informe um e-mail válido." });

        var existing = await _users.FindByEmailAsync(normalizedEmail);
        if (existing is not null)
            return Conflict(new { message = "Já existe um usuário com este e-mail." });

        var user = new IdentityUser { UserName = normalizedEmail, Email = normalizedEmail, EmailConfirmed = true };
        var result = await _users.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(new { message = "Não foi possível criar o usuário.", errors = result.Errors.Select(e => e.Description) });

        return Ok(await ToUserDto(user));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [EnableRateLimiting("auth")]
    public Task<ActionResult<LoginResponseDto>> Login(LoginDto dto) => LoginInternalAsync(dto, requireDesktopOrigin: false);

    // Rota exclusiva do aplicativo Desktop.
    // Somente contas marcadas como criadas pelo Desktop podem obter token por esta rota.
    [HttpPost("desktop-login")]
    [AllowAnonymous]
    [EnableRateLimiting("auth")]
    public Task<ActionResult<LoginResponseDto>> DesktopLogin(LoginDto dto) => LoginInternalAsync(dto, requireDesktopOrigin: true);

    private async Task<ActionResult<LoginResponseDto>> LoginInternalAsync(LoginDto dto, bool requireDesktopOrigin)
    {
        var email = dto.Email?.Trim().ToLowerInvariant() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(email) || email.Length > 180 || !System.Net.Mail.MailAddress.TryCreate(email, out _))
            return Unauthorized(new { message = "E-mail ou senha inválidos." });

        var user = await _users.FindByEmailAsync(email);
        if (user is null) return Unauthorized(new { message = "E-mail ou senha inválidos." });

        var result = await _signIn.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        if (!result.Succeeded) return Unauthorized(new { message = "E-mail ou senha inválidos." });

        if (requireDesktopOrigin)
        {
            var claims = await _users.GetClaimsAsync(user);
            var isDesktopUser = claims.Any(c =>
                c.Type == "created_by" &&
                string.Equals(c.Value, "desktop", StringComparison.OrdinalIgnoreCase));

            if (!isDesktopUser)
                return Unauthorized(new { message = "Este usuário não possui acesso ao Desktop." });
        }

        var roles = await _users.GetRolesAsync(user);
        var expires = DateTime.UtcNow.AddHours(8);
        var token = CreateToken(user, roles, expires);

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiresAtUtc = expires,
            User = new UserDto { Id = user.Id, Email = user.Email ?? string.Empty, Roles = roles }
        });
    }

    [HttpPut("email")]
    [Authorize]
    public async Task<ActionResult<UserDto>> UpdateEmail(UpdateEmailDto dto)
    {
        var user = await _users.GetUserAsync(User);
        if (user is null)
            return Unauthorized(new { message = "Usuário não encontrado." });

        var email = dto.Email?.Trim().ToLowerInvariant() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Informe o e-mail." });

        var existing = await _users.FindByEmailAsync(email);
        if (existing is not null && existing.Id != user.Id)
            return Conflict(new { message = "Já existe um usuário com este e-mail." });

        if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
        {
            var emailResult = await _users.SetEmailAsync(user, email);
            if (!emailResult.Succeeded)
                return BadRequest(new { message = "Não foi possível alterar o e-mail.", errors = emailResult.Errors.Select(e => e.Description) });

            var userNameResult = await _users.SetUserNameAsync(user, email);
            if (!userNameResult.Succeeded)
                return BadRequest(new { message = "Não foi possível atualizar o usuário.", errors = userNameResult.Errors.Select(e => e.Description) });
        }

        return Ok(await ToUserDto(user));
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> UpdateProfile(UpdateProfileDto dto)
    {
        var user = await _users.GetUserAsync(User);
        if (user is null)
            return Unauthorized(new { message = "Usuário não encontrado." });

        var email = dto.Email.Trim();
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest(new { message = "Informe o e-mail." });

        var existing = await _users.FindByEmailAsync(email);
        if (existing is not null && existing.Id != user.Id)
            return Conflict(new { message = "Já existe um usuário com este e-mail." });

        var wantsPasswordChange = !string.IsNullOrWhiteSpace(dto.NewPassword);
        if (wantsPasswordChange)
        {
            if (string.IsNullOrWhiteSpace(dto.CurrentPassword))
                return BadRequest(new { message = "Informe a senha atual para alterar a senha." });

            var passwordCheck = await _signIn.CheckPasswordSignInAsync(user, dto.CurrentPassword, false);
            if (!passwordCheck.Succeeded)
                return BadRequest(new { message = "A senha atual está incorreta." });
        }

        if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
        {
            var emailResult = await _users.SetEmailAsync(user, email);
            if (!emailResult.Succeeded)
                return BadRequest(new { message = "Não foi possível alterar o e-mail.", errors = emailResult.Errors.Select(e => e.Description) });

            var userNameResult = await _users.SetUserNameAsync(user, email);
            if (!userNameResult.Succeeded)
                return BadRequest(new { message = "Não foi possível atualizar o usuário.", errors = userNameResult.Errors.Select(e => e.Description) });
        }

        if (wantsPasswordChange)
        {
            var passwordResult = await _users.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!passwordResult.Succeeded)
                return BadRequest(new { message = "Não foi possível alterar a senha.", errors = passwordResult.Errors.Select(e => e.Description) });
        }

        return Ok(await ToUserDto(user));
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> Me()
    {
        var user = await _users.GetUserAsync(User);
        return user is null ? Unauthorized(new { message = "Usuário não encontrado." }) : Ok(await ToUserDto(user));
    }

    private async Task<UserDto> ToUserDto(IdentityUser user) => new()
    {
        Id = user.Id,
        Email = user.Email ?? string.Empty,
        Roles = await _users.GetRolesAsync(user)
    };

    private string CreateToken(IdentityUser user, IList<string> roles, DateTime expires)
    {
        var key = _configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(key) || Encoding.UTF8.GetByteCount(key) < 32)
            throw new InvalidOperationException("Jwt:Key não configurada ou muito curta.");
        var issuer = _configuration["Jwt:Issuer"] ?? "AtelieDaTransformacao";
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer, issuer, claims, expires: expires, signingCredentials: credentials));
    }
}
