using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly CadastrarLocatarioUseCase _cadastrarLocatarioUseCase;

    public UsuarioController(CadastrarLocatarioUseCase cadastrarLocatarioUseCase)
    {
        _cadastrarLocatarioUseCase = cadastrarLocatarioUseCase;
    }

    [HttpPost("post/locatario")]
    public async Task<IActionResult> PostLocatario(UsuarioRequest request, CancellationToken cancellationToken)
    {
        var user = await _cadastrarLocatarioUseCase.Execute(request, cancellationToken);
        return Ok(user);
    }
}
