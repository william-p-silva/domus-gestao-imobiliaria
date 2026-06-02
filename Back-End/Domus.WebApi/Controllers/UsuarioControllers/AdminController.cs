using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.UseCases.UsuarioUseCase.AdminUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.UsuarioControllers;

[ApiController]
[Route("domus/[controller]")]
public class AdminController(CadastrarAdminUseCase cadastrarAdminUseCase) : ControllerBase
{
    [HttpPost("post/admin")]
    public async Task<IActionResult> PostAdmin(UsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuario = await cadastrarAdminUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<UsuarioResponse>
        {
            Success = true,
            Data = usuario
        });
    }
}
