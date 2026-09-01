

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.UseCases.ImovelUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Route("domus/imovel/post")]
public class ImovelPostController(
    CadastrarImovelUseCase cadastrarImovelUseCase
    ) : ControllerBase
{
    [HttpPost("imovel")]
    [Authorize(Roles = "Locador")]
    [ProducesResponseType<SuccessApiResponse<ImovelResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostImovel([FromBody] ImovelRequest request, CancellationToken cancellationToken)
    {
        var imovel = await cadastrarImovelUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<ImovelResponse>
        {
            Success = true,
            Data = imovel
        });
    }
}
