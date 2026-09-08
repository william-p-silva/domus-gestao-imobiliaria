

using Microsoft.AspNetCore.SignalR;

namespace Domus.WebApi.Hubs;

public class ChatImovelHub : Hub
{
    public async Task JoinChatGroup(Guid chat_id, CancellationToken cancellationToken = default)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chat_id.ToString(), cancellationToken);
    }

    public async Task LeaveChatGroup(Guid chat_id, CancellationToken cancellationToken = default)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat_id.ToString(), cancellationToken);
    }
}
