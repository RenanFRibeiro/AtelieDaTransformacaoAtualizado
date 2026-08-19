using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.Infrastructure.Repositories;

public sealed class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly AtelieDaTransformacaoDbContext _context;
    public ProductCategoryRepository(AtelieDaTransformacaoDbContext context) => _context = context;

    public async Task<IReadOnlyList<ProductCategory>> GetAllAsync()
        => await _context.ProductCategories.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

    public Task<ProductCategory?> GetByIdAsync(int id)
        => _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(ProductCategory category)
    {
        await _context.ProductCategories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductCategory category)
    {
        _context.ProductCategories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.ProductCategories.FindAsync(id);
        if (category is null) return false;
        if (await _context.Products.AnyAsync(x => x.CategoryId == id))
            throw new InvalidOperationException("Não é possível excluir uma categoria que possui produtos.");
        _context.ProductCategories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
