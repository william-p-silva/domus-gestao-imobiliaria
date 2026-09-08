
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class FuncaoRepository(AppDbContext context) : IFuncaoRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Funcao funcao, CancellationToken cancellationToken = default)
    {
        await _context.Funcoes.AddAsync(funcao, cancellationToken);
    }

    public async Task<Funcao> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var funcao = await _context.Funcoes.FindAsync(id, cancellationToken);
        return funcao;
    }

    public async Task<Funcao> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken = default)
    {
        var funcao = await _context.Funcoes.FirstOrDefaultAsync(f => f.Nome.ToString() == nome);
        return funcao;
    }
}
