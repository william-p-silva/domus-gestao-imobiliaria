

using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.CaracteristicaMapper;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.CaracteristicaUseCase;

public class CadastrarCaracteristicaUseCase(
    ICaracteristicaRepository caracteristicaRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<CaracteristicaResponse> ExecuteAsync(
        CadastrarCaracteristicaRequest request
        ,CancellationToken cancellationToken
        )
    {
        var result = await caracteristicaRepository.BuscarPorNome(request.Nome);

        if (result is not null)
        {
            if (result.Estado != Domain.Enums.Caracteristica.EstadoCaracteristica.Ativa)
                result.Ativar();
            else
                throw new BusinessRuleException("Característica já cadastrada.");
        }

        var caracteristica = new Caracteristica(request.Nome);

        await caracteristicaRepository.AddAsync(caracteristica, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return caracteristica.ToResponse();
    }
}
