
using Domus.Application.Interfaces.Security;
using Domus.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domus.Infrastructure.Data.Security;

public class TokenService(IConfiguration _configuration) : ITokenService
{

    public string GenerateToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("A chave secreta do JWT (Jwt:Key) não foi configurada no appsettings.json.")));

        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, usuario.Usuario_ID.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email.ToString()),

        };

        if(usuario.UsuarioFuncao != null && usuario.UsuarioFuncao.Any())
        {
            foreach (var uf in usuario.UsuarioFuncao)
            {
                if (uf.Funcao != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, uf.Funcao.Nome.ToString()));
                }
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = credenciais,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
