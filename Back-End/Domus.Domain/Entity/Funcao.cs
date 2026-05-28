
using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Funcao
{
    public Guid Funcao_ID { get; private set; }
    public Perfil Perfil { get; private set; }

    //Relacionamentos
    public ICollection<UsuarioFucao> UsuarioFuncao { get; private set; } = new List<UsuarioFucao>();
}
