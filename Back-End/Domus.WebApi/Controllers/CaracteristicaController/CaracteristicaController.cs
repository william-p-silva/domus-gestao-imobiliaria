
using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.UseCases.CaracteristicaUseCase;
using Domus.Application.UseCases.CaracteristicaUseCase.Listar;
using Domus.Domain.Enums.Caracteristica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.CaracteristicaController;

[ApiController]
[Route("domus/[controller]")]
public class CaracteristicaController(
    CadastrarCaracteristicaUseCase cadastrarCaracteristicaUseCase,
    ListarCaracteristicasUseCase listarCaracteristicasUseCase
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

    [HttpGet("get/listar/{estado:int?}")]
    [ProducesResponseType<SuccessApiResponse<List<CaracteristicaResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAtivas(
        CancellationToken cancellationToken,
        [FromRoute] EstadoCaracteristica estado = EstadoCaracteristica.Ativa
        )
    {
        var response = await listarCaracteristicasUseCase.ExecuteAsync(
            cancellationToken: cancellationToken, estado: estado);

        return Ok(new SuccessApiResponse<List<CaracteristicaResponse>>
        {
            Success = true,
            Data = response
        });
    }
}
