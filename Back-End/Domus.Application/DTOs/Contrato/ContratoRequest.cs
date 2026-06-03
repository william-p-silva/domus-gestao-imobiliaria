
namespace Domus.Application.DTOs.Contrato;

public class ContratoRequest
{
    public Guid Imovel_ID { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Tipo { get; set; }
    public string UrlContrato { get; set; }
}

