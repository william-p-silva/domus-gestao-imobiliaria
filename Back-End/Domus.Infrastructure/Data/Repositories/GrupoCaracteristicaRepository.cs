

using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;
using Domus.Domain.ValueObjects.Caracteristica;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class GrupoCaracteristicaRepository(AppDbContext context) : IGrupoCaracteristicaRepository
{
    public async Task AddAsync(GrupoCaracteristica grupoCaracteristica, CancellationToken cancellationToken = default)
    {
        await context.GruposCaracteristica.AddAsync(grupoCaracteristica);
    }

    public async Task<GrupoCaracteristica?> BuscarPorId(
        Guid id, EstadoCaracteristica? estado = EstadoCaracteristica.Ativa, 
        CancellationToken cancellationToken = default)
    {
        return await context.GruposCaracteristica
            .AsTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.GrupoCaracteristica_ID == id
            && (!estado.HasValue || x.Estado == estado), cancellationToken);
    }

    public async Task<GrupoCaracteristica?> BuscarPorNomeOuCodigo(
        string? nome,
        CodigoGrupoCaracteristica? codigoGrupo,
        EstadoCaracteristica? estado = EstadoCaracteristica.Ativa,
        CancellationToken cancellationToken = default)
    {
        var possuiNome = !string.IsNullOrWhiteSpace(nome);
        var possuiCodigo = codigoGrupo is not null;

        if (!possuiNome && !possuiCodigo)
            return null;

        var query = context.GruposCaracteristica
            .IgnoreQueryFilters()
            .AsTracking();

        if (estado.HasValue)
            query = query.Where(x => x.Estado == estado.Value);

        var nomeNormalizado = nome?.Trim();

        if (possuiNome && possuiCodigo)
        {
            query = query.Where(x =>
                x.Nome == nomeNormalizado ||
                x.Codigo == codigoGrupo
            );
        }
        else if (possuiNome)
        {
            query = query.Where(x => x.Nome == nomeNormalizado);
        }
        else
        {
            query = query.Where(x => x.Codigo == codigoGrupo);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
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
