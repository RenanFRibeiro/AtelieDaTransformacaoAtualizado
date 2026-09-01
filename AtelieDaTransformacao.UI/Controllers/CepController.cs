using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.Controllers;

[Route("Cep")]
public sealed class CepController : Controller
{
    private readonly ICepService _cepService;
    private readonly IFreteService _freteService;

    public CepController(ICepService cepService, IFreteService freteService)
    {
        _cepService = cepService;
        _freteService = freteService;
    }

    [HttpGet("BuscarEndereco")]
    public async Task<IActionResult> BuscarEndereco([FromQuery] string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            return BadRequest(new { erro = true, mensagem = "CEP não informado." });

        var resultado = await _cepService.BuscarEnderecoPorCepAsync(cep);

        if (resultado is null)
            return Ok(new { erro = true, mensagem = "CEP não encontrado ou inválido." });

        return Ok(new
        {
            erro = false,
            logradouro = resultado.Logradouro,
            bairro = resultado.Bairro,
            cidade = resultado.Localidade,
            estado = resultado.Uf,
            complemento = resultado.Complemento
        });
    }

    [HttpPost("CalcularFrete")]
    public async Task<IActionResult> CalcularFrete([FromBody] FreteRequestDto request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.CepDestino))
            return BadRequest(new { disponivel = false, mensagem = "Dados de frete inválidos." });

        var opcoes = await _freteService.CalcularFreteAsync(request);
        return Ok(opcoes);
    }
}