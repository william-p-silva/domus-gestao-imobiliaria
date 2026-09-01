

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.UseCases.UsuarioUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.UsuarioControllers;

[ApiController]
[Route("domus/[controller]")]
public class ConfirmarController(ConfirmarEmailUseCase confirmarEmailUseCase) : ControllerBase
{
    [HttpGet("{token:guid}")]
    [ProducesResponseType<SuccessApiResponse<string>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmarEmailUser(
        [FromRoute] string token, CancellationToken cancellationToken)
    {
        var result = await confirmarEmailUseCase.Execute(token, cancellationToken);
        return Ok(new SuccessApiResponse<string>()
        {
            Data = result,
            Success = true
        });
    }
}
