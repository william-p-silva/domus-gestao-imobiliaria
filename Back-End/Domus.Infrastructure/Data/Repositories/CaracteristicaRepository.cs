
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class CaracteristicaRepository(AppDbContext context) : ICaracteristicaRepository
{
    public async Task AddAsync(Caracteristica caracteristica, CancellationToken cancellationToken = default)
    {
        await context.Caracteristicas.AddAsync(caracteristica, cancellationToken);
    }

    public async Task<Caracteristica?> BuscarPorNome(
        string nome, 
        CancellationToken cancellationToken = default
        )
    {
        return await context.Caracteristicas
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => 
            x.Nome.Nome.Trim().ToLower() == nome.Trim().ToLower(), cancellationToken);
    }

    public async Task<List<Caracteristica>> Listar(
        EstadoCaracteristica estado = EstadoCaracteristica.Ativa, 
        CancellationToken cancellationToken = default
        )
    {
        return await context.Caracteristicas
            .IgnoreQueryFilters()
            .Where(x => x.Estado == estado)
            .ToListAsync();
    }
}
