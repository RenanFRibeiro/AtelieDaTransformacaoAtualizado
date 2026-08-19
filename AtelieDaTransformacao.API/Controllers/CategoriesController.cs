using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly IProductCategoryService _service;
    public CategoriesController(IProductCategoryService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductCategoryDto>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductCategoryDto>> GetById(int id)
        => (await _service.GetByIdAsync(id)) is { } c ? Ok(c) : NotFound();

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductCategoryDto>> Create(CreateProductCategoryDto dto) => Ok(await _service.AddAsync(dto));

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductCategoryDto>> Update(int id, UpdateProductCategoryDto dto)
        => (await _service.UpdateAsync(id, dto)) is { } c ? Ok(c) : NotFound();

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
