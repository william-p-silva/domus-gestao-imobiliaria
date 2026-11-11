
namespace Domus.Application.DTOs.Caracteristica.Request;

public sealed record CadastrarCaracteristicaRequest
{
    public string Nome { get; set; } = string.Empty;
}
