

using Domus.Application.DTOs.Chat.Response;
using Domus.Application.Interfaces.Notifications;
using Domus.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Domus.WebApi.Services.Chat;

public class ChatHubNotifier(IHubContext<ChatImovelHub> hubContext) : IChatHubNotifier
{
    public async Task NotifyNewMessageAsync(
        EnviarMensagemResponse mensagem, CancellationToken cancellationToken = default)
    {
        await hubContext.Clients.Group(mensagem.Chat_ID.ToString())
            .SendAsync("ReceberMensagem", mensagem);
    }
}
