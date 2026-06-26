

using Domus.Application.DTOs.Contrato;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.ContratoUseCase.CicloDeVida;

// Fluxo: Locatário recusa a proposta, limpando o vínculo de inquilino e devolvendo o contrato para o status de Rascunho.
public class RejeitarMinutaContratoUseCase(
    IContratoRepository contratoRepository,
    IUsuarioRepository usuarioRepository,
    IUnitOfWork commit)
{
    public async Task<ContratoResponse> Execute(Guid contrato_ID, Guid usuario_ID, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuario_ID, cancellationToken);
        if (usuario == null)
            throw new ArgumentException("Usuário inválido ", nameof(usuario_ID));

        var contrato = await contratoRepository.BuscarPorIdAsync(contrato_ID, cancellationToken);
        if (contrato == null)
            throw new ArgumentException("Contrato inválido ", nameof(contrato_ID));

        var locador = await usuarioRepository.BuscarPorIdAsync(contrato.Locador_ID, cancellationToken);
        if(locador == null)
            throw new ArgumentException("Locador associado ao contrato não foi encontrado.", nameof(contrato_ID));
        contrato.CancelarPendenciaMinuta(usuario.Usuario_ID);

        await commit.CommitAsync(cancellationToken);

        
        return new ContratoResponse()
        {
            Contrato_ID = contrato.Contrato_ID,
            Imovel_ID = contrato.Imovel_ID,
            Locador = new ContratoLocadorResponse()
            {
                Locador_ID = locador.Usuario_ID,
                Email = locador.Email.ToString(),
                Nome = locador.Nome
            },
            Titulo = contrato.Titulo,
            Descricao = contrato.Descricao,
            Tipo = contrato.Tipo,
            UrlContrato = contrato.UrlContrato,
            CriadoEm = contrato.CriadoEm,
            Status = contrato.Status
        };

    }
}
