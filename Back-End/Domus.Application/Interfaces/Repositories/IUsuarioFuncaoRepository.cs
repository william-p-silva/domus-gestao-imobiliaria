

using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IUsuarioFuncaoRepository
{
    Task<UsuarioFuncao?> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken = default);
    Task<UsuarioFuncao?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UsuarioFuncao> AddAsync(UsuarioFuncao usuarioFuncao, CancellationToken cancellationToken = default);
}
