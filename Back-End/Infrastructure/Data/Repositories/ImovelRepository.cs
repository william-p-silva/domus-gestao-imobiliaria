

using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class ImovelRepository(AppDbContext context) : IImovelRepository
{

    public async Task AddAsync(Imovel imovel, CancellationToken cancellationToken = default)
    {
        await context.Imoveis.AddAsync(imovel, cancellationToken);
    }

    public async Task<Imovel?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var imovel = await context.Imoveis.FindAsync(id, cancellationToken);
        return imovel;
    }

    public Task<List<Imovel>> ListarAsync(bool aprovados = true, CancellationToken cancellationToken = default)
    {
        var query = context.Imoveis.AsNoTracking().AsQueryable();
        if (aprovados)
            query = query.Where(x => x.Aprovado);
        if (!aprovados)
            query = query.Where(x => !x.Aprovado);

        var imoveis = query.ToListAsync(cancellationToken);
        return imoveis;
    }

    public async Task<List<Imovel>> ListarPorStatusAsync(StatusImovel status, CancellationToken cancellationToken = default)
    {
        var imoveis = await context.Imoveis.Where(i => i.Status == status).AsNoTracking().ToListAsync(cancellationToken);
        return imoveis;
    }

    public void Remover(Imovel imovel)
    {
        context.Imoveis.Remove(imovel);
    }
}
