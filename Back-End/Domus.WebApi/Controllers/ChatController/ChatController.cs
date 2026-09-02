


using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Chat.Request;
using Domus.Application.DTOs.Chat.Response;
using Domus.Application.UseCases.ChatUseCase;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ChatController;

[ApiController]
[Route("domus/[controller]")]
public class ChatController(
    CadastrarChatImovel cadastrarChatImovel, 
    EnviarMensagemUseCase enviarMensagem
    ) : ControllerBase
{
    [HttpPost("post")]
    [ProducesResponseType<SuccessApiResponse<Guid>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CadastrarChatDoImovel(
        [FromBody] RequestNewChat request, CancellationToken cancellationToken)
    {
        var locatario_id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var resposne = await cadastrarChatImovel.ExecuteAsync(locatario_id, request, cancellationToken);

        return Ok(new SuccessApiResponse<Guid>
        {
            Success = true,
            Data = resposne
        });
    }

    [HttpPost("post/send-message")]
    [ProducesResponseType<SuccessApiResponse<EnviarMensagemResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> EnviarMensagem(
        [FromBody] EnviarMensagemRequest request, CancellationToken cancellationToken)
    {
        var usuario_id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var response = await enviarMensagem.ExecuteAsync(usuario_id, request, cancellationToken);

        return Ok(new SuccessApiResponse<EnviarMensagemResponse>
        {
            Success = true,
            Data = response
        });
    }
}
