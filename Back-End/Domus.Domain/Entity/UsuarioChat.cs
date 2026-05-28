namespace Domus.Domain.Entity;

public class UsuarioChat
{
    public Guid UsuarioChat_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Chat_ID { get; private set; }

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Chat Chat { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected UsuarioChat() { }

    public UsuarioChat(Guid usuarioId, Guid chatId)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(usuarioId));
        if (chatId == Guid.Empty)
            throw new ArgumentException("O ID do chat é obrigatório.", nameof(chatId));

        UsuarioChat_ID = Guid.NewGuid();
        Usuario_ID = usuarioId;
        Chat_ID = chatId;
    }
}
