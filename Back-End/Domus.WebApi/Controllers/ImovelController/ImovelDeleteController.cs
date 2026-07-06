


using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.UseCases.ImovelUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Route("domus/imovel/delete")]
public class ImovelDeleteController(
    ExcluirImovelUseCase excluirImovelUseCase
    ) : ControllerBase
{
    [HttpDelete]
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
