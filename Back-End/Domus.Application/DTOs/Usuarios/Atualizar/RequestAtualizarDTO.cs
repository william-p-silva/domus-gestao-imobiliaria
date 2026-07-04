
namespace Domus.Application.DTOs.Usuarios.Atualizar;

public class RequestAtualizarDTO
{
    public string? Celular { get; set; }
    public string AtualSenha { get; set; }
    public string? NovaSenha { get; set; }
    public string? Nome { get; set; }
}