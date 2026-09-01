using System.Text.RegularExpressions;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

public sealed class FreteService : IFreteService
{
    public Task<List<FreteResult>> CalcularFreteAsync(FreteRequestDto request)
    {
        var opcoes = new List<FreteResult>();

        if (string.IsNullOrWhiteSpace(request.CepDestino))
        {
            opcoes.Add(new FreteResult { Disponivel = false, Descricao = "CEP não informado." });
            return Task.FromResult(opcoes);
        }

        // Limpeza dos CEPs
        var cepDestinoLimpo = Regex.Replace(request.CepDestino, @"\D", "");
        var cepOrigemLimpo = Regex.Replace(request.CepOrigem ?? "01000000", @"\D", "");

        if (cepDestinoLimpo.Length != 8 || !int.TryParse(cepDestinoLimpo, out var cepNumerico))
        {
            opcoes.Add(new FreteResult { Disponivel = false, Descricao = "CEP inválido." });
            return Task.FromResult(opcoes);
        }

        int.TryParse(cepOrigemLimpo, out var cepOrigemNumerico);

        // Fator de distância simples (simulando a diferença de zonas postais)
        var diferencaCep = Math.Abs(cepNumerico - cepOrigemNumerico);
        var fatorDistancia = diferencaCep / 1000000.0m;

        // Cálculo de peso cubado (padrão Correios / Melhor Envio: C x L x A / 6000)
        var volumeCubico = request.AlturaCm * request.LarguraCm * request.ComprimentoCm;
        var pesoCubado = volumeCubico / 6000m;

        // Cobra-se pelo maior entre o peso físico e o peso cúbico
        var pesoConsiderado = Math.Max(request.PesoKg, pesoCubado);

        // --- CÁLCULO PAC ---
        var valorPac = 15.00m + (pesoConsiderado * 3.50m) + (fatorDistancia * 0.4m);
        var prazoPac = 5 + (int)Math.Ceiling(fatorDistancia / 10);

        opcoes.Add(new FreteResult
        {
            Nome = "PAC",
            Valor = Math.Round(valorPac, 2),
            PrazoEstimadoDias = prazoPac,
            Descricao = "Entrega Econômica",
            Disponivel = true
        });

        // --- CÁLCULO SEDEX ---
        var valorSedex = 25.00m + (pesoConsiderado * 6.50m) + (fatorDistancia * 0.7m);
        var prazoSedex = 2 + (int)Math.Ceiling(fatorDistancia / 20);

        opcoes.Add(new FreteResult
        {
            Nome = "Sedex",
            Valor = Math.Round(valorSedex, 2),
            PrazoEstimadoDias = prazoSedex,
            Descricao = "Entrega Expressa",
            Disponivel = true
        });

        return Task.FromResult(opcoes);
    }
}