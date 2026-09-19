
namespace Domus.Application.DTOs.Imovel;

public sealed record ResponseContratoImovel
{
    public Guid Contrato_ID { get; set; }
    public string UrlContrato { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public string Status { get; set; } = string.Empty;
}
