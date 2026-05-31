
namespace Domus.Application.DTOs.Avaliacoes;

public class AvaliacaoResponse
{
    public Guid Avaliacao_ID { get; set; }
    public Guid Usuario_ID { get; set; }
    public Guid Imovel_ID { get; set; }
    public Guid Contrato_ID { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Nota { get; set; }
    public DateTime PublicadoEm { get; set; }
}
