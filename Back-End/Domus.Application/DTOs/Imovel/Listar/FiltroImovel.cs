
namespace Domus.Application.DTOs.Imovel.Listar;

public class FiltroImovel
{
    public int? Banheiros { get; set; }
    public int? Comodos { get; set; }
    public decimal[]? FaixaPreco { get; set; } = new decimal[2];
    public decimal[]? AreaM2 { get; set; } = new decimal[2];
    public FiltroEnderecoImovel? Endereco { get; set; }
    public string? TipoImovel { get; set; }
}