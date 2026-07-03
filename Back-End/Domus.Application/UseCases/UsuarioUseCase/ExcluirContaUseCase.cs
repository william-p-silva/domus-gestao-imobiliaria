

using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.UsuarioUseCase;

public class ExcluirContaUseCase(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
{
    public async Task<bool> Execute(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuarioId, cancellationToken);
        if (usuario is null)
            throw new ArgumentException("Usuário não encontrado.", nameof(usuarioId));
        usuario.DesativarUsuario();
        await unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
