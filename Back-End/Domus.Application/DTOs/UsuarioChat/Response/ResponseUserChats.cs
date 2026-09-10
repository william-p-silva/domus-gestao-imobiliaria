

namespace Domus.Application.DTOs.UsuarioChat.Response;

public sealed record ResponseUserChats
{
    public Guid UsuarioChat_ID { get; set; }
    public Guid Usuario_ID { get; set; }
    public Guid Chat_ID { get; set; }

    public string Email { get; set; } = string.Empty;
    public string NomeUsuario { get; set; } = string.Empty;

    public string NomeChat { get; set; } = string.Empty;
    public string Funcao { get; set; } = string.Empty;

    public string EstadoMensagem { get; set; } = string.Empty;
    public string TextoMensagem { get; set; } = string.Empty;

    public DateTime? DataUltimaMensagem { get; set; }

    public string ImgagemUrl { get; set; } = string.Empty;
}