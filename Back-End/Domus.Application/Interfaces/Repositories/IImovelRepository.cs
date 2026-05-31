using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IImovelRepository
{
    Task<Imovel?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Imovel>> ListarAsync(CancellationToken cancellationToken = default);
    void Remover(Imovel imovel);
    Task AddAsync(Imovel contrato, CancellationToken cancellationToken = default);
}