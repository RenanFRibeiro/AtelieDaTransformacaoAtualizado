using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface ICepService
{
    Task<CepDto?> BuscarEnderecoPorCepAsync(string cep);
}