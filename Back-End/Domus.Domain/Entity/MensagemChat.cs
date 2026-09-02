using Domus.Domain.Enums.Mensagem;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Domain.Entity;

public class MensagemChat
{
    public Guid MensagemChat_ID { get; private set; }
    public Guid UsuarioChat_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public string Texto { get; private set; }
    public EstadoMensagem Estado { get; private set; }
    public DateTime? DeletadaEm { get; private set; }
    public DateTime DataEnvio { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public UsuarioChat UsuarioChat { get; private set; }
    public Chat Chat { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected MensagemChat() { }

    public MensagemChat(UsuarioChat usuarioChat, Chat chat, string texto)
    {
        if (usuarioChat is null)
            throw new ValidationException("O usuário é obrigatório.");
        if (chat is null)
            throw new ValidationException("O chat é obrigatório.");
        if (string.IsNullOrWhiteSpace(texto))
            throw new BusinessRuleException("O texto da mensagem é obrigatório.");
        if(texto.Length > 4000)
            throw new BusinessRuleException("O texto da mensagem não pode exceder 4000 caracteres.");

        if(usuarioChat.Chat_ID != chat.Chat_ID)
            throw new ValidationException("O usuário não pertence a este chat.");

        MensagemChat_ID = Guid.NewGuid();
        UsuarioChat_ID = usuarioChat.UsuarioChat_ID;
        Chat_ID = chat.Chat_ID;
        Texto = texto;
        Estado = EstadoMensagem.Enviada;

        Chat = chat;
        UsuarioChat = usuarioChat;
    }
}
