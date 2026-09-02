using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AtelieDaTransformacao.Application.ViewModels;

public sealed class FeedbackFormViewModel
{
    public int PedidoId { get; set; }

    public int ProdutoId { get; set; }

    public string PedidoNumero { get; set; } = string.Empty;

    public string ProdutoNome { get; set; } = string.Empty;

    public string ProdutoImagem { get; set; } = string.Empty;

    [Range(
        1,
        5,
        ErrorMessage =
            "Escolha uma nota entre 1 e 5 estrelas.")]
    [Display(Name = "Nota")]
    public int Nota { get; set; }

    [Required(
        ErrorMessage =
            "Escreva um comentário sobre o produto.")]
    [StringLength(
        2000,
        MinimumLength = 10,
        ErrorMessage =
            "O comentário deve ter entre 10 e 2000 caracteres.")]
    [Display(Name = "Comentário")]
    public string Comentario { get; set; } = string.Empty;

    [Display(Name = "Enviar como anônimo")]
    public bool IsAnonimo { get; set; }

    [Display(Name = "Foto do produto")]
    public IFormFile? Imagem { get; set; }
}

public sealed class OrderFeedbackItemViewModel
{
    public int PedidoId { get; set; }

    public string PedidoNumero { get; set; } = string.Empty;

    public int ProdutoId { get; set; }

    public string ProdutoNome { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public bool PodeAvaliar { get; set; }

    public FeedbackDisplayViewModel? Feedback { get; set; }
}

public sealed class FeedbackDisplayViewModel
{
    public int Id { get; set; }

    public int Nota { get; set; }

    public string Comentario { get; set; } = string.Empty;

    public string? ImagemUrl { get; set; }

    public bool IsAnonimo { get; set; }

    public DateTime DataCriacao { get; set; }
}

public sealed class FeedbackCardViewModel
{
    public int Id { get; set; }

    public string PublicName { get; set; } = "Cliente";

    public string ProdutoNome { get; set; } = string.Empty;

    public int Nota { get; set; }

    public string Comentario { get; set; } = string.Empty;

    public string? ImagemUrl { get; set; }

    public DateTime DataCriacao { get; set; }
}