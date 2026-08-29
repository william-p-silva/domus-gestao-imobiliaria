

using Domus.Application.DTOs.Contrato;
using Domus.Application.DTOs.Contrato.CicloDeVida;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Enums;

namespace Domus.Application.UseCases.ContratoUseCase.CicloDeVida;

// Fluxo: Locatário aceita os termos, insere sua assinatura e o contrato torna-se Ativo iniciando a vigência.
public class AssinarContratoUseCase(
    IContratoRepository contratoRepository,
    IUsuarioRepository usuarioRepository,
    IUnitOfWork commit,
    IImovelRepository imovelRepository)
{
    public async Task<ResponseMinutaContrato> Execute(
        RequestAssinarContrato request, 
        Guid locatario_id, CancellationToken cancellationToken)
    {
        var contrato = await contratoRepository.BuscarPorIdAsync(request.Contrato_ID, cancellationToken);
        if (contrato == null)
            throw new ArgumentException("Contrato invalido ", nameof(request.Contrato_ID));

        var locatario = await usuarioRepository.BuscarPorIdAsync(locatario_id, cancellationToken);
        if (locatario == null || contrato.Locatario_ID != locatario_id)
            throw new ArgumentException("Locatario invalido", nameof(locatario_id));

        if (!locatario.PossuiFuncao(Domain.Enums.FuncaoUser.Locatario))
            throw new ArgumentException("Usuário invalido ");

        var locador = await usuarioRepository.BuscarPorIdAsync(contrato.Locador_ID, cancellationToken);
        if(locador == null)
            throw new ArgumentException("Locador invalido");

        var imovel = await imovelRepository.BuscarPorIdAsync(contrato.Imovel_ID, cancellationToken);
        if (imovel == null || imovel.Status != Domain.Enums.StatusImovel.Disponivel)
            throw new ArgumentException("Imovel inválido ", nameof(contrato.Imovel_ID));

        contrato.LocatarioAssinaMinuta(dataTermino: request.DataTermino, imovel.ValorAluguel);
        imovel.Alugar();

        await commit.CommitAsync(cancellationToken);

        return new ResponseMinutaContrato()
        {
            Contrato_ID = contrato.Contrato_ID,
            Imovel_ID = contrato.Imovel_ID,
            Locador = new ContratoLocadorResponse()
            {
                Locador_ID = locador.Usuario_ID,
                Email = locador.Email.Endereco,
                Nome = locador.Nome.NomeCompleto
            },
            Locatario = new ContratoLocatarioResponse()
            {
                Locatario_ID = locatario.Usuario_ID,
                Email = locatario.Email.Endereco,
                Nome = locatario.Nome.NomeCompleto
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
