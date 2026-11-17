
namespace Domus.Application.DTOs.Caracteristica.Response;

public sealed record GrupoCaracteristicaResponse
{
    public Guid GrupoCaracteristica_ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public DateTime? DeletadoEm { get; set; }
}
