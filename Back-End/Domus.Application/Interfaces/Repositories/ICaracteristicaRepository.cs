using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;

namespace Domus.Application.Interfaces.Repositories;

public interface ICaracteristicaRepository
{
    Task AddAsync(
        Caracteristica caracteristica, 
        CancellationToken cancellationToken = default);
    Task<Caracteristica?> BuscarPorNome(
        string nome, 
        CancellationToken cancellationToken = default);
    Task<List<Caracteristica>> Listar(
        EstadoCaracteristica estado = EstadoCaracteristica.Ativa, 
        CancellationToken cancellationToken = default);

    Task<Caracteristica?> BuscarPorId(
        Guid id,
        EstadoCaracteristica? estado = EstadoCaracteristica.Ativa,
        CancellationToken cancellationToken = default
        );
}
