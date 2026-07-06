

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.Atualizar;
using Domus.Application.DTOs.Imovel.CicloDeVida;
using Domus.Application.UseCases.ImovelUseCase.Atualizar;
using Domus.Application.UseCases.ImovelUseCase.CicloDeVida;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ImovelController;


[ApiController]
[Route("domus/imovel/put")]
public class ImovelPutController(
    AlterarInfosImovelUseCase alterarInfosImovelUseCase,
    AprovarImovelUseCase aprovarImovelUseCase
    ) : ControllerBase
{
    [HttpPut("infos")]
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
    [HttpPut("avaliar")]
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
