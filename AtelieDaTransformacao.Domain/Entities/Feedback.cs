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

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public string PublicName { get; set; } = "Cliente";
}