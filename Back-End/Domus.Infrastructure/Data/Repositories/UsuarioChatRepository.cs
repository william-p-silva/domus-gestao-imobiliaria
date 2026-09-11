

using Domus.Application.DTOs.UsuarioChat.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class UsuarioChatRepository(AppDbContext context) : IUsuarioChatRepository
{
    public async Task<List<ResponseUserChats>> ListUserChats(Guid user_id, CancellationToken cancellationToken = default)
    {
        return await context.UsuariosChat
            .Where(x => x.Usuario_ID == user_id)
            .Select(x => new ResponseUserChats
            {
                UsuarioChat_ID = x.UsuarioChat_ID,
                Usuario_ID = x.Usuario_ID,
                Chat_ID = x.Chat_ID,

                Email = x.Chat.UsuarioChats
                    .FirstOrDefault(x => x.Usuario_ID != user_id)
                        .Usuario.Email.Endereco ?? "",
                NomeUsuario = x.Chat.UsuarioChats
                    .FirstOrDefault(x => x.Usuario_ID != user_id)
                        .Usuario.Nome.NomeCompleto ?? "",

                NomeChat = x.Chat.Nome,

                Funcao = x.Funcao.ToString(),

                EstadoMensagem = x.Chat.MensagensChat
                    .OrderByDescending(m => m.DataEnvio)
                    .Select(m => m.Estado.ToString())
                    .FirstOrDefault() ?? string.Empty,

                TextoMensagem = x.Chat.MensagensChat
                    .OrderByDescending(m => m.DataEnvio)
                    .Select(m => m.Texto)
                    .FirstOrDefault() ?? string.Empty,

                DataUltimaMensagem = x.Chat.MensagensChat
                    .OrderByDescending(m => m.DataEnvio)
                    .Select(m => m.DataEnvio)
                    .FirstOrDefault(),

                ImagemUrl = string.Empty
            }).ToListAsync(cancellationToken);
    }
}
