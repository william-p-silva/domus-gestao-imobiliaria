using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IAvaliacaoRepository
{
    Task<Avaliacao?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Avaliacao>> ListarAsync(CancellationToken cancellationToken = default);
    void Remover(Avaliacao avaliacao);
    Task AddAsync(Avaliacao avaliacao, CancellationToken cancellationToken = default);
}
