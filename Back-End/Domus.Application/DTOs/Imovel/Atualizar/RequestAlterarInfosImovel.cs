

using Domus.Domain.Enums;

namespace Domus.Application.DTOs.Imovel.Atualizar;

public class RequestAlterarInfosImovel
{
    public Guid Imovel_ID { get; set; }
    public decimal? ValorAluguel { get; set; }
    public int? Banheiros { get; set; }
    public int? Comodos { get; set; }
    public decimal? MetrosQuadrados { get; set; }
    public TipoImovel? Tipo { get; set; }
    public string? Descricao { get; set; }
    public string? Titulo { get; set; }
    public string ConfirmaSenha { get; set; }
}
