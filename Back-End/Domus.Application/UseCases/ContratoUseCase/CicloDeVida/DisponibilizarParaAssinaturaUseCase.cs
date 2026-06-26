

using Domus.Application.DTOs.Contrato;
using Domus.Application.DTOs.Contrato.CicloDeVida;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.ContratoUseCase.CicloDeVida;

// Fluxo: Locador assina a minuta do contrato e indica o possível locatário, alterando o status para Pendente.
public class DisponibilizarParaAssinaturaUseCase(
    IContratoRepository contratoRepository,
    IUsuarioRepository usuarioRepository,
    IUnitOfWork commit 
    )
{

    /// <summary>
    /// Executa o fluxo de negócio para o locador assinar a minuta e disponibilizá-la formalmente para o locatário indicado.
    /// </summary>
    /// <remarks>
    /// O método valida a existência e os perfis (<see cref="FuncaoUser"/>) do locador e do locatário, 
    /// invoca a transição de estado na entidade <see cref="Contrato"/> e persiste as alterações de forma atômica.
    /// </remarks>
    /// <param name="request">DTO contendo o identificador do contrato e o ID do candidato a locatário.</param>
    /// <param name="locador_id">O identificador exclusivo do locador autenticado que está realizando a operação.</param>
    /// <param name="cancellationToken">Token de cancelamento para interrupção resiliente da requisição assíncrona.</param>
    /// <returns>Um objeto <see cref="ResponseMinutaContrato"/> contendo o estado atualizado do contrato e os dados resumidos dos envolvidos.</returns>
    /// <exception cref="ArgumentException">
    /// Lançada se o contrato, locatário ou locador não forem encontrados, ou se os usuários não possuírem as funções requeridas para a operação.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Lançada se a entidade <see cref="Contrato"/> violar suas invariantes de negócio internas (ex: contrato já assinado ou fora de rascunho).
    /// </exception>
    public async Task<ResponseMinutaContrato> Execute(
        RequestDisponibilizarAssinatura request,
        Guid locador_id, CancellationToken cancellationToken)
    {
        var contrato = await contratoRepository.BuscarPorIdAsync(request.Contrato_ID, cancellationToken);
        if (contrato == null)
            throw new ArgumentException("Contrato invalido ", nameof(request.Contrato_ID));

        var locatario = await usuarioRepository.BuscarPorIdAsync(request.Locatario_ID, cancellationToken);
        if (locatario == null || !locatario.PossuiFuncao(Domain.Enums.FuncaoUser.Locatario))
            throw new ArgumentException("Locatario invalido", nameof(request.Locatario_ID));

        var locador = await usuarioRepository.BuscarPorIdAsync(locador_id, cancellationToken);
        if (locador == null || !locador.PossuiFuncao(Domus.Domain.Enums.FuncaoUser.Locador))
            throw new ArgumentException("Locador invalido", nameof(locador_id));


        contrato.LocadorDisponibilizaAssinaturaMinuta(request.Locatario_ID);

        await commit.CommitAsync(cancellationToken);
         
        return new ResponseMinutaContrato()
        {
            Contrato_ID = contrato.Contrato_ID,
            Imovel_ID = contrato.Imovel_ID,
            Locador = new ContratoLocadorResponse()
            {
                Locador_ID = locador.Usuario_ID,
                Email = locador.Email.ToString(),
                Nome = locador.Nome
            },
            Locatario = new ContratoLocatarioResponse()
            {
                Locatario_ID = locatario.Usuario_ID,
                Email = locatario.Email.ToString(),
                Nome = locatario.Nome
            },
            CriadoEm = contrato.CriadoEm,
            Descricao = contrato.Descricao,
            Status = contrato.Status,
            Tipo = contrato.Tipo,
            Titulo = contrato.Titulo,
            UrlContrato = contrato.UrlContrato,
            AssinaturaLocador = contrato.AssinaturaLocador,
            AssinaturaLocatario = contrato.AssinaturaLocatario
        };
    }
}
