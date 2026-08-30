
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class ChatRepository(AppDbContext context) : IChatRepository
{
    public async Task AddAsync(Chat chat, CancellationToken cancellationToken = default)
    {
        await context.Chats.AddAsync(chat, cancellationToken);
    }

    public async Task<Chat?> BuscarPorImovelELocatarioAsync(Guid imovel_id, Guid locatario_id, CancellationToken cancellationToken = default)
    {
        return await context.Chats.FirstOrDefaultAsync(
            c => c.Imovel_ID == imovel_id && c.UsuarioChats.Any(x => x.Usuario_ID == locatario_id));
    }
}
