namespace Domus.Application.DTOs.Usuarios.LocatarioDTOs;

public class UsuarioResponse
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<Guid> Funcao_ID { get; set; }
    public List<Guid> UsuarioFuncao_ID { get; set; }
    public List<string> Perfil { get; set; }

}
