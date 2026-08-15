using System.Security.Claims;
using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Usuarios;
using Domus.Application.UseCases.UsuarioUseCase.AuthUseCase;
using Microsoft.AspNetCore.Authorization;
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

        Response.Cookies.Append("token", usuario.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(3)
        });

        return Ok(new SuccessApiResponse<Object>
        {
            Success = true,
            Data = new
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil
            }
        });
    }


    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");

        return Ok(new SuccessApiResponse<string>
        {
            Success = true,
            Data = "Logout realizado com sucesso."
        });
    }

    [HttpPost("me")]
    [Authorize]
    public IActionResult Me()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var nome = User.FindFirst(ClaimTypes.Name)?.Value;
        var perfis = User.FindAll(ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new SuccessApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                Usuario_ID = id,
                Email = email,
                Nome = nome,
                Perfil = perfis
            }
        });
    }
}
