

using Domus.Application.DTOs.Caracteristica.Response;
using Domus.Domain.Entity;

namespace Domus.Application.Mappers.CaracteristicaMapper;

public static class GrupoCaracteristicaMapper
{
    public static GrupoCaracteristicaResponse ToResponse(this GrupoCaracteristica grupo)
    {
        return new GrupoCaracteristicaResponse
        {
            GrupoCaracteristica_ID = grupo.GrupoCaracteristica_ID,
            Nome = grupo.Nome,
            Estado = grupo.Estado.ToString(),
            Codigo = grupo.Codigo.Value,
            CriadoEm = grupo.CriadoEm,
            DeletadoEm = grupo.DeletadoEm
        };
    }
}
