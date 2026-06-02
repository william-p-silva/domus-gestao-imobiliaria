

using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class ContratoRepository(AppDbContext context) : IContratoRepository
{

    public async Task AddAsync(Contrato contrato, CancellationToken cancellationToken = default)
    {
        await context.Contratos.AddAsync(contrato, cancellationToken);
    }

    public async Task<Contrato?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var contrato = await context.Contratos.FindAsync(id, cancellationToken);
        return contrato;
    }

    public Task<List<Contrato>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var contratos = context.Contratos.AsNoTracking().ToListAsync(cancellationToken);
        return contratos;
    }

    public Task<List<Contrato>> ListarPorStatusAsync(StatusContrato status, CancellationToken cancellationToken = default)
    {
        var contratos = context.Contratos.AsNoTracking().Where(x => x.Status == status).ToListAsync(cancellationToken);
        return contratos;
    }

    public void Remover(Contrato contrato)
    {
        context.Contratos.Remove(contrato);
    }
}
