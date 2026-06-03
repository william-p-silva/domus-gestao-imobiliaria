using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Contrato;
using Domus.Application.UseCases.ContratoUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.ContratoController;

[ApiController]
[Route("domus/[controller]")]
public class ContratoController(CadastrarContratoUseCase cadastrarContratoUseCase) : ControllerBase
{

    [HttpPost("post/contrato")]
    public async Task<IActionResult> CadastrarContrato([FromBody] ContratoRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse("2967AB27-178C-4A93-9F1F-EFC95052FB61");
        var contrato = await cadastrarContratoUseCase.Execute(request, userId, cancellationToken);
        return Ok(new SuccessApiResponse<ContratoResponse>
        {
            Success = true,
            Data = contrato
        });
    } 
}
