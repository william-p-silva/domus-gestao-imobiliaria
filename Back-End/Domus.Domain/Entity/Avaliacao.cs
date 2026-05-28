
namespace Domus.Domain.Entity;

public class Avaliacao
{
    public Guid Avaliacao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public Guid Contrato_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public int Nota { get; private set; }
    public DateTime PublicadoEm { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Imovel Imovel { get; private set; }
    public Contrato Contrato { get; private set; }
}
