

using Domus.Application.DTOs.Imovel;

namespace Domus.Application.DTOs.Chat.Response;

public sealed record ResponseImovelChat
{
    public Guid Imovel_ID { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Comodos { get; set; }
    public int Banheiros { get; set; }
    public decimal MetrosQuadrados { get; set; }
    public decimal ValorAluguel { get; set; }
    public List<ResponseImagemImovel> Imagens { get; set; } = new List<ResponseImagemImovel>();
}
