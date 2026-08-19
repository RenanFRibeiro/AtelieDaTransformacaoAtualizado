using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services;

public sealed class CategoriesApiService
{
    public Task<List<CategoryDto>?> GetAllAsync() => HttpClientHelper.GetAsync<List<CategoryDto>>("categories");
    public Task<CategoryDto?> CreateAsync(CategoryWriteDto dto) => HttpClientHelper.PostAsync<CategoryDto>("categories", dto);
    public Task<CategoryDto?> UpdateAsync(int id, CategoryWriteDto dto) => HttpClientHelper.PutAsync<CategoryDto>($"categories/{id}", dto);
    public Task DeleteAsync(int id) => HttpClientHelper.DeleteAsync($"categories/{id}");
}
