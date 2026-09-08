
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class EnderecoRepository(AppDbContext context) : IEnderecoRepository
{
    public async Task AddAsync(Endereco endereco, CancellationToken cancellationToken = default)
    {
        await context.Enderecos.AddAsync(endereco, cancellationToken);
    }

    public async Task<Endereco?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var endereco = await context.Enderecos.FindAsync(id, cancellationToken);
        return endereco;
    }

    public async Task<List<Endereco>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var enderecos = await context.Enderecos.ToListAsync(cancellationToken);
        return enderecos;
    }

    public void Remover(Endereco endereco)
    {
        context.Enderecos.Remove(endereco);
    }
}
