
using Domus.Application.DTOs.Endereco;
using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Imovel;

public class ImovelResponse
{
    public Guid Imovel_ID { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Comodos { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal ValorAluguel { get; set; }
    public DateTime CriadoEm { get; set; }
    public bool Aprovado { get; set; }
    public bool Avaliado { get; set; }
    public decimal MetrosQuadrados { get; set; }
    public int Banheiros { get; set; }
    public string TipoDoImovel { get; set; } = string.Empty;
    public ResponseUsuarioImovel Locador { get; set; } = new ResponseUsuarioImovel();
    public EnderecoResponse Endereco { get; set; } = new EnderecoResponse();
    public ResponseContratoImovel? Contrato { get; set; } = new ResponseContratoImovel();
}