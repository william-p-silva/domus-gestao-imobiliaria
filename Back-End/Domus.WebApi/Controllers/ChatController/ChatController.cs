


using Azure;
using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Chat.Request;
using Domus.Application.DTOs.Chat.Response;
using Domus.Application.UseCases.ChatUseCase;
using Domus.Application.UseCases.ChatUseCase.Listar;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ChatController;

[ApiController]
[Route("domus/[controller]")]
public class ChatController(
    CadastrarChatImovel cadastrarChatImovel, 
    EnviarMensagemUseCase enviarMensagem,
    BuscarImovelChatUseCase buscarImovelChatUseCase
    ) : ControllerBase
{

    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("Usuário não autenticado.");

        return Guid.Parse(claim);
    }


    [HttpPost("post")]
    [ProducesResponseType<SuccessApiResponse<Guid>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarChatDoImovel(
        [FromBody] RequestNewChat request, CancellationToken cancellationToken)
    {
        var locatario_id = GetUserId();

        var response = await cadastrarChatImovel.ExecuteAsync(locatario_id, request, cancellationToken);

        return Ok(ApiResponse.Success(response));
    }

    [HttpPost("post/send-message")]
    [ProducesResponseType<SuccessApiResponse<EnviarMensagemResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> EnviarMensagem(
        [FromBody] EnviarMensagemRequest request, CancellationToken cancellationToken)
    {
        var usuario_id = GetUserId();

        var response = await enviarMensagem.ExecuteAsync(usuario_id, request, cancellationToken);

        return Ok(ApiResponse.Success(response));
    }

    [HttpGet("get/{imovel_id:guid}")]
    [EndpointSummary("Obter ou criar chat do imóvel")]
    [EndpointDescription("Busca a conversa ativa entre o locatário autenticado e o imóvel informado. Caso o chat ainda não exista, ele será criado automaticamente.")]
    [ProducesResponseType<SuccessApiResponse<ResponseChat>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscarChatPorImovel(
        [FromRoute] Guid imovel_id, CancellationToken cancellationToken
        )
    {
        var locatario_id = GetUserId();

        var chat = await buscarImovelChatUseCase.ExecuteAsync(imovel_id, locatario_id, cancellationToken);

        return Ok(ApiResponse.Success(chat));
    }
}
