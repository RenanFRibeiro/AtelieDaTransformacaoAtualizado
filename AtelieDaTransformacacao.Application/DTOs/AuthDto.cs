using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs;

public sealed class LoginDto
{
    [Required, EmailAddress]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;
}

public sealed class RegisterDto
{
    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 80 caracteres.")]
    [Display(Name = "Nome")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu sobrenome.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O sobrenome deve ter entre 2 e 120 caracteres.")]
    [Display(Name = "Sobrenome")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu telefone/WhatsApp.")]
    [Phone(ErrorMessage = "Informe um telefone válido.")]
    [StringLength(30)]
    [Display(Name = "Telefone / WhatsApp")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu endereço.")]
    [StringLength(180, MinimumLength = 3, ErrorMessage = "Informe um endereço válido.")]
    [Display(Name = "Endereço")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o número.")]
    [StringLength(20)]
    [Display(Name = "Número")]
    public string AddressNumber { get; set; } = string.Empty;

    [StringLength(80)]
    [Display(Name = "Complemento")]
    public string Complement { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o bairro.")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Bairro")]
    public string District { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Cidade")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(2, MinimumLength = 2,
        ErrorMessage = "Use a UF com 2 letras.")]
    [RegularExpression("^[A-Za-z]{2}$",
        ErrorMessage = "Informe a UF com 2 letras.")]
    [Display(Name = "Estado")]
    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CEP.")]
    [StringLength(9, MinimumLength = 8,
        ErrorMessage = "Informe um CEP válido.")]
    [RegularExpression("^\\d{5}-?\\d{3}$",
        ErrorMessage = "Informe um CEP válido.")]
    [Display(Name = "CEP")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(180)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Crie uma senha.")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "A senha deve ter pelo menos 8 caracteres.")]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+$",
        ErrorMessage = "A senha deve conter letra maiúscula, letra minúscula, número e caractere especial.")]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme sua senha.")]
    [Compare(nameof(Password),
        ErrorMessage = "As senhas não coincidem.")]
    [Display(Name = "Confirmar senha")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true",
        ErrorMessage = "Você precisa aceitar os Termos de Uso e a Política de Privacidade.")]
    [Display(Name = "Aceito os termos")]
    public bool AcceptTerms { get; set; }
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
    public string Email { get; set; } = string.Empty;

    public string? CurrentPassword { get; set; }

    public string? NewPassword { get; set; }
}

public sealed class DesktopCreateUserDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}