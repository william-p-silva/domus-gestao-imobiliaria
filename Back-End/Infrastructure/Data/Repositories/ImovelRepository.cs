

using Domus.Application.DTOs.Imovel.Listar;
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
        var imovel = await context.Imoveis
            .Include(i => i.Endereco)
            .Include(i => i.Contratos)
            .FirstOrDefaultAsync(i => i.Imovel_ID == id, cancellationToken);
        return imovel;
    }

    public async Task<List<Imovel>> ListarAllImoveisAsync(CancellationToken cancellationToken = default)
    {
        return await context.Imoveis
            .Include(i => i.Endereco)
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<List<Imovel>> ListarAsync(bool aprovados = true, CancellationToken cancellationToken = default)
    {
        var query = context.Imoveis.AsNoTracking().AsQueryable();

        query = query.Where(x => x.Aprovado);

        var imoveis = await query
            .Include(i => i.Endereco)
            .Include(i => i.Contratos)
            .Where(x => x.Status == StatusImovel.Disponivel && x.Aprovado)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return imoveis;
    }

    public Task<List<Imovel>> ListarAvaliadosAsync(bool avaliados = true, CancellationToken cancellationToken = default)
    {
        var query = context.Imoveis
            .Include(x => x.Endereco)
            .AsNoTracking()
            .AsQueryable();

        query = query.Where(x => x.Avaliado);


        return query.ToListAsync();
    }

    public async Task<List<Imovel>> ListarComFiltroAsync(FiltroImovel filtro, CancellationToken cancellationToken = default)
    {
        var query = context.Imoveis
            .Include(x => x.Endereco)
            .Where(x => x.Status == StatusImovel.Disponivel && x.Aprovado)
            .AsNoTracking()
            .AsQueryable();


        if (filtro.Comodos is not null && filtro.Comodos > 0 && filtro.Comodos < 4)
            query = query.Where(x => x.Comodos >= filtro.Comodos.Value);


        if (filtro.Banheiros is not null && filtro.Banheiros >= 0 && filtro.Banheiros < 4)
            query = query.Where(x => x.Banheiros >= filtro.Banheiros.Value);

        if (!string.IsNullOrWhiteSpace(filtro.TipoImovel))
        {
            if (Enum.TryParse<TipoImovel>(filtro.TipoImovel, ignoreCase: true, out TipoImovel tipoEnum))
            {
                query = query.Where(x => x.Tipo == tipoEnum);
            }
        }


        // Filtro de Área (m²)
        if (filtro.MinArea is not null && filtro.MinArea > 0)
            query = query.Where(x => x.MetrosQuadrados >= filtro.MinArea.Value);
        if (filtro.MaxArea is not null && filtro.MaxArea > 0)
            query = query.Where(x => x.MetrosQuadrados <= filtro.MaxArea.Value);

        // Filtro de Preço
        if (filtro.MinPreco is not null && filtro.MinPreco > 0)
            query = query.Where(x => x.MetrosQuadrados >= filtro.MinPreco.Value);
        if (filtro.MaxPreco is not null && filtro.MaxPreco > 0)
            query = query.Where(x => x.MetrosQuadrados <= filtro.MaxPreco.Value);

        if (filtro.Endereco is not null)
        {
            if (!string.IsNullOrWhiteSpace(filtro.Endereco.CEP))
                query = query.Where(x => x.Endereco.CEP == filtro.Endereco.CEP);
            if (!string.IsNullOrWhiteSpace(filtro.Endereco.UF))
                query = query.Where(x => x.Endereco.UF == filtro.Endereco.UF);
            if (!string.IsNullOrWhiteSpace(filtro.Endereco.Cidade))
                query = query.Where(x => x.Endereco.Cidade.ToLower().Contains(filtro.Endereco.Cidade.ToLower()));
            if (!string.IsNullOrWhiteSpace(filtro.Endereco.Bairro))
                query = query.Where(x => x.Endereco.Bairro.ToLower().Contains(filtro.Endereco.Bairro.ToLower()));
        }


        return await query.ToListAsync();
    }

    public async Task<List<Imovel>> ListarImoveisLocador(Guid locadorId, CancellationToken cancellationToken = default)
    {
        var imovesLocador = await context.Imoveis
            .Where(i => i.Usuario_ID == locadorId)
            .Include(i => i.Endereco)
            .Include(i => i.Contratos)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return imovesLocador;
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
