
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.Listar;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class ListarImoveisComFiltroUseCase
{
    public async Task<List<ImovelResponse>> Execute(FiltroImovel filtro, CancellationToken cancellationToken)
    {
        if(filtro is null)
            throw new ArgumentNullException("O filtro não pode ser nulo. ",nameof(filtro));
    }
}
