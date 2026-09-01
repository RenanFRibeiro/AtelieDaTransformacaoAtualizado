namespace AtelieDaTransformacao.Application.DTOs;

/// <summary>
/// Resultado da consulta de CEP via ViaCEP.
/// </summary>
public sealed class CepDto
{
    public string Cep { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
    public string Uf { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Ibge { get; set; } = string.Empty;
    public bool Erro { get; set; }
}

/// <summary>
/// Requisição com parâmetros obrigatórios para cálculo de frete.
/// </summary>
public class FreteRequestDto
{
    public string CepOrigem { get; set; } = "01000000";
    public string CepDestino { get; set; } = string.Empty;
    public decimal PesoKg { get; set; }
    public decimal AlturaCm { get; set; }
    public decimal LarguraCm { get; set; }
    public decimal ComprimentoCm { get; set; }
}

/// <summary>
/// Resultado de uma opção cálculo de frete.
/// </summary>
public sealed class FreteResult
{
    public string Nome { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public int PrazoEstimadoDias { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public bool Disponivel { get; set; } = true;
}