using Domus.Domain.Entity;
using Domus.Domain.Enums;

namespace Domus.Application.Interfaces.Repositories;

public interface IImovelRepository
{
    Task<Imovel?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Imovel>> ListarAsync(bool aprovados = true ,CancellationToken cancellationToken = default);
    Task<List<Imovel>> ListarAllImoveisAsync(CancellationToken cancellationToken = default);
    Task<List<Imovel>> ListarImoveisLocador(Guid locadorId, CancellationToken cancellationToken = default);
    Task<List<Imovel>> ListarPorStatusAsync(StatusImovel status, CancellationToken cancellationToken = default);
    void Remover(Imovel imovel);
    Task AddAsync(Imovel imovel, CancellationToken cancellationToken = default);
}