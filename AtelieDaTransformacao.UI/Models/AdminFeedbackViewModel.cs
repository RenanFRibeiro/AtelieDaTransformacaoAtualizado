using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.UI.Models;

public sealed class AdminFeedbackViewModel
{
    public string? Status { get; set; }

    public string? Cliente { get; set; }

    public string? Produto { get; set; }

    public IReadOnlyList<AdminFeedbackItemViewModel> Feedbacks { get; set; }
        = Array.Empty<AdminFeedbackItemViewModel>();

    public int Pendentes => Feedbacks.Count(x => !x.IsAprovado);

    public int Aprovados => Feedbacks.Count(x => x.IsAprovado);
}

public sealed class AdminFeedbackItemViewModel
{
    public int Id { get; set; }
    public string Cliente { get; set; } = "Cliente";
    public string Email { get; set; } = string.Empty;
    public string Produto { get; set; } = "Produto";
    public int PedidoId { get; set; }
    public int Nota { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string? ImagemUrl { get; set; }
    public bool IsAnonimo { get; set; }
    public bool IsAprovado { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? AprovadoEm { get; set; }
}
