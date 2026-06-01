

using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<Usuario>> ListarAsync(CancellationToken cancellationToken = default);
    void Remover(Usuario usuario);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default);
}
