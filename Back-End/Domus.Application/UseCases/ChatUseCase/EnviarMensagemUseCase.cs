

using Domus.Application.DTOs.Chat.Request;
using Domus.Application.DTOs.Chat.Response;
using Domus.Application.Interfaces.Notifications;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums.Chat;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.ChatUseCase;

public class EnviarMensagemUseCase(
    IChatRepository chatRepository,
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork,
    IChatHubNotifier chatHubNotifier
    )
{
    public async Task<EnviarMensagemResponse> ExecuteAsync(Guid usuario_id, EnviarMensagemRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ValidationException("Requisição inválida.");        

        var chat = await chatRepository.BuscarPorIdAsync(request.Chat_ID, cancellationToken)
            ?? throw new NotFoundException("Chat não encontrado.");

        var usuario = await usuarioRepository.BuscarPorIdAsync(usuario_id, cancellationToken)
            ?? throw new NotFoundException("Usuário não encontrado.");

        var usuarioChat = chat.UsuarioChats.FirstOrDefault(u => u.Usuario_ID == usuario_id && u.Estado == EstadoUsuarioChat.Ativo)
            ?? throw new BusinessRuleException("Usuário não pertence a este chat.");

        var mensagem = new MensagemChat(usuarioChat, chat, request.Texto);

        chat.AdicionarMensagem(mensagem);

        await chatRepository.AddMensagemAsync(mensagem, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new EnviarMensagemResponse
        {
            MensagemChat_ID = mensagem.MensagemChat_ID,
            Chat_ID = mensagem.Chat_ID,
            UsuarioChat_ID = mensagem.UsuarioChat_ID,
            Usuario_ID = usuario.Usuario_ID,
            Texto = mensagem.Texto,
            DataEnvio = mensagem.DataEnvio
        };

        // Dispara notificação ao hub (implementação concreta será provida pelo projeto Web/API)
        try
        {
            if (chatHubNotifier is not null)
                await chatHubNotifier.NotifyNewMessageAsync(response, cancellationToken);
        }
        catch
        {
            // Não deve impedir o fluxo principal em caso de falha na notificação
        }

        return response;
    }
}
