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
    public async Task<IActionResult> PostLocador([FromBody] UsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuario = await cadastrarLocadorUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<UsuarioResponse>
        {
            Success = true,
            Data = usuario
        });
    }
}
