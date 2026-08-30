
using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IChatRepository
{
    Task AddAsync(Chat chat, CancellationToken cancellationToken = default);
    Task<Chat?> BuscarPorImovelELocatarioAsync(Guid imovel_id, Guid locatario_id,  
        CancellationToken cancellationToken = default);

}
