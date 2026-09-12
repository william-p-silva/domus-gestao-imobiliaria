

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
    public async Task<ResponseMensagemChat> ExecuteAsync(Guid usuario_id, EnviarMensagemRequest request, CancellationToken cancellationToken)
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

        Console.WriteLine(
            $"[CHAT] Mensagem salva. Chat={mensagem.Chat_ID} Mensagem={mensagem.MensagemChat_ID}"
        );

        var response = new ResponseMensagemChat
        {
            MensagemChat_ID = mensagem.MensagemChat_ID,
            Chat_ID = mensagem.Chat_ID,
            Usuario_ID = usuario.Usuario_ID,
            Texto = mensagem.Texto,
            DataEnvio = mensagem.DataEnvio,
            Estado = mensagem.Estado.ToString()
        };

        Console.WriteLine(
            $"\n\n\n [CHAT] Chamando notifier. Chat={response.Chat_ID} \n\n\n"
        );

        await chatHubNotifier.NotifyNewMessageAsync(response, cancellationToken);

        Console.WriteLine(
            $"\n\n\n [CHAT] Notifier finalizado. Chat={response.Chat_ID} \n\n\n"
        );

        return response;
    }
}
