

using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.CaracteristicaMapper;
using Domus.Domain.Enums.Caracteristica;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.CaracteristicaUseCase.Listar;

public class ListarCaracteristicasUseCase(
    ICaracteristicaRepository caracteristicaRepository
    )
{
    public async Task<List<CaracteristicaResponse>> ExecuteAsync(
        CancellationToken cancellationToken,
        EstadoCaracteristica estado = EstadoCaracteristica.Ativa
        )
    {
        if (!Enum.IsDefined(estado))
            throw new ValidationException("Estado de característica inválido.");

        var caracteristicas = await caracteristicaRepository.Listar(estado, cancellationToken);

        return caracteristicas.Select(x => x.ToResponse()).ToList();
    }
}
