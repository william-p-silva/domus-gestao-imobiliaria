

using Domus.Application.DTOs.ApiResponse;
using Domus.Application.UseCases.UsuarioUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.UsuarioControllers;

[ApiController]
[Route("domus/[controller]")]
public class ConfirmarController(ConfirmarEmailUseCase confirmarEmailUseCase) : ControllerBase
{
    [HttpGet("{token:guid}")]
    public async Task<IActionResult> ConfirmarEmailUser(
    [FromRoute] string token, CancellationToken cancellationToken)
    {
        var usuario = await confirmarEmailUseCase.Execute(token, cancellationToken);
        return Ok(new SuccessApiResponse<string>()
        {
            Data = usuario,
            Success = true
        });
    }
}
