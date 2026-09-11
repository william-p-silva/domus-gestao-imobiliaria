
using Domus.Application.DTOs.Chat.Response;
using Domus.Application.DTOs.Imovel;
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

    public async Task<ResponseChat?> BuscarPorImovelELocatarioAsync(Guid imovel_id, Guid locatario_id, CancellationToken cancellationToken = default)
    {
        return await context.Chats
                .AsSplitQuery()
                .Where(
                    c => c.Imovel_ID == imovel_id
                    && c.UsuarioChats.Any(x => x.Usuario_ID == locatario_id))
                .Select(
                    chat => new ResponseChat
                    {
                        Chat_ID = chat.Chat_ID,
                        CriadoEm = chat.CriadoEm,
                        Estado = chat.Estado.ToString(),
                        Nome = chat.Nome,

                        // Abordagem direta e traduzível para o imóvel do chat
                        imovel = chat.UsuarioChats
                            .Where(uc => uc.Usuario_ID != locatario_id)
                            .Select(uc => uc.Usuario.Imoveis.FirstOrDefault(i => i.Imovel_ID == imovel_id))
                            .Where(i => i != null)
                            .Select(i => new ResponseImovelChat
                            {
                                Imovel_ID = i.Imovel_ID,
                                Descricao = i.Descricao,
                                Comodos = i.Comodos,
                                Banheiros = i.Banheiros,
                                MetrosQuadrados = i.MetrosQuadrados,
                                Titulo = i.Titulo,
                                ValorAluguel = i.ValorAluguel,
                                Imagens = i.Imagens.Select(im => new ResponseImagemImovel
                                {
                                    ImagemImovel_ID = im.ImagemImovel_ID,
                                    Titulo = im.Titulo,
                                    UrlImagem = im.UrlImagem
                                }).ToList()
                            })
                            .FirstOrDefault(),

                        Participantes = chat.UsuarioChats.Select(uc => new ResponseUsuariosChat
                        {
                            CriadoEm = uc.CriadoEm,
                            Email = uc.Usuario.Email.Endereco,
                            Estado = uc.Estado.ToString(),
                            Funcao = uc.Funcao.ToString(),
                            UsuarioChat_ID = uc.UsuarioChat_ID,
                            Usuario_ID = uc.Usuario_ID,
                            Nome = uc.ChatNome.Nome
                        }).ToList(),

                        Mensagens = chat.MensagensChat.Select(m => new ResponseMensagemChat
                        {
                            DataEnvio = m.DataEnvio,
                            Estado = m.Estado.ToString(),
                            MensagemChat_ID = m.MensagemChat_ID,
                            Texto = m.Texto,
                        }).ToList()
                    }
                ).FirstOrDefaultAsync();
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
