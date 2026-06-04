

namespace Domus.Application.DTOs.Usuarios;

public class LoginResponse
{
    public Guid Usuario_ID { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<string> Perfil { get; set; }
    public string Token { get; set; }
}
