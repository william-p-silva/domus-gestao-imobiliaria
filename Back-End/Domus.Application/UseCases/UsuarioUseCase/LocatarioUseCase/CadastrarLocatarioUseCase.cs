using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;

public class CadastrarLocatarioUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher, IUnitOfWork commit, IFuncaoRepository funcaoRepository)
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IFuncaoRepository _funcaoRepository = funcaoRepository;
    private readonly IUnitOfWork _commit = commit;

    public async Task<UsuarioResponse> Execute(UsuarioRequest request, CancellationToken cancellationToken)
    {
        var userExiste = await _usuarioRepository.BuscarPorEmailAsync(request.Email, cancellationToken);
        if (userExiste != null)
            throw new ArgumentException("Usuário já esta cadastrado", nameof(request.Email));

        var newSenhaHash = _passwordHasher.GerarHash(request.Senha);

        Usuario usuario = new Usuario(nome: request.Nome, email: request.Email, senhaHash: newSenhaHash);



        Funcao funcao = await _funcaoRepository.BuscarPorNomeAsync(nome: "Locatario", cancellationToken);

        if (funcao == null)
            throw new ArgumentNullException("Funcao não encontrada", nameof(funcao));


        usuario.AddFuncaoUsuario(funcao);


        await _usuarioRepository.AddAsync(usuario, cancellationToken);

        await _commit.CommitAsync(cancellationToken);

        return new UsuarioResponse()
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Funcao_ID = funcao.Funcao_ID, //Transformar em Lista de IDs, um user pode ter varias funções
            Perfil = new List<string> {funcao.Nome.ToString()},
            UsuarioFuncao_ID = usuario.UsuarioFuncao.Select(x => x.UsuarioFuncao_ID).ToList(),
        };
    }
}
