


using Domus.Application.DTOs.ApiResponse;
using Domus.Application.DTOs.Chat.Request;
using Domus.Application.UseCases.ChatUseCase;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Domus.WebApi.Controllers.ChatController;

[ApiController]
[Route("domus/[controller]")]
public class ChatController(
    CadastrarChatImovel cadastrarChatImovel
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
}
