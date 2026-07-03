

using Domus.Application.DTOs.Usuarios;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.UsuarioUseCase;

public class AdicionarInfosExtrasUseCase(
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork
    )
{

    public async Task<string> Execute(RequestInfosExtras request, Guid usuario_id, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuario_id, cancellationToken);
        if ( usuario is null)  
            throw new ArgumentException("Usuário não encontrado.", nameof(usuario_id));

        if (request.CPF != null)
        {
            usuario.AdicionarCPF(request.CPF);
        }

        if (request.Celular != null)
        {
            usuario.AdicionarCelular(request.Celular);
        }

        await unitOfWork.CommitAsync(cancellationToken);
        
        return "Usuário atualizado com sucesso.";
    }
}
