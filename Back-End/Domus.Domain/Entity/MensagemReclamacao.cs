
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

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected MensagemReclamacao() { }

    public MensagemReclamacao(Guid reclamacao_id, Guid emissor_id, string texto)
    {
        if (reclamacao_id == Guid.Empty)
            throw new ArgumentException("O ID da reclamação é obrigatório.", nameof(reclamacao_id));
        if (emissor_id == Guid.Empty)
            throw new ArgumentException("O ID do emissor é obrigatório.", nameof(emissor_id));
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("O texto da mensagem é obrigatório.", nameof(texto));

        MensagemReclamacao_ID = Guid.NewGuid();
        Reclamacao_ID = reclamacao_id;
        Emissor_ID = emissor_id;
        Texto = texto;
    }
}
