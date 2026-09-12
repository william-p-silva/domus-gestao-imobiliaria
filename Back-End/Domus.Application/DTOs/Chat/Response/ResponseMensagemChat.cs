

namespace Domus.Application.DTOs.Chat.Response;

public sealed record ResponseMensagemChat
{
    public Guid MensagemChat_ID { get; set; }
    public Guid Chat_ID { get; set; }
    public Guid Usuario_ID { get; set; }
    public string Texto { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime DataEnvio { get; set; }
}