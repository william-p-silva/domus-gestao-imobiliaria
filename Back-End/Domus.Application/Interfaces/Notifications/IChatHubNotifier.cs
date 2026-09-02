using Domus.Application.DTOs.Chat.Response;

namespace Domus.Application.Interfaces.Notifications;

public interface IChatHubNotifier
{
    Task NotifyNewMessageAsync(EnviarMensagemResponse mensagem, CancellationToken cancellationToken = default);
}
