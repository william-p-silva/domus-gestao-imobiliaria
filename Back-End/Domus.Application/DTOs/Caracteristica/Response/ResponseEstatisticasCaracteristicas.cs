

namespace Domus.Application.DTOs.Caracteristica.Response;

public sealed record ResponseEstatisticasCaracteristicas
{
    public int Ativas { get; set; }
    public int Inativas { get; set; }
    public decimal PorcentagemAtivas { get; set; }
    public int TotalImoveis { get; set; }
    public CaracteristicaResponse Top { get; set; } = new CaracteristicaResponse();
}
