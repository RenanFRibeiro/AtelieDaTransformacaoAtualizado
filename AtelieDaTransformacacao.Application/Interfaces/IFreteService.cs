using AtelieDaTransformacao.Application.DTOs;

namespace AtelieDaTransformacao.Application.Interfaces;

/// <summary>
/// Serviço de cálculo de frete por CEP de destino.
/// </summary>
public interface IFreteService
{
    /// <summary>
    /// Calcula o valor do frete e o prazo estimado de entrega
    /// com base no CEP de destino.
    /// </summary>
    Task<FreteResult> CalcularFreteAsync(string cepDestino);
}