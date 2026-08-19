using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services;

public sealed class UsersApiService
{
    public Task<List<UserSummaryDto>?> GetAllAsync() => HttpClientHelper.GetAsync<List<UserSummaryDto>>("users");
    public Task DeleteAsync(string id) => HttpClientHelper.DeleteAsync($"users/{Uri.EscapeDataString(id)}");
}
