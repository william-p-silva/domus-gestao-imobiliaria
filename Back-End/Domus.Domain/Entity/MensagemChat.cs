
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
}
