
using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Imovel
{
    public Guid Imovel_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Endereco_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public int Comodos { get; private set; }
    public StatusImovel Status { get; private set; }
    public decimal ValorAluguel { get; private set; }

    //Relacionamentos
    public ICollection<Reclamacao> Reclamacoes { get; private set; } = new List<Reclamacao>();
    public ICollection<Contrato> Contratos { get; private set; } = new List<Contrato>();

    public ICollection<ImagemImovel> Imagens { get; private set; } = new List<ImagemImovel>();
    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();

    public ICollection<Chat> Chats { get; private set; } = new List<Chat>();
    public ICollection<MensagemReclamacao> MensagensReclamacao { get; private set; } = new List<MensagemReclamacao>();

    public Usuario Usuario { get; private set; }
    public Endereco Endereco { get; private set; }

}
