
using Domus.Application.Interfaces.Security;
using Domus.Domain.Entity;

namespace Domus.Infrastructure.Data.Security;

public class TokenService : ITokenService
{
    public Task<string> GenereteToken(Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
