

using Domus.Application.DTOs.Contrato;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.ContratoUseCase.Listar;

public class BuscarContratoUseCase(IContratoRepository contratoRepository)
{

    public async Task<ContratoResponse> Execute(Guid contrato_id, CancellationToken cancellationToken)
    {
        var contrato = await contratoRepository.BuscarPorIdAsync(contrato_id, cancellationToken);
        if (contrato is null)
            throw new Exception("Contrato não encontrado");
        return new ContratoResponse
        {
            Contrato_ID = contrato.Contrato_ID,
            CriadoEm = contrato.CriadoEm,
            Descricao = contrato.Descricao,
            Imovel_ID = contrato.Imovel_ID,
            Locador = new ContratoLocadorResponse
            {
                Locador_ID = contrato.Locador.Usuario_ID,
                Email = contrato.Locador.Email.Endereco,
                Nome = contrato.Locador.Nome.NomeCompleto,
            },
            Status = contrato.Status,
            Tipo = contrato.Tipo,
            Titulo = contrato.Titulo,
            UrlContrato = contrato.UrlContrato,
        };
    }
}
