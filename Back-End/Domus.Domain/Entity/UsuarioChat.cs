using Domus.Domain.Enums;
using Domus.Domain.Enums.Chat;
using Domus.Domain.Exceptions.Domain;
using Domus.Domain.ValueObjects.Chat;

namespace Domus.Domain.Entity;

public class UsuarioChat
{
    public Guid UsuarioChat_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Chat_ID { get; private set; }
    public NomeChat ChatNome { get; private set; }
    public FuncaoUser Funcao { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? DeletadoEm { get; private set; }
    public EstadoUsuarioChat Estado { get; private set; }

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Chat Chat { get; private set; }
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();



    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected UsuarioChat() { }

    public UsuarioChat(
        Usuario usuario, 
        FuncaoUser funcao,
        Chat chat,
        string nome
        )
    {
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado. Verifique as informações.");
        if (chat is null)
            throw new NotFoundException("Chat não encontrado. Verifique as informações.");

        if (!usuario.PossuiFuncao(funcao))
            throw new BusinessRuleException("Usuário não contempla a função exigida.");

        UsuarioChat_ID = Guid.NewGuid();
        Usuario_ID = usuario.Usuario_ID;
        Chat_ID = chat.Chat_ID;
        Estado = EstadoUsuarioChat.Ativo;
        CriadoEm = DateTime.UtcNow;
        Funcao = funcao;
        ChatNome = NomeChat.Create(nome);
    }
}
