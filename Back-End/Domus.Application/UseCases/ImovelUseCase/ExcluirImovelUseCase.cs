

using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;
using Domus.Domain.Enums;

namespace Domus.Application.UseCases.ImovelUseCase;

public class ExcluirImovelUseCase(
    IImovelRepository imovelRepository, 
    IUsuarioRepository usuarioRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
{

    public async Task<string> Execute(RequestExcluirImovel request, Guid userId, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(userId, cancellationToken);
        if (usuario is null)
            throw new ArgumentException("Usuário não encontrado.", nameof(userId));
        if(!usuario.PossuiFuncao(FuncaoUser.Locador))
            throw new ArgumentException("Apenas locadores podem excluir imóveis.", nameof(userId));

        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if (imovel is null)
            throw new ArgumentException("Imóvel não encontrado.", nameof(request.Imovel_ID));
        if(imovel.Usuario_ID != usuario.Usuario_ID)
            throw new ArgumentException("Você não é o proprietário deste imóvel.", nameof(request.Imovel_ID));

        if(!passwordHasher.VerificarSenha(request.ConfirmarSenha, usuario.SenhaHash))
            throw new ArgumentException("Senha incorreta.", nameof(request.ConfirmarSenha));

        imovel.Excluir(); 
        await unitOfWork.CommitAsync(cancellationToken);

        return "Imóvel excluído com sucesso.";
    }
}
