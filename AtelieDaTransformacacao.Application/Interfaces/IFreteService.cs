using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

/// <summary>
/// Serviço de cálculo de frete baseado em volume, peso e distâncias.
/// </summary>
public interface IFreteService
{
    /// <summary>
    /// Calcula as opções disponíveis de frete com base na origem, destino, dimensões e peso.
    /// </summary>
    Task<List<FreteResult>> CalcularFreteAsync(FreteRequestDto request);
}