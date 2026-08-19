using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public string WhatsAppLink { get; set; } = string.Empty;
}

public class CreateProductDto
{
    [Required, StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [StringLength(255)]
    public string Description { get; set; } = string.Empty;
    [Range(0.01, 100000)]
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
    [Range(0, 10000)]
    public int StockQuantity { get; set; }
}

public class UpdateProductDto : CreateProductDto { }
