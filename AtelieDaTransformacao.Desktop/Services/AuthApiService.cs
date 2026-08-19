using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services;

public sealed class AuthApiService
{
    public Task<LoginResponseDto?> LoginAsync(string email, string password) => HttpClientHelper.PostAsync<LoginResponseDto>("auth/login", new LoginRequestDto { Email = email, Password = password });
    public Task<UserDto?> MeAsync() => HttpClientHelper.GetAsync<UserDto>("auth/me");
    public Task<UserDto?> RegisterAsync(RegisterRequestDto dto) => HttpClientHelper.PostAsync<UserDto>("auth/register", dto);
}
