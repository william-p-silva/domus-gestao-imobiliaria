using Microsoft.AspNetCore.SignalR;

namespace Domus.WebApi.Hubs;

public class ChatImovelHub : Hub
{
    public async Task JoinChatGroup(Guid chatId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            chatId.ToString()
        );
    }

    public async Task LeaveChatGroup(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            chatId.ToString()
        );
    }
}