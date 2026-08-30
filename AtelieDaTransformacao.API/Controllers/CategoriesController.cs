using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AtelieDaTransformacao.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAll()
        {
            var categories = await _productCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductCategoryDto>> Create(
            [FromBody] CreateProductCategoryDto dto)
        {
            if (dto == null)
                return BadRequest("Os dados da categoria não podem ser nulos.");

            await _productCategoryService.AddAsync(dto);

            return Ok(new ProductCategoryDto
            {
                Name = dto.Name,
                Description = dto.Description
            });
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ProductCategoryDto>> Update(
            int id,
            [FromBody] UpdateProductCategoryDto dto)
        {
            if (dto == null)
                return BadRequest("Os dados da categoria não podem ser nulos.");

            var existing = await _productCategoryService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"A categoria com o ID {id} não foi encontrada." });

            var category = new ProductCategoryDto
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description
            };

            await _productCategoryService.UpdateAsync(category);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _productCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { message = $"A categoria com o ID {id} não foi encontrada." });
            }

            try
            {
                await _productCategoryService.DeleteAsync(id);
                return Ok(new { message = "Categoria removida com sucesso do Ateliê!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    message = "Não foi possível deletar a categoria. Verifique se não existem produtos vinculados a ela.",
                    details = ex.Message
                });
            }
        }
    }
}
