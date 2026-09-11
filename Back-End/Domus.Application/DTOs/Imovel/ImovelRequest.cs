

using Domus.Application.DTOs.Endereco;
using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Imovel;

public sealed record ImovelRequest
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Comodos { get; set; }
    public int Banheiros { get; set; }
    public decimal MetrosQuadrados { get; set; }
    public StatusImovel Status { get; set; }
    public decimal ValorAluguel { get; set; }
    public TipoImovel TipoDoImovel { get; set; }
    public EnderecoRequest Endereco { get; set; } = new EnderecoRequest();
}
