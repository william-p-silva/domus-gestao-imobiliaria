

using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Application.Interfaces.Email;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;
using Domus.Domain.Entity;
using System.Runtime.CompilerServices;

namespace Domus.Application.UseCases.UsuarioUseCase.LocadorUseCase;

public class CadastrarLocadorUseCase(
    IUsuarioRepository usuarioRepository, 
    IPasswordHasher passwordHasher, 
    IFuncaoRepository funcaoRepository, 
    IUnitOfWork commit,
    IEmailService emailService
    )
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IFuncaoRepository _funcaoRepository = funcaoRepository;
    private readonly IUnitOfWork _commit = commit;
    private readonly IEmailService _emailService = emailService;

    public async Task<string> Execute(UsuarioRequest request, 
        CancellationToken cancellationToken)
    {
        var userExiste = await _usuarioRepository.BuscarPorEmailAsync(request.Email, cancellationToken);
        if (userExiste != null)
            throw new ArgumentException("Usuário já esta cadastrado", nameof(request.Email));

        var newSenhaHash = _passwordHasher.GerarHash(request.Senha);

        var usuario = new Usuario(
            nome: request.Nome, 
            emailAConfirmar: request.Email, 
            senhaHash: newSenhaHash);

        Funcao funcao = await _funcaoRepository.BuscarPorNomeAsync(nome: "Locador", cancellationToken);

        if (funcao == null)
            throw new ArgumentNullException("Funcao não encontrada", nameof(funcao));
        
        usuario.AddFuncaoUsuario(funcao);

        await _usuarioRepository.AddAsync(usuario, cancellationToken);
        await _commit.CommitAsync(cancellationToken);

        string linkConfirmaEmail = $"http://localhost:5038/domus/confirmar/{usuario.TokenConfirmaEmail}";

        await _emailService.EnviarAsync(
            destinatario: usuario.EmailAConfirmar.Endereco,
            assunto: "Bem-vindo à Domus!",
            corpo: $"""
                <h2>Olá, {usuario.Nome}!</h2>
                <p>Seu cadastro foi realizado com sucesso na plataforma <strong>Domus Gestão Imobiliária</strong>.</p>
                <p>Agora você só precisa acessar o link para ativar a sua conta {linkConfirmaEmail}</p>
                <br/>
                <p>Atenciosamente,<br/>Equipe Domus</p>
                """
        );

        return linkConfirmaEmail;

    }
}
