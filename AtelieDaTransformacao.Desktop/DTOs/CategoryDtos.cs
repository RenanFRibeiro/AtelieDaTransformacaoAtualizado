namespace AtelieDaTransformacao.Desktop.DTOs;

public sealed class CategoryDto { public int Id { get; set; } public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; }
public sealed class CategoryWriteDto { public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; }
