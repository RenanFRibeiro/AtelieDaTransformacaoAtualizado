using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IUserManagementService
{
    Task<IReadOnlyList<UserSummaryDto>> GetAllAsync();
    Task<bool> DeleteAsync(string id, string? currentUserId);
}
