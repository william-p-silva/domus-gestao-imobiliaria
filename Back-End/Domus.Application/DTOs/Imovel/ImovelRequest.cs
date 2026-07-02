

using Domus.Application.DTOs.Endereco;
using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Imovel;

public class ImovelRequest
{
    public Guid Usuario_ID { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Comodos { get; set; }
    public int Banheiros { get; set; }
    public decimal MetrosQuadrados { get; set; }
    public StatusImovel Status { get; set; }
    public decimal ValorAluguel { get; set; }
    public TipoImovel TipoDoImovel { get; set; }
    public EnderecoRequest Endereco { get; set; }

}
