using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Imovel;
using Domus.Application.UseCases.ImovelUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.ImovelController;

[ApiController]
[Route("domus/[controller]")]
public class ImovelController(CadastrarImovelUseCase cadastrarImovelUseCase) : ControllerBase
{
    [HttpPost("post/imovel")]
    public async Task<IActionResult> PostImovel(ImovelRequest request, CancellationToken cancellationToken)
    {
        var imovel = await cadastrarImovelUseCase.Execute(request, cancellationToken);
        return Ok(new SuccessApiResponse<ImovelResponse>
        {
            Success = true,
            Data = imovel
        });
    }
}
