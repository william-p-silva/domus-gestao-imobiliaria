

using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Reclamacao
{
    public Guid Reclamacao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public DateTime DataInicio { get; private set; } = DateTime.UtcNow;
    public DateTime? DataResolucao { get; private set; }
    public StatusReclamacao Status { get; private set; } = StatusReclamacao.Pendente;

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Imovel Imovel { get; private set; }
    public ICollection<MensagemReclamacao> MensagemReclamacoes { get; private set; } = new List<MensagemReclamacao>();
}
