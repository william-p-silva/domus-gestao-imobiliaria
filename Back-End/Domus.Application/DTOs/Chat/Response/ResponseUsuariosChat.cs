



namespace Domus.Application.DTOs.Chat.Response;

public sealed record ResponseUsuariosChat
{
    public Guid UsuarioChat_ID { get; set; }
    public Guid Usuario_ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Funcao { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }

}
