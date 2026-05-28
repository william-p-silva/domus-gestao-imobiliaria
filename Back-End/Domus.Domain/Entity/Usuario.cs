namespace Domus.Domain.Entity;

public class Usuario
{
    public Guid Usuario_ID { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public bool Status { get; private set; }
    public string SenhaHash { get; private set; }
    public Guid? Endereco_ID { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public ICollection<UsuarioFucao> UsuarioFuncao { get; private set; } = new List<UsuarioFucao>();
    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();
    public ICollection<Notificacao> Notificacoes { get; private set; } = new List<Notificacao>();
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();
    public ICollection<UsuarioChat> UsuarioChats { get; private set; } = new List<UsuarioChat>();
    public ICollection<Imovel> Imoveis { get; private set; } = new List<Imovel>();
    public ICollection<Reclamacao> Reclamacoes { get; private set; } = new List<Reclamacao>();
    public ICollection<Contrato> Contratos { get; private set; } = new List<Contrato>();

    public Endereco Endereco { get; private set; }
}
