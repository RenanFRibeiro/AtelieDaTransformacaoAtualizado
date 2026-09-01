using System.Text.RegularExpressions;
using AtelieDaTransformacao.Application.DTOs;
using AtelieDaTransformacao.Application.Interfaces;

namespace AtelieDaTransformacao.Application.Services;

/// <summary>
/// Implementação do serviço de cálculo de frete por região.
/// Usa uma tabela fixa baseada nos primeiros dígitos do CEP para determinar
/// a região e aplicar a tarifa correspondente.
///
/// Faixas de CEP por estado:
/// https://blog.correios.com.br/faixas-de-cep/
///
/// Para integrar com Correios ou Melhor Envio no futuro,
/// basta criar uma nova implementação de IFreteService.
/// </summary>
public sealed class FreteService : IFreteService
{
    // =========================================================
    // TABELA DE FRETE POR REGIÃO
    // Ajuste os valores conforme a necessidade do ateliê.
    // =========================================================

    private static readonly List<FaixaFrete> Faixas = new()
    {
        // São Paulo — Capital e Grande SP (CEPs 01xxx a 09xxx)
        new FaixaFrete
        {
            CepInicio = 01000000,
            CepFim    = 09999999,
            Valor     = 15.00m,
            Prazo     = 2,
            Descricao = "São Paulo - Capital e Grande SP"
        },

        // Interior de SP (CEPs 10xxx a 19xxx)
        new FaixaFrete
        {
            CepInicio = 10000000,
            CepFim    = 19999999,
            Valor     = 25.00m,
            Prazo     = 4,
            Descricao = "Interior de São Paulo"
        },

        // Rio de Janeiro (CEPs 20xxx a 28xxx)
        new FaixaFrete
        {
            CepInicio = 20000000,
            CepFim    = 28999999,
            Valor     = 35.00m,
            Prazo     = 5,
            Descricao = "Rio de Janeiro"
        },

        // Espírito Santo (CEPs 29xxx)
        new FaixaFrete
        {
            CepInicio = 29000000,
            CepFim    = 29999999,
            Valor     = 35.00m,
            Prazo     = 5,
            Descricao = "Espírito Santo"
        },

        // Minas Gerais (CEPs 30xxx a 39xxx)
        new FaixaFrete
        {
            CepInicio = 30000000,
            CepFim    = 39999999,
            Valor     = 35.00m,
            Prazo     = 5,
            Descricao = "Minas Gerais"
        },

        // Bahia (CEPs 40xxx a 48xxx)
        new FaixaFrete
        {
            CepInicio = 40000000,
            CepFim    = 48999999,
            Valor     = 50.00m,
            Prazo     = 8,
            Descricao = "Bahia"
        },

        // Sergipe (CEPs 49xxx)
        new FaixaFrete
        {
            CepInicio = 49000000,
            CepFim    = 49999999,
            Valor     = 50.00m,
            Prazo     = 8,
            Descricao = "Sergipe"
        },

        // Pernambuco (CEPs 50xxx a 56xxx)
        new FaixaFrete
        {
            CepInicio = 50000000,
            CepFim    = 56999999,
            Valor     = 50.00m,
            Prazo     = 8,
            Descricao = "Pernambuco"
        },

        // Alagoas (CEPs 57xxx)
        new FaixaFrete
        {
            CepInicio = 57000000,
            CepFim    = 57999999,
            Valor     = 50.00m,
            Prazo     = 8,
            Descricao = "Alagoas"
        },

        // Paraíba (CEPs 58xxx)
        new FaixaFrete
        {
            CepInicio = 58000000,
            CepFim    = 58999999,
            Valor     = 50.00m,
            Prazo     = 8,
            Descricao = "Paraíba"
        },

        // Rio Grande do Norte (CEPs 59xxx)
        new FaixaFrete
        {
            CepInicio = 59000000,
            CepFim    = 59999999,
            Valor     = 55.00m,
            Prazo     = 10,
            Descricao = "Rio Grande do Norte"
        },

        // Ceará (CEPs 60xxx a 63xxx)
        new FaixaFrete
        {
            CepInicio = 60000000,
            CepFim    = 63999999,
            Valor     = 55.00m,
            Prazo     = 10,
            Descricao = "Ceará"
        },

        // Piauí (CEPs 64xxx)
        new FaixaFrete
        {
            CepInicio = 64000000,
            CepFim    = 64999999,
            Valor     = 55.00m,
            Prazo     = 10,
            Descricao = "Piauí"
        },

        // Maranhão (CEPs 65xxx)
        new FaixaFrete
        {
            CepInicio = 65000000,
            CepFim    = 65999999,
            Valor     = 55.00m,
            Prazo     = 10,
            Descricao = "Maranhão"
        },

        // Pará (CEPs 66xxx a 68xxx)
        new FaixaFrete
        {
            CepInicio = 66000000,
            CepFim    = 68899999,
            Valor     = 55.00m,
            Prazo     = 12,
            Descricao = "Pará"
        },

        // Amapá (CEPs 689xx)
        new FaixaFrete
        {
            CepInicio = 68900000,
            CepFim    = 68999999,
            Valor     = 60.00m,
            Prazo     = 15,
            Descricao = "Amapá"
        },

        // Amazonas / Roraima / Acre (CEPs 69xxx)
        new FaixaFrete
        {
            CepInicio = 69000000,
            CepFim    = 69999999,
            Valor     = 60.00m,
            Prazo     = 15,
            Descricao = "Amazonas / Roraima / Acre"
        },

        // Distrito Federal / Goiás (CEPs 70xxx a 76xxx)
        new FaixaFrete
        {
            CepInicio = 70000000,
            CepFim    = 76999999,
            Valor     = 45.00m,
            Prazo     = 7,
            Descricao = "Distrito Federal / Goiás / Tocantins"
        },

        // Mato Grosso (CEPs 78xxx)
        new FaixaFrete
        {
            CepInicio = 78000000,
            CepFim    = 78999999,
            Valor     = 45.00m,
            Prazo     = 7,
            Descricao = "Mato Grosso"
        },

        // Mato Grosso do Sul (CEPs 79xxx)
        new FaixaFrete
        {
            CepInicio = 79000000,
            CepFim    = 79999999,
            Valor     = 45.00m,
            Prazo     = 7,
            Descricao = "Mato Grosso do Sul"
        },

        // Paraná (CEPs 80xxx a 87xxx)
        new FaixaFrete
        {
            CepInicio = 80000000,
            CepFim    = 87999999,
            Valor     = 40.00m,
            Prazo     = 5,
            Descricao = "Paraná"
        },

        // Santa Catarina (CEPs 88xxx a 89xxx)
        new FaixaFrete
        {
            CepInicio = 88000000,
            CepFim    = 89999999,
            Valor     = 40.00m,
            Prazo     = 6,
            Descricao = "Santa Catarina"
        },

        // Rio Grande do Sul (CEPs 90xxx a 99xxx)
        new FaixaFrete
        {
            CepInicio = 90000000,
            CepFim    = 99999999,
            Valor     = 45.00m,
            Prazo     = 7,
            Descricao = "Rio Grande do Sul"
        },

        // Rondônia (CEPs 76800 a 76999 — sobrepõe parcialmente com GO/TO)
        new FaixaFrete
        {
            CepInicio = 76800000,
            CepFim    = 76999999,
            Valor     = 55.00m,
            Prazo     = 12,
            Descricao = "Rondônia"
        }
    };

