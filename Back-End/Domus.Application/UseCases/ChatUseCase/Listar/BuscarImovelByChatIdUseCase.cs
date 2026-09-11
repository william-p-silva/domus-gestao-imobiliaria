

using Domus.Application.DTOs.Chat.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.ChatUseCase.Listar;

public class BuscarImovelByChatIdUseCase(
    IChatRepository chatRepository)
{
    public async Task<ResponseChat> ExecuteAsync(Guid chat_id,Guid user_id, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.BuscarPorIdResponse(chat_id, user_id)
            ?? throw new NotFoundException("Chat não encontrado.");

        return chat;
    }
}
