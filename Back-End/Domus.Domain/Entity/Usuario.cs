using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Usuario
{
    public Guid Usuario_ID { get; private set; }
    public Guid? Endereco_ID { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string? CPF { get; private set; }
    public string? Celular { get; private set; }
    public bool Ativo { get; private set; }
    public string SenhaHash { get; private set; }
    public Guid TokenConfirmaEmail { get; private set; }
    public DateTime TokenEmailExpire { get; private set; } = DateTime.UtcNow.AddHours(2);
    public string EmailAConfirmar { get; private set; }
    public bool EmailConfirmado { get; private set; } = false;
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public DateTime? ExcluidoEm { get; private set; }


    private readonly List<UsuarioFuncao> _usuarioFuncao = new();

    //Relacionamentos
    public IReadOnlyCollection<UsuarioFuncao> UsuarioFuncao => _usuarioFuncao.AsReadOnly();
    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();

    public ICollection<Notificacao> Notificacoes { get; private set; } = new List<Notificacao>();

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


    public Usuario(
        string nome,
        string emailAConfirmar,
        string senhaHash,
        string? cpf = null,
        string? celular = null,
        Guid? enderecoId = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do usuário é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(emailAConfirmar))
            throw new ArgumentException("O email do usuário é obrigatório.", nameof(emailAConfirmar));
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("A senha do usuário é obrigatória.", nameof(senhaHash));

        Usuario_ID = Guid.NewGuid();
        TokenConfirmaEmail = Guid.NewGuid();
        Nome = nome;
        CPF = cpf;
        Celular = celular;
        EmailAConfirmar = emailAConfirmar;
        SenhaHash = senhaHash;
        Ativo = false;
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

    public void ConfirmarEmail()
    {

        if (EmailConfirmado)
            throw new InvalidOperationException("Email já confirmado.");

        Email = EmailAConfirmar;
        Ativo = true;
        EmailConfirmado = true;

        TokenConfirmaEmail = Guid.Empty;
        TokenEmailExpire = DateTime.MinValue;
    }

    public void AdicionarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("O CPF do usuário é obrigatório.", nameof(cpf));
        if (cpf.Count(c => char.IsDigit(c)) != 11)
            throw new ArgumentException("O CPF deve conter 11 dígitos.", nameof(cpf));
        if (cpf.Any(c => !char.IsDigit(c)))
            throw new ArgumentException("O CPF deve conter apenas números.", nameof(cpf));
        CPF = cpf;
    }

    public void AdicionarCelular(string celular)
    {
        if (string.IsNullOrWhiteSpace(celular))
            throw new ArgumentException("O celular do usuário é obrigatório.", nameof(celular));
        if (celular.Count(c => char.IsDigit(c)) != 11)
            throw new ArgumentException("O celular deve conter 11 dígitos.", nameof(celular));
        if (celular.Any(c => !char.IsDigit(c)))
            throw new ArgumentException("O celular deve conter apenas números.", nameof(celular));
        Celular = celular;
    }

    public void AlterarCelular(string celular)
    {
        if (string.IsNullOrWhiteSpace(celular))
            throw new ArgumentException("O celular do usuário é obrigatório.", nameof(celular));
        if (celular.Count(c => char.IsDigit(c)) != 11)
            throw new ArgumentException("O celular deve conter 11 dígitos.", nameof(celular));
        if (celular.Any(c => !char.IsDigit(c)))
            throw new ArgumentException("O celular deve conter apenas números.", nameof(celular));
        Celular = celular;
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
        if(!Ativo)
            throw new InvalidOperationException("O usuário já está desativado.");
        Ativo = false;
        ExcluidoEm = DateTime.UtcNow;
    }

    public void AtivarUsuario()
    {
        if (Ativo)
            throw new InvalidOperationException("O usuário já está ativo.");
        Ativo = true;
    }

    public void AdicionarEndereco(Guid enderecoId)
    {
        if (enderecoId == Guid.Empty)
            throw new ArgumentException("É preciso um id valido para esta ação", nameof(enderecoId));

        Endereco_ID = enderecoId;
    }

    public void RemoverEndereco()
    {
        if (Endereco_ID == Guid.Empty)
            throw new InvalidOperationException("O usuário não possui endereço para remover.");
        Endereco_ID = null;
    }

    public bool PossuiFuncao(FuncaoUser funcao)
    {
        return _usuarioFuncao != null && _usuarioFuncao.Any(uf =>
            uf.Funcao != null &&
            uf.Funcao.Nome == funcao);
    }
}
