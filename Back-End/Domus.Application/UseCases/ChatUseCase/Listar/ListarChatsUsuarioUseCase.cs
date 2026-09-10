

using Domus.Application.DTOs.UsuarioChat.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.UsuarioChats;

namespace Domus.Application.UseCases.ChatUseCase.Listar;

public class ListarChatsUsuarioUseCase(
    IUsuarioChatRepository usuarioChatRepository
    )
{
    public async Task<List<ResponseUserChats>> ExecuteAsync(Guid user_id, CancellationToken cancellationToken)
    {
        var userChats = await usuarioChatRepository.ListUserChats(user_id, cancellationToken);

        return userChats;
    }
}
