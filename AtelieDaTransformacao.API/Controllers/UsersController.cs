using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AtelieDaTransformacao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserManagementService _service;
    public UsersController(IUserManagementService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserSummaryDto>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost("create-desktop")]
    public async Task<ActionResult<UserDto>> CreateDesktop(DesktopCreateUserDto dto)
    {
        try
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest(new { message = "As senhas não coincidem." });

            var user = await _service.CreateDesktopUserAsync(dto);
            return user is null
                ? BadRequest(new { message = "Não foi possível criar o usuário." })
                : Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var currentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _service.DeleteAsync(id, currentId) ? NoContent() : BadRequest(new { message = "Usuário não encontrado ou operação não permitida." });
    }

    [HttpPost("deactivate/{id}")]
    public async Task<IActionResult> Deactivate(string id)
    {
        var currentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _service.DeactivateAsync(id, currentId) ? NoContent() : BadRequest(new { message = "Não foi possível desativar o usuário." });
    }

    [HttpPost("activate/{id}")]
    public async Task<IActionResult> Activate(string id)
    {
        var currentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _service.ActivateAsync(id, currentId) ? NoContent() : BadRequest(new { message = "Não foi possível ativar o usuário." });
    }
}
