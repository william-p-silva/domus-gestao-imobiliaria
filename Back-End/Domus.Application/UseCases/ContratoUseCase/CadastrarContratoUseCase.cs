

using Domus.Application.DTOs.Contrato;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Contrato;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Application.UseCases.ContratoUseCase;

public class CadastrarContratoUseCase(
    IContratoRepository contratoRepository, 
    IUsuarioRepository usuarioRepository, 
    IImovelRepository imovelRepository, 
    IUnitOfWork commit)
{

    /// <summary>
    /// Caso de Uso: Cria uma nova minuta/rascunho de contrato de locação associado a um locador e a um imóvel específico.
    /// </summary>
    /// <remarks>
    /// Valida se o imóvel está elegível para uma nova locação (não possuindo contratos duplicados e estando ativo) 
    /// e assegura que o criador da minuta possua o papel regulamentar de locador.
    /// </remarks>
    /// <param name="request">DTO contendo o título, descrição, tipo e link do documento do contrato.</param>
    /// <param name="locador_ID">Identificador exclusivo do locador que está propondo a minuta.</param>
    /// <param name="cancellationToken">Token de cancelamento para interrupção segura da transação assíncrona.</param>
    /// <returns>Um <see cref="ContratoResponse"/> representando a minuta criada com o status inicial em rascunho.</returns>
    /// <exception cref="ArgumentException">
    /// Lançada se o imóvel ou locador não existirem, se o imóvel já possuir vínculo contratual ativo, 
    /// se a propriedade estiver indisponível, ou se o usuário criador não for um locador válido.
    /// </exception>
    public async Task<ContratoResponse> Execute(ContratoRequest request, Guid locador_ID, CancellationToken cancellationToken)
    {
        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken)
            ?? throw new NotFoundException($"Imovel Inexistente {request.Imovel_ID}");        

        var user = await usuarioRepository.BuscarPorIdAsync(locador_ID, cancellationToken)
            ?? throw new NotFoundException($"Locador Inexistente {locador_ID}");

        var contrato = new Contrato(
            imovel: imovel
            ,locador: user
            ,titulo: request.Titulo
            ,descricao: request.Descricao
            ,urlContrato: request.UrlContrato
            ,tipo: request.Tipo
            );

        await contratoRepository.AddAsync(contrato, cancellationToken);
        await commit.CommitAsync(cancellationToken);

        return contrato.ToResponse();

    }
}
