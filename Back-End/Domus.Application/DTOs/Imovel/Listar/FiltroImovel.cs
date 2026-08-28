
namespace Domus.Application.DTOs.Imovel.Listar;

public class FiltroImovel
{
    public int? Banheiros { get; set; }
    public int? Comodos { get; set; }
    public decimal? MinPreco { get; set; }
    public decimal? MaxPreco { get; set; }
    public decimal? MinArea { get; set; }
    public decimal? MaxArea { get; set; }
    public string? TipoImovel { get; set; }
    public FiltroEnderecoImovel? Endereco { get; set; }
}
