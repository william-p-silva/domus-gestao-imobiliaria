namespace Domus.Application.DTOs.Chat.Response;

public sealed record EnviarMensagemResponse
{
    public Guid MensagemChat_ID { get; init; }
    public Guid Chat_ID { get; init; }
    public Guid UsuarioChat_ID { get; init; }
    public Guid Usuario_ID { get; init; }
    public string Texto { get; init; } = string.Empty;
    public DateTime DataEnvio { get; init; }
}
