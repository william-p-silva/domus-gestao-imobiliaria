
using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Contrato.CicloDeVida;

public class ResponseMinutaContrato
{
    public Guid Contrato_ID { get; set; }
    public Guid Imovel_ID { get; set; }
    public ContratoLocadorResponse Locador { get; set; }
    public ContratoLocatarioResponse Locatario { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Tipo { get; set; }
    public string UrlContrato { get; set; }
    public DateTime CriadoEm { get; set; }
    public StatusContrato Status { get; set; }
    public bool AssinaturaLocador { get; set; }
    public bool AssinaturaLocatario { get; set; }
}
