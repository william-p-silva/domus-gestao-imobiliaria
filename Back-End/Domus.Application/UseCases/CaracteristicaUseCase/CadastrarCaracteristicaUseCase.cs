

using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.CaracteristicaMapper;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.CaracteristicaUseCase;

public class CadastrarCaracteristicaUseCase(
    ICaracteristicaRepository caracteristicaRepository,
    IGrupoCaracteristicaRepository grupoCaracteristica,
    IUnitOfWork unitOfWork
    )
{
    public async Task<CaracteristicaResponse> ExecuteAsync(
        CadastrarCaracteristicaRequest request
        , CancellationToken cancellationToken
        )
    {
        var result = await caracteristicaRepository.BuscarPorNome(request.Nome);

        if (result is not null)
        {
            if (result.Estado != Domain.Enums.Caracteristica.EstadoCaracteristica.Ativa)
            {
                result.Ativar();
                await unitOfWork.CommitAsync(cancellationToken);

                return result.ToResponse();
            }
            else
                throw new BusinessRuleException("Característica já cadastrada.");
        }

        var grupo = await grupoCaracteristica.BuscarPorId(
            request.GrupoCaracteristica_ID,
            Domain.Enums.Caracteristica.EstadoCaracteristica.Ativa,
            cancellationToken
            ) ?? throw new NotFoundException("Grupo de característica não encontrado.");

        var caracteristica = new Caracteristica(request.Nome, grupo);

        await caracteristicaRepository.AddAsync(caracteristica, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return caracteristica.ToResponse();
    }
}
