using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.UI.Models;

public class QuoteRequestViewModel
{
    [Required(ErrorMessage = "Informe seu nome.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu WhatsApp.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o tipo de peça desejada.")]
    public string ProductType { get; set; } = string.Empty;

    public string? Measurements { get; set; }
    public string? Material { get; set; }

    [Required(ErrorMessage = "Conte um pouco sobre o projeto desejado.")]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
}
