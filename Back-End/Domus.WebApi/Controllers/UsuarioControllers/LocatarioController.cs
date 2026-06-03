using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.UsuarioControllers;

[ApiController]
[Route("domus/[controller]")]
public class LocatarioController(CadastrarLocatarioUseCase cadastrarLocatarioUseCase) : ControllerBase
{
    [HttpPost("post/locatario")]
    public async Task<IActionResult> PostLocatario([FromBody] UsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuario = await cadastrarLocatarioUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<UsuarioResponse>
        {
            Success = true,
            Data = usuario
        });
    }
}
