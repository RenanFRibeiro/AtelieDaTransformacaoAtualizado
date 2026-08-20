using System.Linq;

namespace AtelieDaTransformacao.Application.ViewModels;

public sealed class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();
    public decimal Total => Items.Sum(x => x.Subtotal);
    public int TotalItems => Items.Sum(x => x.Quantity);
}
