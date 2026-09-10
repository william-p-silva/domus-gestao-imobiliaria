

using Domus.Application.DTOs.UsuarioChat.Response;
using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IUsuarioChatRepository
{
    Task<List<ResponseUserChats>> ListUserChats(
        Guid user_id, CancellationToken cancellationToken = default);
}
