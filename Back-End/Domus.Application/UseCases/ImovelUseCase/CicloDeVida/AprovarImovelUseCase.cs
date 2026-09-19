
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.CicloDeVida;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Imovel;
using Domus.Domain.Enums;

namespace Domus.Application.UseCases.ImovelUseCase.CicloDeVida;

public class AprovarImovelUseCase(
    IUsuarioRepository usuarioRepository,
    IImovelRepository imovelRepository,
    IUnitOfWork commit
    )
{
    

    public async Task<ImovelResponse> Execute(
        RequestAprovarImovel request, Guid admId, 
        CancellationToken cancellationToken)
    {
        var admin = await usuarioRepository.BuscarPorIdAsync(admId, cancellationToken);
        if (admin == null || !admin.PossuiFuncao(FuncaoUser.Administrador))
            throw new ArgumentException("Usuario inválido ", nameof(admId));

        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if (imovel == null)
            throw new ArgumentException("Imovel inválido ", nameof(request.Imovel_ID));

        imovel.Avaliar(request.Aprovado);

        await commit.CommitAsync(cancellationToken);

        return imovel.ToResponse();
    }
}
