

using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.CaracteristicaMapper;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;
using Domus.Domain.ValueObjects.Caracteristica;

namespace Domus.Application.UseCases.CaracteristicaUseCase;

public class CadastrarGrupoCaracteristicasUseCase(
    IGrupoCaracteristicaRepository grupoCaracteristicaRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task<GrupoCaracteristicaResponse> ExecuteAsync(
        RequestCadastrarGrupoCaracteristica request, 
        CancellationToken cancellationToken)
    {
        var codigo = request.Codigo != null ? CodigoGrupoCaracteristica.Create(request.Codigo) : null;
        var result = await grupoCaracteristicaRepository.BuscarPorNomeOuCodigo(
            nome: request.Nome,
            codigoGrupo: codigo,
            estado: null, 
            cancellationToken: cancellationToken
            );

        if (result != null)
        {
            if (result.Estado != Domain.Enums.Caracteristica.EstadoCaracteristica.Ativa)
            {
                result.Ativar();

                await unitOfWork.CommitAsync(cancellationToken);

                return result.ToResponse();
            }
            else
            {
                throw new ValidationException("Grupo de característica já cadastrada.");
            }
        }
        else
        {
            var grupo = new GrupoCaracteristica(nome: request.Nome, 
                codigo: request.Codigo != null ? request.Codigo : request.Nome);

            await grupoCaracteristicaRepository.AddAsync(grupo, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);

            return grupo.ToResponse();
        }
    }
}


