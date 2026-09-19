
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Imovel;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class BuscarImovelPorIdUseCase(
    IImovelRepository imovelRepository,
    IUsuarioRepository usuarioRepository
    )
{
    public async Task<ImovelResponse> Execute(Guid ImovelId, CancellationToken cancellationToken)
    {
        var imovel = await imovelRepository.BuscarPorIdAsync(ImovelId, cancellationToken);
        if (imovel is null)
            throw new ArgumentException("Imovel não encontrado. ", nameof(ImovelId));

        return imovel.ToResponse();
    }
}
