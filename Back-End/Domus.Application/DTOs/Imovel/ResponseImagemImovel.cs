

namespace Domus.Application.DTOs.Imovel;

public sealed record ResponseImagemImovel
{
    public Guid ImagemImovel_ID { get; set; }
    public string UrlImagem { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
}