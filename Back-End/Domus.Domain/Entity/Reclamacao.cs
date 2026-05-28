

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
    public StatusReclamacao Status { get; private set; }

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Imovel Imovel { get; private set; }
    public ICollection<MensagemReclamacao> MensagemReclamacoes { get; private set; } = new List<MensagemReclamacao>();

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Reclamacao() { }

    public Reclamacao(Guid usuarioId, Guid imovelId, string titulo, string descricao)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(usuarioId));
        if (imovelId == Guid.Empty)
            throw new ArgumentException("O ID do imóvel é obrigatório.", nameof(imovelId));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título da reclamação é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da reclamação é obrigatória.", nameof(descricao));

        Reclamacao_ID = Guid.NewGuid();
        Usuario_ID = usuarioId;
        Imovel_ID = imovelId;
        Titulo = titulo;
        Descricao = descricao;
        Status = StatusReclamacao.EmAndamento;
    }
}
