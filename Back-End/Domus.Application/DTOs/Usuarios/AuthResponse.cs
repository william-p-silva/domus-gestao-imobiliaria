

namespace Domus.Application.DTOs.Usuarios;

public sealed record AuthResponse
{
    public string Nome { get; set; }
    public string Usuario_ID { get; set; }
    public string Email { get; set; }
    public List<string> Perfil { get; set; }
}
