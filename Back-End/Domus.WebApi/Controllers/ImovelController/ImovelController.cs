using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.CicloDeVida;
using Domus.Application.UseCases.ImovelUseCase;
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
    AprovarImovelUseCase aprovarImovelUseCase,
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


    
    [HttpGet("get/listar/aprovados")]
    public async Task<IActionResult> ListarimoveisAprovados()
    {
        var imoveis = await listarImoveisAprovadosUseCase.Execute();
        return Ok(new SuccessApiResponse<List<ImovelResponse>>()
        {
            Success = true,
            Data = imoveis
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
