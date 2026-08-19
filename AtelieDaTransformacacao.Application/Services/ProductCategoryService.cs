using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

public sealed class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _repo;
    public ProductCategoryService(IProductCategoryRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<ProductCategoryDto>> GetAllAsync()
        => (await _repo.GetAllAsync()).Select(ToDto).ToList();

    public async Task<ProductCategoryDto?> GetByIdAsync(int id)
    {
        var c = await _repo.GetByIdAsync(id);
        return c is null ? null : ToDto(c);
    }

    public async Task<ProductCategoryDto> AddAsync(CreateProductCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("O nome da categoria é obrigatório.");
        var c = new ProductCategory { Name = dto.Name.Trim(), Description = dto.Description?.Trim() ?? string.Empty };
        await _repo.AddAsync(c);
        return ToDto(c);
    }

    public async Task<ProductCategoryDto?> UpdateAsync(int id, UpdateProductCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("O nome da categoria é obrigatório.");
        var c = await _repo.GetByIdAsync(id);
        if (c is null) return null;
        c.Name = dto.Name.Trim();
        c.Description = dto.Description?.Trim() ?? string.Empty;
        await _repo.UpdateAsync(c);
        return ToDto(c);
    }

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    private static ProductCategoryDto ToDto(ProductCategory c) => new() { Id = c.Id, Name = c.Name, Description = c.Description };
}
