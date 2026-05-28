
namespace Domus.Domain.Entity;

public class UsuarioFucao
{
    public Guid UsuarioFuncao_ID { get; private set; }
    public Guid Funcao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Funcao Funcao { get; private set; }
    public Usuario Usuario { get; private set; }
}
