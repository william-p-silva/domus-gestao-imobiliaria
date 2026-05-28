namespace Domus.Domain.Entity;

public class UsuarioChat
{
    public Guid UsuarioChat_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Chat_ID { get; private set; }

    //Relacionamentos
    public Usuario Usuario { get; private set; } = new List<Usuario>();
    public Chat Chat { get; private set; }
}
