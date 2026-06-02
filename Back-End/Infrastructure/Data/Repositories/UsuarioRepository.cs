
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Domus.Infrastructure.Data.Repositories;

public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(usuario, cancellationToken);
    }

    public async Task AddFuncaoUserAsync(UsuarioFuncao usuarioFuncao, CancellationToken cancellationToken = default)
    {
        await _context.UsuarioFuncoes.AddAsync(usuarioFuncao, cancellationToken);
    }

    public async Task<UsuarioFuncao?> BuscarFuncaoUserPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var usuarioFuncao = await _context.UsuarioFuncoes.FindAsync(id, cancellationToken);
        return usuarioFuncao;
    }

    public async Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        return usuario;
    }

    public async Task<Usuario?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var usuario = await _context.Usuarios.Include(u => u.UsuarioFuncao).ThenInclude(f => f.Funcao)
            .FirstOrDefaultAsync(u => u.Usuario_ID == id, cancellationToken); 

        return usuario;
    }

    public async Task<List<Usuario>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var usuarios = await _context.Usuarios.AsNoTracking().ToListAsync(cancellationToken);
        return usuarios;
    }

    public void Remover(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
    }
}
