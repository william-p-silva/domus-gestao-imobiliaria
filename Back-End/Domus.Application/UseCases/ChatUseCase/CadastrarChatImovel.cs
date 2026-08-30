

using Domus.Application.DTOs.Chat.Request;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.ChatUseCase;

public class CadastrarChatImovel(
    IImovelRepository imovelRepository,
    IUsuarioRepository usuarioRepository,
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<Guid> ExecuteAsync(
        Guid locatario_id, 
        RequestNewChat request, 
        CancellationToken cancellationToken)
    {
        var existChat = await chatRepository.BuscarPorImovelELocatarioAsync(
            request.Imovel_ID, locatario_id, cancellationToken);
        if (existChat is not null)
            throw new BusinessRuleException("Já existe um chat com esse usuário e imovel.");

        var locatario = await usuarioRepository.BuscarPorIdAsync(locatario_id, cancellationToken)
            ?? throw new NotFoundException("Usuário não foi encontrado.");

        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken)
            ?? throw new NotFoundException("Imóvel não encontrado.");

        var locador = imovel.Usuario
            ?? throw new NotFoundException("Imóvel não encontrado.");

        Chat chat = new Chat(imovel);

        chat.AdicionarUsuarios(locador: locador, locatario: locatario, nome: imovel.Titulo);

        await chatRepository.AddAsync(chat, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return chat.Chat_ID;
    }
}
