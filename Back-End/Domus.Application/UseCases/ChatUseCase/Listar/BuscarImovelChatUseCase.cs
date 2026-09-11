

using Domus.Application.DTOs.Chat.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Chat;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.ChatUseCase.Listar;

public class BuscarImovelChatUseCase(
    IChatRepository chatRepository,
    IUsuarioRepository usuarioRepository,
    IImovelRepository imovelRepository,
    IUnitOfWork unitOfWork
    )
{

    public async Task<ResponseChat> ExecuteAsync(
        Guid imovel_id, Guid locatario_id, CancellationToken cancellationToken)
    {
        var chatExist = await chatRepository.BuscarPorImovelELocatarioAsync(
            imovel_id, locatario_id, cancellationToken);

        if (chatExist is not null)
        {
            return chatExist;
        }

        var locatario = await usuarioRepository.BuscarPorIdAsync(locatario_id, cancellationToken)
            ?? throw new NotFoundException("Locatario não encontrado.");

        var imovel = await imovelRepository.BuscarPorIdAsync(imovel_id, cancellationToken)
            ?? throw new NotFoundException("Imóvel não encontrado.");

        var locador = imovel.Usuario ?? throw new NotFoundException("Locador não encontrado.");

        Chat chat = new Chat(imovel);

        chat.AdicionarUsuarios(locador: locador, locatario: locatario, nome: imovel.Titulo);

        await chatRepository.AddAsync(chat);

        await unitOfWork.CommitAsync();

        return chat.ToResponse();
    }
}
