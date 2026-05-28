
namespace Domus.Domain.Entity;

public class Notificacao
{
    public Guid Notificacao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Mensagem { get; private set; }
    public bool Lida { get; private set; }
    public DateTime DataEnvio { get; private set; }

    //Relacionamentos
    public Usuario Usuario { get; private set; }
}
