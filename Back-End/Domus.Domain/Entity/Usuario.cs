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
    public ICollection<UsuarioFuncao> UsuarioFuncao { get; private set; } = new List<UsuarioFuncao>();
    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();

    public ICollection<Notificacao> Notificacoes { get; private set; } = new List<Notificacao>();
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();

    public ICollection<UsuarioChat> UsuarioChats { get; private set; } = new List<UsuarioChat>();
    public ICollection<Imovel> Imoveis { get; private set; } = new List<Imovel>();

    public ICollection<Contrato> ContratosComoLocador { get; private set; }
    public ICollection<Contrato> ContratosComoLocatario { get; private set; }

    public ICollection<Reclamacao> Reclamacoes { get; private set; } = new List<Reclamacao>();
    public Endereco Endereco { get; private set; }

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Usuario() { }

    public Usuario(string nome, string email, string senhaHash, Guid? enderecoId = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do usuário é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O email do usuário é obrigatório.", nameof(email));
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("A senha do usuário é obrigatória.", nameof(senhaHash));

        Usuario_ID = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Status = true; 
        Endereco_ID = enderecoId;
    }
}
