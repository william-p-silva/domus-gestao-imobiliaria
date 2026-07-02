
using Domus.Application.DTOs.Endereco;
using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Imovel;

public class ImovelResponse
{
    public Guid Imovel_ID { get; set; }
    public Guid Usuario_ID { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Comodos { get; set; }
    public string Status { get; set; }
    public decimal ValorAluguel { get; set; }
    public DateTime CriadoEm { get; set; }
    public bool Aprovado { get; set; }
    public bool Avaliado { get; set; }
    public decimal MetrosQuadrados { get; set; }
    public int Banheiros { get; set; }
    public string TipoDoImovel { get; set; }
    public EnderecoResponse Endereco { get; set; }
}
