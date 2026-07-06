

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
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
    BuscarImovelPorIdUseCase buscarImovelPorIdUseCase
    ): ControllerBase
{


    [HttpGet("listar/todos")]
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
}
