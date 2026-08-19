using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services;

public sealed class ProductsApiService
{
    public Task<List<ProductDto>?> GetAllAsync(string? search = null, int? categoryId = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(search)) query.Add($"search={Uri.EscapeDataString(search)}");
        if (categoryId.HasValue && categoryId.Value > 0) query.Add($"categoryId={categoryId.Value}");
        var url = query.Count == 0 ? "products" : "products?" + string.Join("&", query);
        return HttpClientHelper.GetAsync<List<ProductDto>>(url);
    }
    public async Task<int> CountAsync() => await HttpClientHelper.GetAsync<int>("products/count");
    public Task<ProductDto?> CreateAsync(ProductWriteDto dto) => HttpClientHelper.PostAsync<ProductDto>("products", dto);
    public Task<ProductDto?> UpdateAsync(int id, ProductWriteDto dto) => HttpClientHelper.PutAsync<ProductDto>($"products/{id}", dto);
    public Task DeleteAsync(int id) => HttpClientHelper.DeleteAsync($"products/{id}");
}
