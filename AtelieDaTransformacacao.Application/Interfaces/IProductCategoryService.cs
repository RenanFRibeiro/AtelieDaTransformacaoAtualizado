using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IProductCategoryService
{
    Task<IReadOnlyList<ProductCategoryDto>> GetAllAsync();
    Task<ProductCategoryDto?> GetByIdAsync(int id);
    Task<ProductCategoryDto> AddAsync(CreateProductCategoryDto dto);
    Task<ProductCategoryDto?> UpdateAsync(int id, UpdateProductCategoryDto dto);
    Task<bool> DeleteAsync(int id);
}
