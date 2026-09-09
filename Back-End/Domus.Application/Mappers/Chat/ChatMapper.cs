

using Domus.Application.DTOs.Chat.Response;
using Domus.Domain.Entity;

namespace Domus.Application.Mappers.Chat;

public static class ChatMapper
{
    public static ResponseChat ToResponse(this Domain.Entity.Chat chat)
    {
        return new ResponseChat()
        {
            Chat_ID = chat.Chat_ID,
            CriadoEm = chat.CriadoEm,
            Estado = chat.Estado.ToString(),
            Imovel_ID = chat.Imovel_ID,
            Nome = chat.Nome,
            Mensagens = chat.MensagensChat.Select(m => new EnviarMensagemResponse
            {
                MensagemChat_ID = m.Chat_ID,
                Chat_ID = m.Chat_ID,
                Texto = m.Texto,
                DataEnvio = m.DataEnvio,
                UsuarioChat_ID = m.UsuarioChat_ID,
                Usuario_ID = m.UsuarioChat.Usuario_ID
            }).ToList(),
            Participantes = chat.UsuarioChats.Select(uc => new ResponseUsuariosChat
            {
                Usuario_ID = uc.Usuario_ID,
                Nome = uc.Usuario.Nome.NomeCompleto,
                Email = uc.Usuario.Email.Endereco,
                CriadoEm = uc.CriadoEm,
                Estado = uc.Estado.ToString(),
                Funcao = uc.Funcao.ToString(),
                UsuarioChat_ID = uc.UsuarioChat_ID
            }).ToList()
        };
    }
}
