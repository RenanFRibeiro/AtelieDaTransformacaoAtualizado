using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IUserManagementService
{
    Task<IReadOnlyList<UserSummaryDto>> GetAllAsync();
    Task<bool> DeleteAsync(string id, string? currentUserId);
    Task<bool> DeactivateAsync(string id, string? currentUserId);
    Task<bool> ActivateAsync(string id, string? currentUserId);
}
