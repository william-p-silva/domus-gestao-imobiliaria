

using Domus.Application.DTOs.Endereco;

namespace Domus.Application.DTOs.Usuarios.Perfil;

public class PerfilUsuarioResponse
{
    public Guid Usuario_Id { get; set; }
    public EnderecoResponse? Endereco { get; set; }
    public DateTime CriadoEm { get; set; }
    public string? CPFMascarado { get; set; }
    public string? Celular { get; set; }
    public List<string> Funcao { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
}
