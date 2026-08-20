using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AtelieDaTransformacao.Application.Services;

public sealed class UserManagementService : IUserManagementService
{
    private readonly UserManager<IdentityUser> _users;
    public UserManagementService(UserManager<IdentityUser> users) => _users = users;

    public async Task<IReadOnlyList<UserSummaryDto>> GetAllAsync()
    {
        var result = new List<UserSummaryDto>();
        foreach (var user in _users.Users.OrderBy(x => x.Email))
        {
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
