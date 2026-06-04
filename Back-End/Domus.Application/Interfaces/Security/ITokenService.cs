using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Security;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}
