

namespace Domus.Application.DTOs.Contrato.CicloDeVida;

public class RequestAssinarContrato
{
    public Guid Contrato_ID { get; set; }
    public DateTime DataTermino { get; set; }
}
