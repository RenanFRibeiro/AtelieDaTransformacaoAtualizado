using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs;

public sealed class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public sealed class RegisterDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public sealed class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
}

public sealed class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public UserDto User { get; set; } = new();
}

public sealed class UpdateEmailDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public sealed class UpdateProfileDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string CurrentPassword { get; set; } = string.Empty;

    [MinLength(6)]
    public string NewPassword { get; set; } = string.Empty;
}
