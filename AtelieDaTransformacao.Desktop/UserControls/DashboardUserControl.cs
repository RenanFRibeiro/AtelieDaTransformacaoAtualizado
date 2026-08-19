using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Services;
using AtelieDaTransformacao.Desktop.Themes;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class DashboardUserControl : UserControl
{
    private readonly ProductsApiService _products = new();
    private readonly CategoriesApiService _categories = new();

    public DashboardUserControl()
    {
        InitializeComponent();
        _refreshButton.Click += async (_, _) => await LoadAsync();
        Load += async (_, _) => await LoadAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            _statusLabel.Text = "Atualizando dados...";
            var products = await _products.GetAllAsync() ?? new List<ProductDto>();
            var categories = await _categories.GetAllAsync() ?? new List<CategoryDto>();
            var featured = products.Count(x => x.IsFeatured);
            var stock = products.Sum(x => x.StockQuantity);
            var low = products.Count(x => x.StockQuantity > 0 && x.StockQuantity <= 5);

            _productsValueLabel.Text = products.Count.ToString();
            _stockValueLabel.Text = stock.ToString();
            _categoriesValueLabel.Text = categories.Count.ToString();
            _lowStockValueLabel.Text = low.ToString();
            _featuredValueLabel.Text = featured.ToString();

            _grid.DataSource = products.OrderByDescending(x => x.Id).Take(10)
                .Select(x => new
                {
                    Produto = x.Title,
                    Categoria = x.CategoryName,
                    Preço = x.Price.ToString("C2"),
                    Estoque = x.StockQuantity,
                    Status = x.StockQuantity == 0 ? "Sem estoque" : x.StockQuantity <= 5 ? "Estoque baixo" : "Disponível"
                }).ToList();
            _statusLabel.Text = $"Atualizado em {DateTime.Now:dd/MM/yyyy HH:mm}";
        }
        catch (Exception ex)
        {
            _statusLabel.Text = ex.Message;
        }
    }
}
