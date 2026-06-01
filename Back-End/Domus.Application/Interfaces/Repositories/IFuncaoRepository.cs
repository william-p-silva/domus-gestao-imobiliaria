

using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IFuncaoRepository
{
    Task<Funcao> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken = default);
    Task<Funcao> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Funcao funcao, CancellationToken cancellationToken = default);
}
