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
            result.Add(new UserSummaryDto { Id = user.Id, Email = user.Email ?? user.UserName ?? string.Empty, Roles = await _users.GetRolesAsync(user) });
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
}
