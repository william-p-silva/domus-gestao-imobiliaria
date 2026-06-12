using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Contrato;
using Domus.Application.DTOs.Contrato.CicloDeVida;
using Domus.Application.UseCases.ContratoUseCase;
using Domus.Application.UseCases.ContratoUseCase.CicloDeVida;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ContratoController;

[ApiController]
[Authorize]
[Route("domus/[controller]")]
public class ContratoController(
    CadastrarContratoUseCase cadastrarContratoUseCase,
    DisponibilizarParaAssinaturaUseCase disponibilizarParaAssinaturaUseCase,
    AssinarContratoUseCase assinarContratoUseCase,
    RejeitarMinutaContratoUseCase rejeitarMinutaContratoUseCase
    ) : ControllerBase
{
    [Authorize(Roles = "Locador")]
    [HttpPost("post/contrato")]
    public async Task<IActionResult> CadastrarContrato([FromBody] ContratoRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var contrato = await cadastrarContratoUseCase.Execute(request, userId, cancellationToken);
        return Ok(new SuccessApiResponse<ContratoResponse>
        {
            Success = true,
            Data = contrato
        });
    }

    [Authorize(Roles = "Locador")]
    [HttpPut("put/assinatura/locador")]
    public async Task<IActionResult> DisponibilizarParaAssinaturaDoLocatario(
        [FromBody] RequestDisponibilizarAssinatura request,
        CancellationToken cancellationToken)
    {
        var locadorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var contrato = await disponibilizarParaAssinaturaUseCase.Execute(request: request, locador_id: locadorId, cancellationToken);

        return Ok(new SuccessApiResponse<ResponseMinutaContrato>()
        {
            Success = true,
            Data = contrato
        });
    }

    [Authorize(Roles = "Locatario")]
    [HttpPut("put/assinatura/locatario")]
    public async Task<IActionResult> LocatarioAssinaContrato(
        [FromBody] RequestAssinarContrato request,
        CancellationToken cancellationToken
        )
    {
        var locatarioId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var contrato = await assinarContratoUseCase.Execute(request: request, locatario_id: locatarioId, cancellationToken);

        return Ok(new SuccessApiResponse<ResponseMinutaContrato>()
        {
            Success = true,
            Data = contrato
        });
    }


    [Authorize(Roles = "Locador,Locatario")]
    [HttpPut("put/cancelar-minuta")]
    public async Task<IActionResult> CancelarMinutaContrato(
        [FromBody] Guid contrato_id,
        CancellationToken cancellationToken
        )
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var contrato = await rejeitarMinutaContratoUseCase.Execute(contrato_ID: contrato_id ,userId, cancellationToken);

        return Ok(new SuccessApiResponse<ContratoResponse>()
        {
            Success = true,
            Data = contrato
        });
    }
}
