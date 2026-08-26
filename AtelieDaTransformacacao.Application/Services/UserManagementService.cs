using System.Security.Claims;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AtelieDaTransformacao.Application.Services;

public sealed class UserManagementService : IUserManagementService
{
    private const string CreatedByClaimType = "created_by";
    private const string DesktopOrigin = "desktop";

    private readonly UserManager<IdentityUser> _users;

    public UserManagementService(UserManager<IdentityUser> users) => _users = users;

    public async Task<IReadOnlyList<UserSummaryDto>> GetAllAsync()
    {
        var result = new List<UserSummaryDto>();

        foreach (var user in _users.Users.OrderBy(x => x.Email))
        {
            // O painel do Desktop deve exibir somente contas criadas pelo próprio Desktop.
            var claims = await _users.GetClaimsAsync(user);
            if (!claims.Any(c => c.Type == CreatedByClaimType &&
                                 string.Equals(c.Value, DesktopOrigin, StringComparison.OrdinalIgnoreCase)))
                continue;

            var isActive = !(user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow);
            result.Add(new UserSummaryDto
            {
                Id = user.Id,
                Email = user.Email ?? user.UserName ?? string.Empty,
                Roles = await _users.GetRolesAsync(user),
                IsActive = isActive
            });
        }

        return result;
    }

    public async Task<UserDto?> CreateDesktopUserAsync(DesktopCreateUserDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            return null;

        var email = dto.Email.Trim();
        if (string.IsNullOrWhiteSpace(email))
            return null;

        var existing = await _users.FindByEmailAsync(email);
        if (existing is not null)
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");

        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await _users.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(" ", result.Errors.Select(e => e.Description)));

        var claimResult = await _users.AddClaimAsync(user, new Claim(CreatedByClaimType, DesktopOrigin));
        if (!claimResult.Succeeded)
        {
            await _users.DeleteAsync(user);
            throw new InvalidOperationException("Não foi possível registrar a origem do usuário criado pelo Desktop.");
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Roles = await _users.GetRolesAsync(user)
        };
    }

    public async Task<bool> DeleteAsync(string id, string? currentUserId)
    {
        if (string.IsNullOrWhiteSpace(id) || string.Equals(id, currentUserId, StringComparison.OrdinalIgnoreCase)) return false;
        var user = await _users.FindByIdAsync(id);
        if (user is null) return false;
        var result = await _users.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeactivateAsync(string id, string? currentUserId)
    {
        if (string.IsNullOrWhiteSpace(id) || string.Equals(id, currentUserId, StringComparison.OrdinalIgnoreCase)) return false;
        var user = await _users.FindByIdAsync(id);
        if (user is null) return false;
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;
        var result = await _users.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> ActivateAsync(string id, string? currentUserId)
    {
        if (string.IsNullOrWhiteSpace(id)) return false;
        var user = await _users.FindByIdAsync(id);
        if (user is null) return false;
        user.LockoutEnd = null;
        user.LockoutEnabled = true;
        var result = await _users.UpdateAsync(user);
        return result.Succeeded;
    }
}