    public Task<FreteResult> CalcularFreteAsync(string cepDestino)
    {
        if (string.IsNullOrWhiteSpace(cepDestino))
        {
            return Task.FromResult(new FreteResult
            {
                Disponivel = false,
                Descricao = "CEP não informado."
            });
        }

        // Remove caracteres não-numéricos
        var cepLimpo = Regex.Replace(cepDestino, @"\D", "");

        if (cepLimpo.Length != 8 || !int.TryParse(cepLimpo, out var cepNumerico))
        {
            return Task.FromResult(new FreteResult
            {
                Disponivel = false,
                Descricao = "CEP inválido."
            });
        }

        // Procura a faixa correspondente (da mais específica para a mais geral)
        var faixa = Faixas
            .OrderBy(f => f.CepFim - f.CepInicio) // faixas menores primeiro (mais específicas)
            .FirstOrDefault(f => cepNumerico >= f.CepInicio && cepNumerico <= f.CepFim);

        if (faixa is null)
        {
            return Task.FromResult(new FreteResult
            {
                Disponivel = false,
                Descricao = "Região não atendida."
            });
        }

        return Task.FromResult(new FreteResult
        {
            Valor = faixa.Valor,
            PrazoEstimadoDias = faixa.Prazo,
            Descricao = faixa.Descricao,
            Disponivel = true
        });
    }

    // =========================================================
    // CLASSE INTERNA — FAIXA DE FRETE
    // =========================================================

    private sealed class FaixaFrete
    {
        public int CepInicio { get; init; }
        public int CepFim { get; init; }
        public decimal Valor { get; init; }
        public int Prazo { get; init; }
        public string Descricao { get; init; } = string.Empty;
    }
}