using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Funcao
{
    public Guid Funcao_ID { get; private set; }
    public Perfil Nome { get; private set; }

    //Relacionamentos
    public ICollection<UsuarioFuncao> UsuarioFuncao { get; private set; } = new List<UsuarioFuncao>();

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Funcao() { }

    public Funcao(Perfil perfil)
    {
        if (!Enum.IsDefined(typeof(Perfil), perfil))
            throw new ArgumentException("Perfil inválido.", nameof(perfil));
        Funcao_ID = Guid.NewGuid();
        Nome = perfil;
    }
}
