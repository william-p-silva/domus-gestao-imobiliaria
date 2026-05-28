
namespace Domus.Domain.Entity;

public class Notificacao
{
    public Guid Notificacao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Mensagem { get; private set; }
    public bool Lida { get; private set; }
    public DateTime DataEnvio { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Usuario Usuario { get; private set; }

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Notificacao() { }

    public Notificacao(Guid usuarioId, string titulo, string mensagem)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("O ID do usuário não pode ser vazio.", nameof(usuarioId));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título da notificação não pode ser vazio.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(mensagem))
            throw new ArgumentException("A mensagem da notificação não pode ser vazia.", 
                nameof(mensagem));

        Notificacao_ID = Guid.NewGuid();
        Usuario_ID = usuarioId;
        Titulo = titulo;
        Mensagem = mensagem;
        Lida = false; // Por padrão, a notificação é criada como não lida
    }
}
