

using Domus.Application.DTOs.Chat.Response;
using Domus.Application.DTOs.Imovel;
using Domus.Domain.Entity;

namespace Domus.Application.Mappers.Chat;

public static class ChatMapper
{
    public static ResponseChat ToResponse(this Domain.Entity.Chat chat)
    {
        return new ResponseChat
        {
            Chat_ID = chat.Chat_ID,
            CriadoEm = chat.CriadoEm,
            Estado = chat.Estado.ToString(),
            Nome = chat.Nome,

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

            Participantes = chat.UsuarioChats.Select(u => new ResponseUsuariosChat
            {
                CriadoEm = u.CriadoEm,
                Funcao = u.Funcao.ToString(),
                UsuarioChat_ID = u.UsuarioChat_ID,
                Usuario_ID = u.Usuario_ID,
                Email = u.Usuario.Email.Endereco,
                Estado = u.Estado.ToString(),
                Nome = u.Usuario.Nome.NomeCompleto
            }).ToList(),

            Mensagens = chat.MensagensChat.Select(m => new ResponseMensagemChat
            {
                Usuario_ID = m.UsuarioChat.Usuario_ID,
                Chat_ID = m.Chat_ID,
                DataEnvio = m.DataEnvio,
                Estado = m.Estado.ToString(),
                MensagemChat_ID = m.MensagemChat_ID,
                Texto = m.Texto
            }).ToList() ?? new List<ResponseMensagemChat>()       
        };
    }
}
