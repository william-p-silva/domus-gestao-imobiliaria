
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.CaracteristicaMapper;
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

    public async Task<ResponseEstatisticasCaracteristicas> BuscarEstatisticas(
        CancellationToken cancellationToken = default)
    {
        var totalCaracteristicas = await context.Caracteristicas
       .CountAsync(cancellationToken);

        var ativas = await context.Caracteristicas
            .CountAsync(
                c => c.Estado == EstadoCaracteristica.Ativa,
                cancellationToken
            );

        var inativas = await context.Caracteristicas
            .CountAsync(
                c => c.Estado == EstadoCaracteristica.Inativa,
                cancellationToken
            );

        var totalImoveis = await context.Imoveis
            .Where(x => x.Status != Domain.Enums.StatusImovel.Excluido)
            .CountAsync(x => x.CaracteristicasImovel.Count > 0, cancellationToken);

        var top = await context.Caracteristicas
            .Include(x => x.GrupoCaracteristica)
            .Include(x => x.CaracteristicasImovel)
            .Select(c => new
            {
                Caracteristica = c,
                TotalUtilizacoes = c.CaracteristicasImovel.Count()
            })
            .OrderByDescending(c => c.TotalUtilizacoes)
            .Select(c => c.Caracteristica.ToResponse())
            .FirstOrDefaultAsync(cancellationToken);

        return new ResponseEstatisticasCaracteristicas
        {
            Ativas = ativas,
            Inativas = inativas,
            PorcentagemAtivas = totalCaracteristicas == 0
                ? 0
                : Math.Round((decimal)ativas / totalCaracteristicas * 100, 2),
            TotalImoveis = totalImoveis,
            Top = top ?? new CaracteristicaResponse()
        };
    }

    public async Task<Caracteristica?> BuscarPorId(
        Guid id,
        EstadoCaracteristica? estado = EstadoCaracteristica.Ativa,
        CancellationToken cancellationToken = default)
    {
        return await context.Caracteristicas
            .AsTracking()
            .IgnoreQueryFilters()
            .Include(x => x.CaracteristicasImovel)
            .FirstOrDefaultAsync(x => x.Caracteristica_ID == id
                && (!estado.HasValue || x.Estado == estado), cancellationToken);
    }

    public async Task<Caracteristica?> BuscarPorNome(
        string nome,
        CancellationToken cancellationToken = default
        )
    {
        return await context.Caracteristicas
            .AsTracking()
            .IgnoreQueryFilters()
            .Include(x => x.CaracteristicasImovel)
            .FirstOrDefaultAsync(x =>
            x.Nome.Nome.Trim().ToLower() == nome.Trim().ToLower(), cancellationToken);
    }

    public async Task<List<Caracteristica>> Listar(
        EstadoCaracteristica estado = EstadoCaracteristica.Ativa,
        CancellationToken cancellationToken = default
        )
    {
        return await context.Caracteristicas
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Include(x => x.GrupoCaracteristica)
            .Include(x => x.CaracteristicasImovel.Where(x => x.DeletadoEm == null))
            .Where(x => x.Estado == estado)
            .ToListAsync();
    }
}
