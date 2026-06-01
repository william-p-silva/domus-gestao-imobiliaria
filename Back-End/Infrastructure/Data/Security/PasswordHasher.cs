

using Domus.Application.Interfaces.Security;

namespace Domus.Infrastructure.Data.Security;

public class PasswordHasher : IPasswordHasher
{
    public string GerarHash(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentNullException("A senha não pode ser vazia", nameof(senha));

        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool VerificarSenha(string senha, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}
