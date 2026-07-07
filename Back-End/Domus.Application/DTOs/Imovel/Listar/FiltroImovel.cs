using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Imovel.Listar;

public class FiltroImovel
{
    public FiltroEnderecoImovel? Endereco { get; set; }
    public decimal? ValorAluguel { get; set; }
    public int? Banheiros { get; set; }
    public int? Comodos { get; set; }
    public decimal? MetrosQuadrados { get; set; }
    public string? Titulo { get; set; }
    public TipoImovel? Tipo { get; set; }
}