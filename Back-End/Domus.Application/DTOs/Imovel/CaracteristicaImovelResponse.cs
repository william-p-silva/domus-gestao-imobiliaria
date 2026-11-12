

namespace Domus.Application.DTOs.Imovel;

public sealed record CaracteristicaImovelResponse
{
    public Guid Caracteristica_ID { get; set; }
    public Guid CaracteristicaImovel_ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public DateTime? DeletadoEm { get; set; }
}