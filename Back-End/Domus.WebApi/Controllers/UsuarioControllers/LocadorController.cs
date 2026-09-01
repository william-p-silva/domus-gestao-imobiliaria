using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.UseCases.UsuarioUseCase.LocadorUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.UsuarioControllers;

[ApiController]
[Route("domus/[controller]")]
public class LocadorController(CadastrarLocadorUseCase cadastrarLocadorUseCase) : ControllerBase
{

    [HttpPost("post/locador")]
    [ProducesResponseType<SuccessApiResponse<string>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostLocador([FromBody] UsuarioRequest request, CancellationToken cancellationToken)
    {
        var result = await cadastrarLocadorUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<string>
        {
            Success = true,
            Data = result
        });
    }
}
