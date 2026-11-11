

namespace Domus.Application.DTOs.Caracteristica.Response;

public sealed record CaracteristicaResponse
{
    public Guid Caracteristica_ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public DateTime? DeletadoEm { get; set; }
}