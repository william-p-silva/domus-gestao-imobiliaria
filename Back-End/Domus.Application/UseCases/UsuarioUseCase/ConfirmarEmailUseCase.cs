
using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.UsuarioUseCase;

public class ConfirmarEmailUseCase(
    IUsuarioRepository usuarioRepository,
    IUnitOfWork commit
    )
{
    public async Task<string> Execute(string token, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorTokenEmailAsync(token, cancellationToken);
        if (usuario == null)
            throw new ArgumentException("Usuário inesistente. ");
        if (usuario.TokenEmailExpire < DateTime.UtcNow)
            throw new ArgumentException("Token expirado. ");
        if (usuario.EmailConfirmado)
            throw new ArgumentException("Email já confirmado.");

        usuario.ConfirmarEmail();

        await commit.CommitAsync(cancellationToken);

        return "Email Confirmado, Faça Login na Domus";
    }
}
