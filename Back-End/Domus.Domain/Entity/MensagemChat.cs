
namespace Domus.Domain.Entity;

public class MensagemChat
{
    public Guid MensagemChat_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public string Texto { get; private set; }
    public DateTime DataEnvio { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Chat Chat { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected MensagemChat() { }

    public MensagemChat(Guid usuario_id, Guid chat_id, string texto)
    {
        if (usuario_id == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(usuario_id));
        if (chat_id == Guid.Empty)
            throw new ArgumentException("O ID do chat é obrigatório.", nameof(chat_id));
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("O texto da mensagem é obrigatório.", nameof(texto));

        MensagemChat_ID = Guid.NewGuid();
        Usuario_ID = usuario_id;
        Chat_ID = chat_id;
        Texto = texto;
    }
}
