

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.Listar;
using Domus.Application.UseCases.ImovelUseCase.Listar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Route("domus/imovel/get")]
public class ImovelGetController(
    ListarTodosImoveisUseCase listarTodosImoveisUseCase,
    BuscarImoveisDoLocadorUseCase buscarImoveisDoLocadorUseCase,
    ListarImoveisAprovadosUseCase listarImoveisAprovadosUseCase,
    ListarImoveisComFiltroUseCase listarImoveisComFiltroUseCase,
    ListarImoveisNaoAvaliadosUseCase listarImoveisNaoAvaliados,
    ListarImoveisNaoAprovadosUseCase listarImoveisNaoAprovadosUseCase,
    BuscarImovelPorIdUseCase buscarImovelPorIdUseCase
    ): ControllerBase
{


    [HttpGet("listar/todos")]
    [ProducesResponseType<SuccessApiResponse<List<ImovelResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarTodosImoveis(CancellationToken cancellationToken)
    {
        var imoveis = await listarTodosImoveisUseCase.Execute(cancellationToken);
        return Ok(new SuccessApiResponse<List<ImovelResponse>>
        {
            Success = true,
            Data = imoveis
        });
    }


    [HttpGet("listar/locador")]
    [Authorize(Roles = "Locador")]
    [ProducesResponseType<SuccessApiResponse<List<ImovelResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarImoveisLocador(
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var imoveisLocador = await buscarImoveisDoLocadorUseCase.Execute(userId, cancellationToken);

        return Ok(new SuccessApiResponse<List<ImovelResponse>>
        {
            Success = true,
            Data = imoveisLocador
        });
    }


    [HttpGet("listar/aprovados")]
    [ProducesResponseType<SuccessApiResponse<List<ImovelResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarimoveisAprovados(
    CancellationToken cancellationToken)
    {
        var imoveis = await listarImoveisAprovadosUseCase.Execute(cancellationToken);
        return Ok(new SuccessApiResponse<List<ImovelResponse>>()
        {
            Success = true,
            Data = imoveis
        });
    }


    [HttpGet("buscar/{imovelId:guid}")]
    [ProducesResponseType<SuccessApiResponse<ImovelResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscarPorId(
        [FromRoute] Guid imovelId,
        CancellationToken cancellationToken
        )
    {
        var imovel = await buscarImovelPorIdUseCase.Execute(imovelId, cancellationToken);
        return Ok(new SuccessApiResponse<ImovelResponse>
        {
            Success = true,
            Data = imovel
        });
    }


    [HttpGet("listar/pesquisa")]
    [ProducesResponseType<SuccessApiResponse<List<ImovelResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarComFiltro(
        [FromQuery] FiltroImovel filtro,
        CancellationToken cancellationToken)
    {
        var imoveis = await listarImoveisComFiltroUseCase.Execute(filtro, cancellationToken);

        return Ok(new SuccessApiResponse<List<ImovelResponse>>
        {
            Success = true,
            Data = imoveis
        });
    }


    [HttpGet("listar/sem-avaliacao")]
    [ProducesResponseType<SuccessApiResponse<List<ImovelResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarNaoAvaliados(CancellationToken cancellationToken)
    {
        var imoveis = await listarImoveisNaoAvaliados.Execute(cancellationToken);

        return Ok(new SuccessApiResponse<List<ImovelResponse>>
        {
            Success = true,
            Data = imoveis
        });
    }

    [HttpGet("listar/nao-aprovados")]
    [ProducesResponseType<SuccessApiResponse<List<ImovelResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarNaoAprovados(CancellationToken cancellationToken)
    {
        var imoveis = await listarImoveisNaoAprovadosUseCase.Execute(cancellationToken);

                return Ok(new SuccessApiResponse<List<ImovelResponse>>
        {
            Success = true,
            Data = imoveis
        });
    }
}
