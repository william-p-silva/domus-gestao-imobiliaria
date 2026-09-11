

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.UseCases.ImovelUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Route("domus/imovel/post")]
public class ImovelPostController(
    CadastrarImovelUseCase cadastrarImovelUseCase
    ) : ControllerBase
{
    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        return Guid.Parse(claim);
    }


    [HttpPost("imovel")]
    [Authorize(Roles = "Locador")]
    [ProducesResponseType<SuccessApiResponse<ImovelResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostImovel([FromBody] ImovelRequest request, CancellationToken cancellationToken)
    {
        var user_id = GetUserId();
        var imovel = await cadastrarImovelUseCase.Execute(request, user_id, cancellationToken);
        return Ok(new SuccessApiResponse<ImovelResponse>
        {
            Success = true,
            Data = imovel
        });
    }
}
