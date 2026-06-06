using Domus.Domain.Enums;

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


    private readonly List<UsuarioFuncao> _usuarioFuncao = new();

    //Relacionamentos
    public IReadOnlyCollection<UsuarioFuncao> UsuarioFuncao => _usuarioFuncao.AsReadOnly();
    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();

    public ICollection<Notificacao> Notificacoes { get; private set; } = new List<Notificacao>();
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();

    public ICollection<UsuarioChat> UsuarioChats { get; private set; } = new List<UsuarioChat>();
    public ICollection<Imovel> Imoveis { get; private set; } = new List<Imovel>();

    public ICollection<Contrato> ContratosComoLocador { get; private set; }
    public ICollection<Contrato> ContratosComoLocatario { get; private set; }

    public ICollection<MensagemReclamacao> MensagensReclamacao { get; private set; } = new List<MensagemReclamacao>();
    public ICollection<Reclamacao> Reclamacoes { get; private set; } = new List<Reclamacao>();

    public Endereco EnderecoUsuario { get; private set; }

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


    public void AddFuncaoUsuario(Funcao funcao)
    {
        if (funcao.Funcao_ID == Guid.Empty)
            throw new ArgumentException("É preciso um id valido para esta ação", nameof(funcao.Funcao_ID));

        if (_usuarioFuncao.Any(uf => uf.Funcao_ID == funcao.Funcao_ID))
            throw new ArgumentException("O usuário já possui esta função.");

        var usuarioFuncao = new UsuarioFuncao(usuarioId: Usuario_ID, funcao: funcao);
        _usuarioFuncao.Add(usuarioFuncao);
    }

    public void AlterarEmail(string novoEmail)
    {
        if (string.IsNullOrWhiteSpace(novoEmail))
            throw new ArgumentException("O email do usuário é obrigatório.", nameof(novoEmail));

        Email = novoEmail;
    }

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("O nome do usuário é obrigatório.", nameof(novoNome));
        Nome = novoNome;
    }
    public void AlterarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novaSenhaHash))
            throw new ArgumentException("A senha do usuário é obrigatória.", nameof(novaSenhaHash));
        SenhaHash = novaSenhaHash;
    }

    public void DesativarUsuario()
    {
        Status = false;
    }

    public void AtivarUsuario()
    {
        Status = true;
    }

    public void AdicionarEndereco(Guid enderecoId)
    {
        if (enderecoId == Guid.Empty)
            throw new ArgumentException("É preciso um id valido para esta ação", nameof(enderecoId));

        Endereco_ID = enderecoId;
    }

    public void RemoverEndereco()
    {
        Endereco_ID = null;
    }

    public bool PossuiFuncao(FuncaoUser funcao)
    {
        return _usuarioFuncao != null && _usuarioFuncao.Any(uf =>
            uf.Funcao != null &&
            uf.Funcao.Nome == funcao);
    }
}
