using System.Text.RegularExpressions;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

public sealed class FreteService : IFreteService
{
    private const decimal MinimumWeightKg = 0.1m;
    private const decimal DefaultWeightKg = 1.5m;
    private const decimal DefaultHeightCm = 25m;
    private const decimal DefaultWidthCm = 25m;
    private const decimal DefaultLengthCm = 20m;

    public Task<List<FreteResult>> CalcularFreteAsync(FreteRequestDto request)
    {
        if (request is null)
            return Task.FromResult(Unavailable("Dados de frete inválidos."));

        var cepDestino = Regex.Replace(request.CepDestino ?? string.Empty, @"\D", "");
        var cepOrigem = Regex.Replace(request.CepOrigem ?? "01000000", @"\D", "");

        if (cepDestino.Length != 8 || !long.TryParse(cepDestino, out var destino))
            return Task.FromResult(InvalidCep());

        if (cepOrigem.Length != 8 || !long.TryParse(cepOrigem, out var origem))
            origem = 1000000;

        var weight = request.PesoKg > 0 ? request.PesoKg : DefaultWeightKg;
        var height = request.AlturaCm > 0 ? request.AlturaCm : DefaultHeightCm;
        var width = request.LarguraCm > 0 ? request.LarguraCm : DefaultWidthCm;
        var length = request.ComprimentoCm > 0 ? request.ComprimentoCm : DefaultLengthCm;

        if (weight < MinimumWeightKg || height <= 0 || width <= 0 || length <= 0 ||
            weight > 100 || height > 200 || width > 200 || length > 300)
        {
            return Task.FromResult(Unavailable("Peso ou dimensões fora dos limites permitidos."));
        }

        var volume = height * width * length;
        var volumetricWeight = volume / 6000m;
        var consideredWeight = Math.Max(weight, volumetricWeight);

        // Estimativa interna: não representa cotação oficial dos Correios.
        var distanceFactor = Math.Min(Math.Abs(destino - origem) / 1_000_000m, 100m);

        var pac = new FreteResult
        {
            Nome = "PAC",
            Valor = Math.Round(15m + consideredWeight * 3.50m + distanceFactor * 0.40m, 2),
            PrazoEstimadoDias = Math.Clamp(5 + (int)Math.Ceiling(distanceFactor / 10m), 1, 30),
            Descricao = "Entrega econômica",
            Disponivel = true
        };

        var sedex = new FreteResult
        {
            Nome = "Sedex",
            Valor = Math.Round(25m + consideredWeight * 6.50m + distanceFactor * 0.70m, 2),
            PrazoEstimadoDias = Math.Clamp(2 + (int)Math.Ceiling(distanceFactor / 20m), 1, 20),
            Descricao = "Entrega expressa",
            Disponivel = true
        };

        return Task.FromResult(new List<FreteResult> { pac, sedex });
    }

    private static List<FreteResult> InvalidCep() => new()
    {
        new FreteResult { Disponivel = false, Descricao = "CEP inválido." }
    };

    private static List<FreteResult> Unavailable(string message) => new()
    {
        new FreteResult { Disponivel = false, Descricao = message }
    };
}
