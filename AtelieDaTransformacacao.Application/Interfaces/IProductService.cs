using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IProductService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync(string? search = null, int? categoryId = null);
    Task<ProductDto?> GetByIdAsync(int id);
    Task<IReadOnlyList<ProductDto>> GetFeaturedAsync();
    Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(int categoryId);
    Task<int> CountAsync();
    Task<ProductDto> AddAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DebitStockAsync(int productId, int quantity);
}
