using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

public sealed class CepService : ICepService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress ??= new Uri("https://viacep.com.br/");
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<CepDto?> BuscarEnderecoPorCepAsync(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            return null;

        var cepLimpo = Regex.Replace(cep, @"\D", "");

        if (cepLimpo.Length != 8)
            return null;

        try
        {
            var response = await _httpClient.GetAsync($"ws/{cepLimpo}/json/");

            if (!response.IsSuccessStatusCode)
                return null;

            var resultado = await response.Content.ReadFromJsonAsync<CepDto>(JsonOptions);

            if (resultado is null || resultado.Erro)
                return null;

            return resultado;
        }
        catch (Exception)
        {
            return null;
        }
    }
}