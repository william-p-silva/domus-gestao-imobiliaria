

using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class GrupoCaracteristicaRepository(AppDbContext context) : IGrupoCaracteristicaRepository
{
    public async Task AddAsync(GrupoCaracteristica grupoCaracteristica, CancellationToken cancellationToken = default)
    {
        await context.GruposCaracteristica.AddAsync(grupoCaracteristica);
    }

    public async Task<GrupoCaracteristica?> BuscarPorId(Guid id, EstadoCaracteristica? estado = EstadoCaracteristica.Ativa, CancellationToken cancellationToken = default)
    {
        return await context.GruposCaracteristica
            .AsTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.GrupoCaracteristica_ID == id 
            && (!estado.HasValue || x.Estado == estado), cancellationToken);
    }

    public async Task<List<GrupoCaracteristica>> Listar(EstadoCaracteristica estado = EstadoCaracteristica.Ativa, CancellationToken cancellationToken = default)
    {
        return await context.GruposCaracteristica
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.Estado == estado)
            .ToListAsync();
    }
}
