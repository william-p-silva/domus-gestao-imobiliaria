

using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly AppDbContext _context;

    public AvaliacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Avaliacao avaliacao, CancellationToken cancellationToken = default)
    {
        await _context.Avaliacoes.AddAsync(avaliacao, cancellationToken);
    }

    public async Task<Avaliacao?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var avaliacao = await _context.Avaliacoes.FindAsync(id, cancellationToken);
        return avaliacao;
    }

    public async Task<List<Avaliacao>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var avaliacoes = await _context.Avaliacoes.AsNoTracking().ToListAsync(cancellationToken);
        return avaliacoes;
    }

    public void Remover(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Remove(avaliacao);
    }
}
