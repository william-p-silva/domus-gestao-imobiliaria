
namespace Domus.Domain.Entity;

public class UsuarioFuncao
{
    public Guid UsuarioFuncao_ID { get; private set; }
    public Guid Funcao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Funcao Funcao { get; private set; }
    public Usuario Usuario { get; private set; }

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected UsuarioFuncao() { }

    public UsuarioFuncao(Guid funcaoId, Guid usuarioId)
    {
        if (funcaoId == Guid.Empty)
            throw new ArgumentException("O ID da função é obrigatório.", nameof(funcaoId));
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(usuarioId));

        UsuarioFuncao_ID = Guid.NewGuid();
        Funcao_ID = funcaoId;
        Usuario_ID = usuarioId;
    }
}
