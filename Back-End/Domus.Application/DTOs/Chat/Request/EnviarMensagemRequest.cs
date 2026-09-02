namespace Domus.Application.DTOs.Chat.Request;

public sealed record EnviarMensagemRequest
{
    public Guid Chat_ID { get; init; }
    public string Texto { get; init; } = string.Empty;
}
