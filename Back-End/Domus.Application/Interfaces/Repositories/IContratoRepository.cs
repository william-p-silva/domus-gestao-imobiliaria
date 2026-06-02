
using Domus.Domain.Entity;
using Domus.Domain.Enums;

namespace Domus.Application.Interfaces.Repositories;

public interface IContratoRepository
{
    Task<Contrato?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Contrato>> ListarAsync(CancellationToken cancellationToken = default);
    Task<List<Contrato>> ListarPorStatusAsync(StatusContrato status, CancellationToken cancellationToken = default);
    void Remover(Contrato contrato);
    Task AddAsync(Contrato contrato, CancellationToken cancellationToken = default);
}
