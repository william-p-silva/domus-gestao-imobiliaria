
using Domus.Application.DTOs.UsuarioChat.Response;
using Domus.Domain.Entity;

namespace Domus.Application.Mappers.UsuarioChats;

public static class UsuarioChatMapper
{
    public static ResponseUserChats ToResponse(this UsuarioChat userChat)
    {
        return new ResponseUserChats
        {
            Chat_ID = userChat.Chat_ID,
            Usuario_ID = userChat.Usuario_ID,
            Funcao = userChat.Funcao.ToString(),
            NomeChat = userChat.ChatNome.Nome,
            UsuarioChat_ID = userChat.UsuarioChat_ID,
            NomeUsuario = userChat.Usuario.Nome.NomeCompleto,
            Email = userChat.Usuario.Email.Endereco,
            EstadoMensagem = userChat.MensagensChat.FirstOrDefault().Estado.ToString(),
            TextoMensagem = userChat.MensagensChat.FirstOrDefault().Texto,
            DataUltimaMensagem = userChat.MensagensChat.FirstOrDefault().DataEnvio,
            ImagemUrl = ""
        };
    }
}
