using Microsoft.AspNetCore.SignalR;

namespace Domus.WebApi.Hubs;

public class ChatImovelHub : Hub
{
    public async Task JoinChatGroup(Guid chatId)
    {
        Console.WriteLine(
            $"[HUB] Conexão {Context.ConnectionId} entrando no grupo {chatId}"
        );

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            chatId.ToString()
        );

        Console.WriteLine(
            $"[HUB] Conexão {Context.ConnectionId} entrou no grupo {chatId}"
        );
    }

    public async Task LeaveChatGroup(Guid chatId)
    {
        Console.WriteLine(
            $"[HUB] Conexão {Context.ConnectionId} saindo do grupo {chatId}"
        );

        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            chatId.ToString()
        );

        Console.WriteLine(
            $"[HUB] Conexão {Context.ConnectionId} saiu do grupo {chatId}"
        );
    }
}