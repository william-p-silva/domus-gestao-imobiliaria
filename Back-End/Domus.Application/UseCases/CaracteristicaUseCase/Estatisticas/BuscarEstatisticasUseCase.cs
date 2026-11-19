

using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.CaracteristicaUseCase.Estatisticas;

public class BuscarEstatisticasUseCase(
    ICaracteristicaRepository caracteristicaRepository
    )
{
    public async Task<ResponseEstatisticasCaracteristicas> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await caracteristicaRepository.BuscarEstatisticas(cancellationToken);
    }
}
