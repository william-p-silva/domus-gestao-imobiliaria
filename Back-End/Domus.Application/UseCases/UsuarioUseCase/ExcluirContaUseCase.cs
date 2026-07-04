

using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;

namespace Domus.Application.UseCases.UsuarioUseCase;

public class ExcluirContaUseCase(
    IUsuarioRepository usuarioRepository, 
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
{
    public async Task<bool> Execute(Guid usuarioId,string confirmarSenha ,CancellationToken cancellationToken = default)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuarioId, cancellationToken);
        if (usuario is null)
            throw new ArgumentException("Usuário não encontrado.", nameof(usuarioId));

        if(!passwordHasher.VerificarSenha(usuario.SenhaHash, confirmarSenha))
            throw new ArgumentException("Senha incorreta.", nameof(confirmarSenha));

        usuario.DesativarUsuario();
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
