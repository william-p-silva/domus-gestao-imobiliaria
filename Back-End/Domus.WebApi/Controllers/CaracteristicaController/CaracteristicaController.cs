
using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.UseCases.CaracteristicaUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.CaracteristicaController;

[ApiController]
[Route("domus/[controller]")]
public class CaracteristicaController(
    CadastrarCaracteristicaUseCase cadastrarCaracteristicaUseCase
    ) : ControllerBase
{
    [HttpPost("post")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType<SuccessApiResponse<CaracteristicaResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostCaracteristica(
        [FromBody] CadastrarCaracteristicaRequest request
        ,CancellationToken cancellationToken
        )
    {
        var response = await cadastrarCaracteristicaUseCase.ExecuteAsync(request, cancellationToken);

        return Ok(new SuccessApiResponse<CaracteristicaResponse>
        {
            Data = response,
            Success = true
        });
    }
}
