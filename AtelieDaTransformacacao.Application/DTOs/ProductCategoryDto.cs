using System.ComponentModel.DataAnnotations;

namespace AtelieDaTransformacao.Application.DTOs;

public class ProductCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
public class CreateProductCategoryDto
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [StringLength(255)]
    public string Description { get; set; } = string.Empty;
}
public class UpdateProductCategoryDto : CreateProductCategoryDto { }
