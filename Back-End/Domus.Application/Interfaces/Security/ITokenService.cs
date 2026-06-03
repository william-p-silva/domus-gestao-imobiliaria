using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Security;

public interface ITokenService
{
    Task<string> GenereteToken(Usuario usuario);
}
