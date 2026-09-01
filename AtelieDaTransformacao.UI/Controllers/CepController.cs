using AtelieDaTransformacao.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtelieDaTransformacao.UI.Controllers;

[Route("Cep")]
public sealed class CepController : Controller
{
    private readonly ICepService _cepService;
    private readonly IFreteService _freteService;

    public CepController(
        ICepService cepService,
        IFreteService freteService)
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
            return Ok(new { erro = true, mensagem = "CEP não encontrado." });

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

    [HttpGet("CalcularFrete")]
    public async Task<IActionResult> CalcularFrete([FromQuery] string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            return BadRequest(new { disponivel = false, mensagem = "CEP não informado." });

        var resultado = await _freteService.CalcularFreteAsync(cep);

        return Ok(new
        {
            disponivel = resultado.Disponivel,
            valor = resultado.Valor,
            valorFormatado = resultado.Valor.ToString("N2"),
            prazo = resultado.PrazoEstimadoDias,
            descricao = resultado.Descricao
        });
    }
}