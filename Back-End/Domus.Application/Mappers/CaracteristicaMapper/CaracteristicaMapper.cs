
using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Domain.Entity;

namespace Domus.Application.Mappers.CaracteristicaMapper;

public static class CaracteristicaMapper
{
    public static CaracteristicaResponse ToResponse(this Caracteristica caracteristica)
    {
        return new CaracteristicaResponse
        {
            Caracteristica_ID = caracteristica.Caracteristica_ID,
            Nome = caracteristica.Nome.Nome,
            Estado = caracteristica.Estado.ToString(),
            CriadoEm = caracteristica.CriadoEm,
            DeletadoEm = caracteristica.DeletadoEm,
            ImoveisTotais = caracteristica.CaracteristicasImovel.Count,

            GrupoCaracteristica = caracteristica.GrupoCaracteristica.ToResponse()
        };
    }
}
