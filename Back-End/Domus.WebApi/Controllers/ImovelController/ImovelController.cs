using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.CicloDeVida;
using Domus.Application.UseCases.ImovelUseCase;
using Domus.Application.UseCases.ImovelUseCase.CicloDeVida;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Authorize]
[Route("domus/[controller]")]
public class ImovelController(
    CadastrarImovelUseCase cadastrarImovelUseCase,
    AprovarImovelUseCase aprovarImovelUseCase
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
}
