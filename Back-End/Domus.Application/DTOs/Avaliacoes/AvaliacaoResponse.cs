
namespace Domus.Application.DTOs.Avaliacoes;

public class AvaliacaoResponse
{
    public Guid Avaliacao_ID { get; set; }
    public AvaliacaoUsuarioResponse Usuario { get; set; }
    public AvaliacaoImovelResponse Imovel { get; set; }
    public AvaliacaoContratoResponse Contrato { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Nota { get; set; }
    public DateTime PublicadoEm { get; set; }
}
