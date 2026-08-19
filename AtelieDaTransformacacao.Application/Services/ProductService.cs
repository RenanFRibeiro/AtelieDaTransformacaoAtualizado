using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IWhatsAppService _whatsApp;

    public ProductService(IProductRepository repository, IWhatsAppService whatsApp)
    {
        _repository = repository;
        _whatsApp = whatsApp;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(string? search = null, int? categoryId = null)
    {
        var products = await _repository.GetAllAsync();
        if (!string.IsNullOrWhiteSpace(search))
            products = products.Where(p => p.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                                        || p.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
        if (categoryId.HasValue)
            products = products.Where(p => p.CategoryId == categoryId.Value);

        return products.Select(ToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var p = await _repository.GetByIdAsync(id);
        return p is null ? null : ToDto(p);
    }

    public async Task<IReadOnlyList<ProductDto>> GetFeaturedAsync()
        => (await _repository.GetFeaturedAsync()).Select(ToDto).ToList();

    public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(int categoryId)
        => (await _repository.GetByCategoryAsync(categoryId)).Select(ToDto).ToList();

    public Task<int> CountAsync() => _repository.CountAsync();

    public async Task<ProductDto> AddAsync(CreateProductDto dto)
    {
        Validate(dto.Title, dto.Price, dto.StockQuantity, dto.CategoryId);
        var p = new Product
        {
            Title = dto.Title.Trim(),
            Description = dto.Description?.Trim() ?? string.Empty,
            Price = dto.Price,
            Image = dto.Image?.Trim() ?? string.Empty,
            CategoryId = dto.CategoryId,
            IsFeatured = dto.IsFeatured,
            StockQuantity = dto.StockQuantity
        };
        await _repository.AddAsync(p);
        return ToDto(p);
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
    {
        Validate(dto.Title, dto.Price, dto.StockQuantity, dto.CategoryId);
        var p = await _repository.GetByIdAsync(id);
        if (p is null) return null;

        p.Title = dto.Title.Trim();
        p.Description = dto.Description?.Trim() ?? string.Empty;
        p.Price = dto.Price;
        p.Image = dto.Image?.Trim() ?? string.Empty;
        p.CategoryId = dto.CategoryId;
        p.IsFeatured = dto.IsFeatured;
        p.StockQuantity = dto.StockQuantity;
        await _repository.UpdateAsync(p);
        return ToDto(p);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (await _repository.GetByIdAsync(id) is null) return false;
        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> DebitStockAsync(int productId, int quantity)
    {
        if (quantity <= 0) return false;
        var p = await _repository.GetByIdAsync(productId);
        if (p is null || p.StockQuantity < quantity) return false;
        p.StockQuantity -= quantity;
        await _repository.UpdateAsync(p);
        return true;
    }

    private ProductDto ToDto(Product p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Price = p.Price,
        Image = p.Image,
        CategoryId = p.CategoryId,
        CategoryName = p.Category?.Name ?? string.Empty,
        IsFeatured = p.IsFeatured,
        StockQuantity = p.StockQuantity,
        IsAvailable = p.StockQuantity > 0,
        WhatsAppLink = _whatsApp.GenerateProductInquiryLink(p.Title, p.Price)
    };

    private static void Validate(string title, decimal price, int stock, int categoryId)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("O título é obrigatório.");
        if (title.Trim().Length > 180) throw new ArgumentException("O título deve ter no máximo 180 caracteres.");
        if (price <= 0) throw new ArgumentException("O preço deve ser maior que zero.");
        if (stock < 0) throw new ArgumentException("O estoque não pode ser negativo.");
        if (categoryId <= 0) throw new ArgumentException("A categoria é obrigatória.");
    }
}
