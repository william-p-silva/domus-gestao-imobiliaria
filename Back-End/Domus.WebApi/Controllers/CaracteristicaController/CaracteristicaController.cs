
using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.UseCases.CaracteristicaUseCase;
using Domus.Application.UseCases.CaracteristicaUseCase.Estatisticas;
using Domus.Application.UseCases.CaracteristicaUseCase.Listar;
using Domus.Domain.Enums.Caracteristica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domus.WebApi.Controllers.CaracteristicaController;

[ApiController]
[Route("domus/[controller]")]
public class CaracteristicaController(
    CadastrarCaracteristicaUseCase cadastrarCaracteristicaUseCase,
    CadastrarGrupoCaracteristicasUseCase cadastrarGrupoCaracteristicasUseCase,
    ListarCaracteristicasUseCase listarCaracteristicasUseCase,
    BuscarEstatisticasUseCase buscarEstatisticasUseCase
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

        return Ok(ApiResponse.Success(response));
    }

    [HttpPost("post/grupo-caracteristica")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType<SuccessApiResponse<GrupoCaracteristicaResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostGrupoCaracteristica(
        [FromBody] RequestCadastrarGrupoCaracteristica request,
        CancellationToken cancellationToken
        )
    {
        var response = await cadastrarGrupoCaracteristicasUseCase.ExecuteAsync(request, cancellationToken);

        return Ok(ApiResponse.Success(response));
    }

    [HttpGet("get/listar/{estado:int?}")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType<SuccessApiResponse<List<CaracteristicaResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAtivas(
        CancellationToken cancellationToken,
        [FromRoute] EstadoCaracteristica estado = EstadoCaracteristica.Ativa
        )
    {
        var response = await listarCaracteristicasUseCase.ExecuteAsync(
            cancellationToken: cancellationToken, estado: estado);

        return Ok(ApiResponse.Success(response));
    }

    [HttpGet("get/buscar/estatisticas")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType<SuccessApiResponse<ResponseEstatisticasCaracteristicas>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscarEstatisticas(CancellationToken cancellationToken)
    {
        var response = await buscarEstatisticasUseCase.ExecuteAsync(cancellationToken);

        return Ok(ApiResponse.Success(response));
    }
}
