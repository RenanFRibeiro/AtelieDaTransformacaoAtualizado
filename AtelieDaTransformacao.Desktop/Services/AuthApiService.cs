using AtelieDaTransformacao.Desktop.DTOs;
using AtelieDaTransformacao.Desktop.Helpers;

namespace AtelieDaTransformacao.Desktop.Services;

public sealed class AuthApiService
{
    // O Desktop NÃO usa /auth/login. Usa uma rota exclusiva que rejeita contas criadas pela UI.
    public Task<LoginResponseDto?> LoginAsync(string email, string password) =>
        HttpClientHelper.PostAsync<LoginResponseDto>(
            "auth/desktop-login",
            new LoginRequestDto { Email = email, Password = password });

    public Task<UserDto?> MeAsync() => HttpClientHelper.GetAsync<UserDto>("auth/me");
    public Task<UserDto?> RegisterAsync(RegisterRequestDto dto) => HttpClientHelper.PostAsync<UserDto>("auth/register", dto);
    public Task<UserDto?> UpdateEmailAsync(UpdateEmailRequestDto dto) => HttpClientHelper.PutAsync<UserDto>("auth/email", dto);
    public Task<UserDto?> UpdateProfileAsync(UpdateProfileRequestDto dto) => HttpClientHelper.PutAsync<UserDto>("auth/profile", dto);
}
