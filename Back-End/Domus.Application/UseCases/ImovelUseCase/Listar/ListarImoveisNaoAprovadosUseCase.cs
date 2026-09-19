

using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Imovel;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class ListarImoveisNaoAprovadosUseCase(
    IImovelRepository imovelRepository)
{

    public async Task<List<ImovelResponse>> Execute(CancellationToken cancellationToken)
    {
        var imoveis = await imovelRepository.ListarAsync(aprovados: false, cancellationToken);

        return imoveis.Select(i => i.ToResponse()).ToList();
    }
}
