
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
                        imovel = new ResponseImovelChat
                        {
                            Imovel_ID = chat.Imovel_ID,
                            Banheiros = chat.Imovel.Banheiros,
                            Comodos = chat.Imovel.Comodos,
                            Descricao = chat.Imovel.Descricao,
                            MetrosQuadrados = chat.Imovel.MetrosQuadrados,
                            Titulo = chat.Imovel.Titulo,
                            ValorAluguel = chat.Imovel.ValorAluguel,
                            Imagens = chat.Imovel.Imagens.Select(i => new ResponseImagemImovel
                            {
                                ImagemImovel_ID = i.ImagemImovel_ID,
                                Titulo = i.Titulo,
                                UrlImagem = i.UrlImagem
                            }).ToList()
                        },

                        Participantes = chat.UsuarioChats.Select(uc => new ResponseUsuariosChat
                        {
                            CriadoEm = uc.CriadoEm,
                            Email = uc.Usuario.Email.Endereco,
                            Estado = uc.Estado.ToString(),
                            Funcao = uc.Funcao.ToString(),
                            UsuarioChat_ID = uc.UsuarioChat_ID,
                            Usuario_ID = uc.Usuario_ID,
                            Nome = uc.Usuario.Nome.NomeCompleto
                        }).ToList(),

                        Mensagens = chat.MensagensChat.Select(m => new ResponseMensagemChat
                        {
                            Usuario_ID = m.UsuarioChat.Usuario_ID,
                            Chat_ID = m.Chat_ID,
                            DataEnvio = m.DataEnvio,
                            Estado = m.Estado.ToString(),
                            MensagemChat_ID = m.MensagemChat_ID,
                            Texto = m.Texto,
                        })
                        .OrderBy(x => x.DataEnvio)
                        .ToList()
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

    public async Task<ResponseChat?> BuscarPorIdResponse(Guid chatId, Guid user_id, CancellationToken cancellationToken = default)
    {
        return await context.Chats
                .AsSplitQuery()
                .Where(
                    c => c.Chat_ID == chatId
                    && c.UsuarioChats.Any(x => x.Usuario_ID == user_id))
                .Select(
                    chat => new ResponseChat
                    {
                        Chat_ID = chat.Chat_ID,
                        CriadoEm = chat.CriadoEm,
                        Estado = chat.Estado.ToString(),
                        Nome = chat.Nome,

                        // Abordagem direta e traduzível para o imóvel do chat
                        imovel = new ResponseImovelChat
                        {
                            Imovel_ID = chat.Imovel_ID,
                            Banheiros = chat.Imovel.Banheiros,
                            Comodos = chat.Imovel.Comodos,
                            Descricao = chat.Imovel.Descricao,
                            MetrosQuadrados = chat.Imovel.MetrosQuadrados,
                            Titulo = chat.Imovel.Titulo,
                            ValorAluguel = chat.Imovel.ValorAluguel,
                            Imagens = chat.Imovel.Imagens.Select(i => new ResponseImagemImovel
                            {
                                ImagemImovel_ID = i.ImagemImovel_ID,
                                Titulo = i.Titulo,
                                UrlImagem = i.UrlImagem
                            }).ToList()
                        },

                        Participantes = chat.UsuarioChats.Select(uc => new ResponseUsuariosChat
                        {
                            CriadoEm = uc.CriadoEm,
                            Email = uc.Usuario.Email.Endereco,
                            Estado = uc.Estado.ToString(),
                            Funcao = uc.Funcao.ToString(),
                            UsuarioChat_ID = uc.UsuarioChat_ID,
                            Usuario_ID = uc.Usuario_ID,
                            Nome = uc.Usuario.Nome.NomeCompleto
                        }).ToList(),

                        Mensagens = chat.MensagensChat.Select(m => new ResponseMensagemChat
                        {
                            Usuario_ID = m.UsuarioChat.Usuario_ID,
                            Chat_ID = m.Chat_ID,
                            DataEnvio = m.DataEnvio,
                            Estado = m.Estado.ToString(),
                            MensagemChat_ID = m.MensagemChat_ID,
                            Texto = m.Texto,
                        })
                        .OrderBy(x => x.DataEnvio)
                        .ToList()
                    }
                ).FirstOrDefaultAsync();
    }
}
