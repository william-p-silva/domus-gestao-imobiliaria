

namespace Domus.Application.Interfaces.Security;

public interface IPasswordHasher
{
    string GerarHash(string senha);
    bool VerificarSenha(string senha, string hash);
}
