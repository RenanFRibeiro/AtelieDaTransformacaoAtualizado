using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll([FromQuery] string? search, [FromQuery] int? categoryId)
        => Ok(await _service.GetAllAsync(search, categoryId));

    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
        => (await _service.GetByIdAsync(id)) is { } p ? Ok(p) : NotFound(new { message = "Produto não encontrado." });

    [HttpGet("count")]
    public async Task<ActionResult<int>> Count() => Ok(await _service.CountAsync());

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var result = await _service.AddAsync(dto);
        return CreatedAtRoute("GetProductById", new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> Update(int id, UpdateProductDto dto)
        => (await _service.UpdateAsync(id, dto)) is { } p ? Ok(p) : NotFound(new { message = "Produto não encontrado." });

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound(new { message = "Produto não encontrado." });
}
