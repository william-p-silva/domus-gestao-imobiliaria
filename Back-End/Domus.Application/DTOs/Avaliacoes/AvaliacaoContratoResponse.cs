
namespace Domus.Application.DTOs.Avaliacoes;

public class AvaliacaoContratoResponse
{
    public DateTime CriadoEm { get; set; }
    public DateTime? DataTermino { get; set; }
    public DateTime? DataInicio { get; set; }
    public Guid Contrato_ID { get; set; }
}
