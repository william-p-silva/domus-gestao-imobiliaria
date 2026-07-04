

using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Usuarios.Atualizar;
using Domus.Application.DTOs.Usuarios.Perfil;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;

namespace Domus.Application.UseCases.UsuarioUseCase.Atualizar;

public class AlterarInfosUseCase(
    IUsuarioRepository usuarioRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork
    )
{

    public async Task<string> Execute(RequestAtualizarDTO request, Guid usuario_id, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuario_id, cancellationToken);
        if (usuario is null)
            throw new ArgumentException("Usuário não encontrado.");

        if (!passwordHasher.VerificarSenha(request.AtualSenha, usuario.SenhaHash))
            throw new ArgumentException("Senha atual incorreta.");

        if (!string.IsNullOrWhiteSpace(request.NovaSenha))
        {
            var novaSenhaHash = passwordHasher.GerarHash(request.NovaSenha);
            usuario.AlterarSenha(novaSenhaHash);
        }
        if (!string.IsNullOrWhiteSpace(request.Nome))
        {
            usuario.AlterarNome(request.Nome);
        }
        if (!string.IsNullOrWhiteSpace(request.Celular))
        {
            usuario.AlterarCelular(request.Celular);
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return "Usuário atualizado com sucesso.";
    }
}
