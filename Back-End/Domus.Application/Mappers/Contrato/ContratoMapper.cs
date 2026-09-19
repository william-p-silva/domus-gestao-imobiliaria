
using Domus.Application.DTOs.Contrato;

namespace Domus.Application.Mappers.Contrato;

public static class ContratoMapper
{
    public static ContratoResponse ToResponse(this Domain.Entity.Contrato contrato)
    {
        return new ContratoResponse
        {
            Contrato_ID = contrato.Contrato_ID,
            Imovel_ID = contrato.Imovel_ID,
            Titulo = contrato.Titulo,
            Descricao = contrato.Descricao,
            Tipo = contrato.Tipo,
            UrlContrato = contrato.UrlContrato,
            CriadoEm = contrato.CriadoEm,
            Status = contrato.Status,
            Locador = new ContratoLocadorResponse
            {
                Email = contrato.Locador.Email.Endereco,
                Locador_ID = contrato.Locador_ID,
                Nome = contrato.Locador.Nome.NomeCompleto
            }
        };
    }
}
