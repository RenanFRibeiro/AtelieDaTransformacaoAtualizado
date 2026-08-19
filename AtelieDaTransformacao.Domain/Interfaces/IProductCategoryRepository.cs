using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Domain.Interfaces;

public interface IProductCategoryRepository
{
    Task<IReadOnlyList<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int id);
    Task AddAsync(ProductCategory category);
    Task UpdateAsync(ProductCategory category);
    Task<bool> DeleteAsync(int id);
}
