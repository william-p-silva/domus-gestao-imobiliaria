using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Avaliacoes;
using Domus.Application.UseCases.AvaliacaoUseCases;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.AvaliacaoController;

[ApiController]
[Route("domus/[controller]")]
public class AvaliacaoController(CriarAvaliacaoUseCase criarAvaliacaoUseCase) : ControllerBase
{
    [HttpPost("post/avaliacao")]
    public async Task<IActionResult> CadastrarAvaliacao(AvaliacaoRequest request, CancellationToken cancellationToken)
    {
        var avaliacao = await criarAvaliacaoUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<AvaliacaoResponse>
        {
            Success = true,
            Data = avaliacao
        });
    }
}
