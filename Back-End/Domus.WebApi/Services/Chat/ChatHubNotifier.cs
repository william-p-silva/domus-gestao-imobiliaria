

using Domus.Application.DTOs.Chat.Response;
using Domus.Application.Interfaces.Notifications;

namespace Domus.WebApi.Services.Chat;

public class ChatHubNotifier : IChatHubNotifier
{
    public Task NotifyNewMessageAsync(EnviarMensagemResponse mensagem, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
