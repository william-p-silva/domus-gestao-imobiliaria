
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

    public async Task<Chat?> BuscarPorIdAsync(Guid chatId, CancellationToken cancellationToken = default)
    {
        return await context.Chats
            .Include(c => c.UsuarioChats)
                .ThenInclude(uc => uc.Usuario)
            .Include(c => c.MensagensChat)
            .FirstOrDefaultAsync(c => c.Chat_ID == chatId, cancellationToken);
    }

    public async Task AddMensagemAsync(MensagemChat mensagem, CancellationToken cancellationToken = default)
    {
        await context.MensagensChat.AddAsync(mensagem, cancellationToken);
    }
}
