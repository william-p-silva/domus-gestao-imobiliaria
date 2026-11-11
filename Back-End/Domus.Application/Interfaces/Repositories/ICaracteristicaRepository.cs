using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface ICaracteristicaRepository
{
    Task AddAsync(Caracteristica caracteristica, CancellationToken cancellationToken = default);
    Task<Caracteristica?> BuscarPorNome(string nome, CancellationToken cancellationToken = default);
}
