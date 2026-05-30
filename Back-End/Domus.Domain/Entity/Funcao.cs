using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Funcao
{
    public Guid Funcao_ID { get; private set; }
    public FuncaoUser Nome { get; private set; }

    //Relacionamentos
    public ICollection<UsuarioFuncao> UsuarioFuncao { get; private set; } = new List<UsuarioFuncao>();

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Funcao() { }

    public Funcao(FuncaoUser perfil)
    {
        if (!Enum.IsDefined(typeof(FuncaoUser), perfil))
            throw new ArgumentException("Perfil inválido.", nameof(perfil));
        Funcao_ID = Guid.NewGuid();
        Nome = perfil;
    }

    // NOVO: Construtor para Seeds / Dados Estáticos (IDs fixos)
    public Funcao(Guid id, FuncaoUser perfil)
    {
        if (!Enum.IsDefined(typeof(FuncaoUser), perfil))
            throw new ArgumentException("Perfil inválido.", nameof(perfil));

        Funcao_ID = id;
        Nome = perfil;
    }
}
