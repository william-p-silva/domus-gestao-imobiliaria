

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


        bool houveAlteracao = false;
        if (!string.IsNullOrWhiteSpace(request.NovaSenha))
        {
            var novaSenhaHash = passwordHasher.GerarHash(request.NovaSenha);
            usuario.AlterarSenha(novaSenhaHash);
            houveAlteracao = true;
        }
        if (!string.IsNullOrWhiteSpace(request.Nome))
        {
            usuario.AlterarNome(request.Nome);
            houveAlteracao = true;
        }
        if (!string.IsNullOrWhiteSpace(request.Celular))
        {
            var celularExistente = await usuarioRepository.BuscarPorCelular(request.Celular, cancellationToken);
            if (celularExistente != null && celularExistente.Usuario_ID != usuario_id)
                throw new ArgumentException("O celular informado já está em uso por outro usuário.");
            usuario.AlterarCelular(request.Celular);
            houveAlteracao = true;
        }

        if(houveAlteracao)
        {
            await unitOfWork.CommitAsync(cancellationToken);
            return "Usuário atualizado com sucesso.";
        }
        else
        {
            return "Nenhuma alteração foi realizada.";
        }

    }
}
