namespace AtelieDaTransformacao.Application.ViewModels;

public sealed class CartItemViewModel
{
    public int ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Price * Quantity;
}
