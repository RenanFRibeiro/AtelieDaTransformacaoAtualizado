using System.ComponentModel.DataAnnotations.Schema;

namespace AtelieDaTransformacao.Domain.Entities;

public sealed class Feedback
{
    public int Id { get; set; }

    public string UsuarioId { get; set; } = string.Empty;

    public int ProdutoId { get; set; }

    public int PedidoId { get; set; }

    public int Nota { get; set; }

    public string Comentario { get; set; } = string.Empty;

    public string? ImagemUrl { get; set; }

    public bool IsAnonimo { get; set; }

    /// <summary>
    /// Novo feedback entra sempre pendente. Somente o administrador
    /// pode alterar este campo para liberar a publicação.
    /// </summary>
    public bool IsAprovado { get; set; }

    public DateTime? AprovadoEm { get; set; }

    public string? AprovadoPor { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public string PublicName { get; set; } = "Cliente";
}
