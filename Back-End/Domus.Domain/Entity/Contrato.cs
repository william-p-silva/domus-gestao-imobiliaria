

using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Contrato
{
    public Guid Contarto_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public Guid Locador_ID { get; private set; }
    public Guid Locatario_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Tipo { get; private set; }
    public string UrlContrato { get; private set; }
    public DateTime DataInicio { get; private set; } = DateTime.UtcNow;
    public DateTime DataTermino { get; private set; }
    public StatusContrato Status { get; private set; }

    //Relacionamentos
    public List<ParcelaAluguel> ParcelasAluguel { get; private set; }
    public Imovel Imovel { get; private set; }
    public Usuario Locador { get; private set; }
    public Usuario Locatario { get; private set; }
}
