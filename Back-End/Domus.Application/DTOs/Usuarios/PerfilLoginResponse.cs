

namespace Domus.Application.DTOs.Usuarios;

public sealed record PerfilLoginResponse
{
    public string Nome { get; set; }
    public List<string> Perfil { get; set; }
    public string Email { get; set; }
}
