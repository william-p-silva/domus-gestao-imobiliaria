
namespace Domus.Domain.Entity;

public class MensagemReclamacao
{
    public Guid MensagemReclamacao_ID { get; private set; }
    public Guid Reclamacao_ID { get; private set; }
    public Guid Emissor_ID { get; private set; }
    public string Texto { get; private set; }

    //Relacionamentos
    public Reclamacao Reclamacao { get; private set; }
    public Usuario Emissor { get; private set; }
}
