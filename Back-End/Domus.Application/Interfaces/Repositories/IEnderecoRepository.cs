
using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IEnderecoRepository
{
    Task AddAsync(Endereco endereco, CancellationToken cancellationToken = default);
    Task<Endereco?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Endereco>> ListarAsync(CancellationToken cancellationToken = default);
    void Remover(Endereco endereco);
}
