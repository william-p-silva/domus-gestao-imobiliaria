
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.Listar;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Imovel;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class ListarImoveisComFiltroUseCase(IImovelRepository imovelRepository)
{
    public async Task<List<ImovelResponse>> Execute(FiltroImovel filtro, CancellationToken cancellationToken)
    {
        if(filtro is null)
            throw new ArgumentNullException("O filtro não pode ser nulo. ",nameof(filtro));
        var imoveis = await imovelRepository.ListarComFiltroAsync(filtro, cancellationToken);

        return imoveis.Select(i => i.ToResponse()).ToList();
    }
}
