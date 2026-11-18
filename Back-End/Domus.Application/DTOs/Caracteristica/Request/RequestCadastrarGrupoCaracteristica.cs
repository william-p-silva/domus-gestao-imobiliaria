

namespace Domus.Application.DTOs.Caracteristica.Request;

public sealed record RequestCadastrarGrupoCaracteristica
{
    public string Nome { get; set; } = string.Empty;
    public string? Codigo { get; set; } = string.Empty;
}
