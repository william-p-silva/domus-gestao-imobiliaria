using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.Atualizar;
using Domus.Application.DTOs.Imovel.CicloDeVida;
using Domus.Application.UseCases.ImovelUseCase;
using Domus.Application.UseCases.ImovelUseCase.Atualizar;
using Domus.Application.UseCases.ImovelUseCase.CicloDeVida;
using Domus.Application.UseCases.ImovelUseCase.Listar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Route("domus/[controller]")]
public class ImovelController(
    CadastrarImovelUseCase cadastrarImovelUseCase,
    AlterarInfosImovelUseCase alterarInfosImovelUseCase,
    AprovarImovelUseCase aprovarImovelUseCase,
    BuscarImoveisDoLocadorUseCase buscarImoveisDoLocadorUseCase,
    BuscarImovelPorIdUseCase buscarImovelPorIdUseCase,
    ListarImoveisAprovadosUseCase listarImoveisAprovadosUseCase,
    ExcluirImovelUseCase excluirImovelUseCase
    ) : ControllerBase
{

    [Authorize(Roles = "Locador")]
    [HttpPost("post/imovel")]
    public async Task<IActionResult> PostImovel([FromBody] ImovelRequest request, CancellationToken cancellationToken)
    {
        var imovel = await cadastrarImovelUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<ImovelResponse>
        {
            Success = true,
            Data = imovel
        });
    }


    [HttpPut("put/infos")]
    [Authorize(Roles = "Locador")]
    public async Task<IActionResult> AlterarInfos(
        RequestAlterarInfosImovel request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var imovel = await alterarInfosImovelUseCase.Execute(request, userId, cancellationToken);

        return Ok(new SuccessApiResponse<string>
        {
            Success = true,
            Data = imovel
        });
    }


    [Authorize(Roles = "Administrador")]
    [HttpPut("put/avaliar")]
    public async Task<IActionResult> AdmAvaliarImovel(
        [FromBody] RequestAprovarImovel request, 
        CancellationToken cancellationToken
        )
    {
        var admin_id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var imovel = await aprovarImovelUseCase.Execute(request, admin_id, cancellationToken);

        return Ok(new SuccessApiResponse<ImovelResponse>()
        {
            Success = true,
            Data = imovel
        });
    }


    [HttpGet("get/listar/locador")]
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

        
    [HttpGet("get/listar/aprovados")]
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


    [HttpGet("get/buscar/{imovelId:guid}")]
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


    [HttpDelete("delete")]
    [Authorize(Roles = "Locador")]
    public async Task<IActionResult> DeletarImovel(
        [FromBody] RequestExcluirImovel request,
        CancellationToken cancellationToken
        )
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await excluirImovelUseCase.Execute(request, userId, cancellationToken);

        return Ok(new SuccessApiResponse<string>
        {
            Success = true,
            Data = result
        });
    }
}
