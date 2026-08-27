

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
        if (aprovados)
            query = query.Where(x => x.Aprovado);
        if (!aprovados)
            query = query.Where(x => !x.Aprovado);

        var imoveis = await query
            .Include(i => i.Endereco)
            .Include(i => i.Contratos)
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

        if (avaliados)
            query = query.Where(x => x.Avaliado);
        if (!avaliados)
            query = query.Where(x => !x.Avaliado);

        return query.ToListAsync();
    }

    public Task<List<Imovel>> ListarComFiltroAsync(FiltroImovel filtro, CancellationToken cancellationToken = default)
    {
        var query = context.Imoveis
            .Include(x => x.Endereco)
            .Where(x => x.Status == StatusImovel.Disponivel && x.Aprovado)
            .AsNoTracking()
            .AsQueryable();


        if (filtro.Comodos is not null && filtro.Comodos > 0)
            query = query.Where(x => x.Comodos >= filtro.Comodos.Value);


        if (filtro.Banheiros is not null && filtro.Banheiros >= 0)
            query = query.Where(x => x.Banheiros >= filtro.Banheiros.Value);

        if (filtro.TipoImovel is not null)
        {
            if(Enum.TryParse<TipoImovel>(filtro.TipoImovel, ignoreCase: true, out TipoImovel resultado))
                query = query.Where(x => x.Tipo == resultado);
        }


        // Filtro de Área (m²)
        if (filtro.AreaM2 != null && filtro.AreaM2.Length >= 2)
        {
            var minArea = filtro.AreaM2[0];
            var maxArea = filtro.AreaM2[1];

            if (minArea > 0)
                query = query.Where(x => x.MetrosQuadrados >= minArea);

            if (maxArea > 0 && maxArea > minArea)
                query = query.Where(x => x.MetrosQuadrados <= maxArea);
        }

        if (filtro.FaixaPreco != null && filtro.FaixaPreco.Length >= 2)
        {
            var minPreco = filtro.FaixaPreco[0];
            var maxPreco = filtro.FaixaPreco[1];

            if (minPreco > 0)
                query = query.Where(x => x.ValorAluguel >= minPreco);

            // Remove a trava arbitrária de <= 8000
            if (maxPreco > 0 && maxPreco > minPreco)
                query = query.Where(x => x.ValorAluguel <= maxPreco);
        }

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


        return query.AsSplitQuery().ToListAsync();
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
