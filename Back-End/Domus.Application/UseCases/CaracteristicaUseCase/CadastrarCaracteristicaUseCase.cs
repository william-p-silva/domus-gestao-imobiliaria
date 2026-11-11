

using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.CaracteristicaUseCase;

public class CadastrarCaracteristicaUseCase(
    ICaracteristicaRepository caracteristicaRepository
    )
{
    public async Task ExecuteAsync(CadastrarCaracteristicaRequest request)
    {

    }
}
