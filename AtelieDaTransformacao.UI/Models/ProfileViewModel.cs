using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.UI.Models;

public sealed class ProfileViewModel
{
    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(80, MinimumLength = 2)]
    [Display(Name = "Nome")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu sobrenome.")]
    [StringLength(120, MinimumLength = 2)]
    [Display(Name = "Sobrenome")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu telefone/WhatsApp.")]
    [Phone(ErrorMessage = "Informe um telefone válido.")]
    [StringLength(30)]
    [Display(Name = "Telefone / WhatsApp")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu endereço.")]
    [StringLength(180)]
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
    [StringLength(100)]
    [Display(Name = "Bairro")]
    public string District { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    [StringLength(100)]
    [Display(Name = "Cidade")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(2, MinimumLength = 2)]
    [RegularExpression("^[A-Za-z]{2}$", ErrorMessage = "Informe a UF com 2 letras.")]
    [Display(Name = "Estado")]
    public string State { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CEP.")]
    [StringLength(9)]
    [RegularExpression("^\\d{5}-?\\d{3}$", ErrorMessage = "Informe um CEP válido.")]
    [Display(Name = "CEP")]
    public string PostalCode { get; set; } = string.Empty;
}


public sealed class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Informe sua senha atual.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha atual")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a nova senha.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A nova senha deve ter pelo menos 6 caracteres.")]
    [DataType(DataType.Password)]
    [Display(Name = "Nova senha")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a nova senha.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "As senhas não coincidem.")]
    [Display(Name = "Confirmar nova senha")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
