using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Usuarios;
using Domus.Application.DTOs.Usuarios.Atualizar;
using Domus.Application.DTOs.Usuarios.Perfil;
using Domus.Application.UseCases.UsuarioUseCase;
using Domus.Application.UseCases.UsuarioUseCase.Atualizar;
using Domus.Application.UseCases.UsuarioUseCase.Listar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.UsuarioControllers;


[ApiController]
[Route("domus/[controller]")]
public class UsuarioController(
    AdicionarInfosExtrasUseCase adicionarInfosExtrasUseCase, 
    BuscarPerfilUseCase buscarPerfilUseCase,
    ExcluirContaUseCase excluirContaUseCase,
    AlterarInfosUseCase alterarInfosUseCase
    ) : ControllerBase
{

    [HttpPut("put/adicionar/infos-extras")]
    [Authorize]
    public async Task<IActionResult> AdicionarInfosExtra(
        [FromBody] RequestInfosExtras request,
        CancellationToken cancellationToken
        )
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await adicionarInfosExtrasUseCase.Execute(request, userId, cancellationToken);

        return Ok(new SuccessApiResponse<string>
        {
            Success = true,
            Data = result
        });
    }


    [HttpPut("put/alterar/infos")]
    [Authorize]
    public async Task<IActionResult> AlterarInfos(
        [FromBody] RequestAtualizarDTO request,
        CancellationToken cancellationToken
        )
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await alterarInfosUseCase.Execute(request, userId, cancellationToken);
        return Ok(new SuccessApiResponse<string>
        {
            Success = true,
            Data = result
        });
    }


    [HttpGet("get/perfil")]
    [Authorize]
    public async Task<IActionResult> BuscarPerfilUsuario(
        CancellationToken cancellationToken
        )
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await buscarPerfilUseCase.Execute(userId, cancellationToken);
        return Ok(new SuccessApiResponse<PerfilUsuarioResponse>
        {
            Success = true,
            Data = result
        });
    }


    [HttpDelete("delete/usuario")]
    [Authorize]
    public async Task<IActionResult> ExcluirConta(
        CancellationToken cancellationToken
        )
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await excluirContaUseCase.Execute(userId, cancellationToken);
        return Ok(new SuccessApiResponse<bool>
        {
            Success = true,
            Data = result
        });
    }
}
