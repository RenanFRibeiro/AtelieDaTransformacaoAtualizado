using System.Globalization;
using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;
using AtelieDaTransformacao.Desktop.Services;

namespace AtelieDaTransformacao.Desktop.UserControls;

public partial class DashboardUserControl : UserControl
{
    private readonly ProductsApiService _products = new();
    private readonly CategoriesApiService _categories = new();
    private readonly UsersApiService _users = new();

    public DashboardUserControl()
    {
        InitializeComponent();

        _refreshButton.Click += async (_, _) => await LoadAsync();
        _recentRefreshButton.Click += async (_, _) => await LoadAsync();
        Load += async (_, _) => await LoadAsync();
        _cardsPanel.Resize += CardsPanel_Resize;
        CenterSummaryCards();
    }

    private async Task LoadAsync()
    {
        try
        {
            _statusLabel.Text = "Atualizando dados...";

            // Produtos e categorias podem ser consultados por qualquer usuário
            // autenticado no Desktop. O token da sessão é enviado automaticamente
            // pelo HttpClientHelper.
            var productsTask = _products.GetAllAsync();
            var categoriesTask = _categories.GetAllAsync();

            await Task.WhenAll(productsTask, categoriesTask);

            var products = await productsTask ?? new List<ProductDto>();
            var categories = await categoriesTask ?? new List<CategoryDto>();

            // O endpoint de usuários é restrito a administradores.
            // Portanto, um usuário comum não pode fazer essa chamada, pois um 403
            // interromperia o carregamento inteiro do Dashboard.
            var activeUsersCount = 0;
            if (SessionManager.IsAdmin)
            {
                var users = await _users.GetAllAsync() ?? new List<UserSummaryDto>();
                activeUsersCount = users.Count(x => x.IsActive);
            }

            // Produtos = quantidade de produtos cadastrados.
            // Estoque = soma das unidades disponíveis em todos os produtos.
            // Categorias = quantidade de categorias cadastradas.
            var productsCount = products.Count;
            var stockCount = products.Sum(x => x.StockQuantity);
            var categoriesCount = categories.Count;

            cardGamesLblNumero.Text = productsCount.ToString("N0");
            label2.Text = stockCount.ToString("N0");
            cardCategoriasLblNumero.Text = categoriesCount.ToString("N0");
            cardUsuariosLblNumero.Text = activeUsersCount.ToString("N0");

            // Mostra os 10 produtos mais recentes abaixo dos cards.
            _grid.DataSource = products
                .OrderByDescending(x => x.Id)
                .Take(10)
                .Select(x => new
                {
                    Produto = x.Title,
                    Categoria = x.CategoryName,
                    Preço = x.Price.ToString("C2", CultureInfo.CurrentCulture),
                    Estoque = x.StockQuantity,
                    Status = x.StockQuantity == 0
                        ? "Sem estoque"
                        : x.StockQuantity <= 5
                            ? "Estoque baixo"
                            : "Disponível"
                })
                .ToList();

            _statusLabel.Text = $"Atualizado em {DateTime.Now:dd/MM/yyyy HH:mm}";
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"Erro ao atualizar: {ex.Message}";
        }
    }

    private void CardsPanel_Resize(object? sender, EventArgs e)
    {
        CenterSummaryCards();
    }

    private void CenterSummaryCards()
    {
        const int cardWidth = 180;
        const int gap = 10;
        const int totalWidth = (cardWidth * 4) + (gap * 3);
        const int top = 12;

        var left = Math.Max(0, (_cardsPanel.ClientSize.Width - totalWidth) / 2);

        cardGames.Location = new Point(left, top);
        guna2Panel1.Location = new Point(left + cardWidth + gap, top);
        cardCategorias.Location = new Point(left + (cardWidth + gap) * 2, top);
        cardUsuarios.Location = new Point(left + (cardWidth + gap) * 3, top);
    }
}
