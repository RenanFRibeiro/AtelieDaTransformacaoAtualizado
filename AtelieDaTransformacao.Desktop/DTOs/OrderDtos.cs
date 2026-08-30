namespace AtelieDaTransformacao.Desktop.DTOs;

/// <summary>
/// Status usados pela interface desktop para representar o mesmo fluxo
/// existente no domínio: Criado -> Pendente -> Aprovado -> Separação ->
/// Faturado -> Enviado -> Entregue -> Cancelado.
/// </summary>
public enum OrderStatus
{
    Created = 0,
    Pending = 1,
    Approved = 2,
    Separation = 3,
    Invoiced = 4,
    Shipped = 5,
    Delivered = 6,
    Cancelado = 7
}

/// <summary>
/// Modelo utilizado pelo Grid e pela janela de detalhes do pedido.
/// </summary>
public sealed class OrderListItemDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime LastUpdate { get; set; }
}
