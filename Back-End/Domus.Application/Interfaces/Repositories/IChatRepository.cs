
using Domus.Application.DTOs.Chat.Response;
using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IChatRepository
{
    Task AddAsync(Chat chat, CancellationToken cancellationToken = default);
    Task<ResponseChat?> BuscarPorImovelELocatarioAsync(
        Guid imovel_id, Guid locatario_id, CancellationToken cancellationToken = default);
    Task<Chat?> BuscarPorIdAsync(Guid chatId, CancellationToken cancellationToken = default);
    Task<ResponseChat?> BuscarPorIdResponse(Guid chatId, Guid user_id, CancellationToken cancellationToken = default);
    Task AddMensagemAsync(MensagemChat mensagem, CancellationToken cancellationToken = default);

}
