using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Usuarios;
using Domus.Application.UseCases.UsuarioUseCase.AuthUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.UsuarioControllers;

[ApiController]
[Route("domus/[controller]")]
public class AuthController(LoginUseCase loginUseCase) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await loginUseCase.Execute(request, cancellationToken);

        return Ok(new SuccessApiResponse<LoginResponse>
        {
            Success = true,
            Data = usuario
        });
    }
}
