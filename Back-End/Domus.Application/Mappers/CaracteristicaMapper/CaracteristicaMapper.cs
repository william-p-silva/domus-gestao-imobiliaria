
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
            CriadoEm = caracteristica.CriadoEm,
            DeletadoEm = caracteristica.DeletadoEm,
            Estado = caracteristica.Estado.ToString(),
            Nome = caracteristica.Nome.Nome
        };
    }
}
