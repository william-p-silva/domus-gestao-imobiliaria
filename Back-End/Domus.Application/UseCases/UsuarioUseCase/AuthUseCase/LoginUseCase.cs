

using Domus.Application.DTOs.Usuarios;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;

namespace Domus.Application.UseCases.UsuarioUseCase.AuthUseCase;

public class LoginUseCase(
    IUsuarioRepository usuarioRepository, 
    ITokenService tokenService, 
    IPasswordHasher passwordHasher)
{


    public async Task<LoginResponse> Execute(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorEmailAsync(request.Email, cancellationToken);
        if (usuario == null)
            throw new ArgumentException("Usuário ou senha inválidos", nameof(request.Email));

        bool senhaValida = passwordHasher.VerificarSenha(request.Senha, usuario.SenhaHash);
        if (!senhaValida)
            throw new ArgumentException("Usuário ou senha inválidos", nameof(request.Senha));

        var token = tokenService.GenerateToken(usuario);
        return new LoginResponse
        {
            Token = token,
            Usuario_ID = usuario.Usuario_ID,
            Nome = usuario.Nome,
            Email = usuario.Email.ToString(),
            Perfil = usuario.UsuarioFuncao.Select(uf => uf.Funcao.Nome.ToString()).ToList()
        };
    }
}
