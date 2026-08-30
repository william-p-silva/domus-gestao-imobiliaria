

namespace Domus.Application.DTOs.Imovel;

public sealed record ResponseUsuarioImovel
{
    public Guid Usuario_ID { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}
