using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Usuario
{
    public Guid Usuario_ID { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public bool Status { get; private set; }
    public string SenhaHash { get; private set; }
    public Funcao Funcao { get; private set; }
    public string Endereco_ID { get; private set; }

}
