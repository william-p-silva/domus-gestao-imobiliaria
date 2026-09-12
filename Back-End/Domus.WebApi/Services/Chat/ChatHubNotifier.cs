

using Domus.Application.DTOs.Chat.Response;
using Domus.Application.Interfaces.Notifications;
using Domus.WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Domus.WebApi.Services.Chat;

public class ChatHubNotifier(IHubContext<ChatImovelHub> hubContext) : IChatHubNotifier
{
    public async Task NotifyNewMessageAsync(
        ResponseMensagemChat mensagem,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine(
            $"\n\n\n [HUB] Notificando grupo {mensagem.Chat_ID} \n\n\n"
        );

        await hubContext
            .Clients
            .Group(mensagem.Chat_ID.ToString())
            .SendAsync(
                "ReceberMensagem",
                mensagem,
                cancellationToken
            );

        Console.WriteLine(
            $"[HUB] Mensagem enviada para grupo {mensagem.Chat_ID}"
        );
    }
}
