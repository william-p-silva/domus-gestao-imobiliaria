
using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;

namespace Domus.Application.Interfaces.Repositories;

public interface IGrupoCaracteristicaRepository
{
    Task AddAsync(GrupoCaracteristica grupoCaracteristica, CancellationToken cancellationToken = default);

    Task<GrupoCaracteristica?> BuscarPorId(
        Guid id,
        EstadoCaracteristica? estado = EstadoCaracteristica.Ativa,
        CancellationToken cancellationToken = default);

    Task<List<GrupoCaracteristica>> Listar(
        EstadoCaracteristica estado = EstadoCaracteristica.Ativa,
        CancellationToken cancellationToken = default);
}
