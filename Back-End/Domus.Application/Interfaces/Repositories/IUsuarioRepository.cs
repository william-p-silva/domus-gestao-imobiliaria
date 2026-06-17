

using Domus.Domain.Entity;

namespace Domus.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Usuario?> BuscarPorTokenEmailAsync(string tokenEmail, CancellationToken cancellationToken = default);
    Task<List<Usuario>> ListarAsync(CancellationToken cancellationToken = default);
    void Remover(Usuario usuario);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default);


    //UsuarioFunção
    Task<UsuarioFuncao?> BuscarFuncaoUserPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddFuncaoUserAsync(UsuarioFuncao usuarioFuncao, CancellationToken cancellationToken = default);
}
