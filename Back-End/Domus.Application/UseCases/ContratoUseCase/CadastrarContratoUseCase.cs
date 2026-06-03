

using Domus.Application.DTOs.Contrato;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;
using Domus.Domain.Enums;

namespace Domus.Application.UseCases.ContratoUseCase;

public class CadastrarContratoUseCase(
    IContratoRepository contratoRepository, 
    IUsuarioRepository usuarioRepository, 
    IImovelRepository imovelRepository, 
    IUnitOfWork commit)
{
    public async Task<ContratoResponse> Execute(ContratoRequest request, Guid locador_ID, CancellationToken cancellationToken)
    {
        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if ( imovel == null)
            throw new ArgumentException("Imovel Inexistente ", nameof(request.Imovel_ID));

        if (imovel.Contratos.Select(c => c.Imovel_ID == imovel.Imovel_ID).FirstOrDefault())
            throw new ArgumentException("Imovel já possui contrato ativo", nameof(request.Imovel_ID));

        if (imovel.Status == StatusImovel.Indisponivel)
            throw new ArgumentException("Imovel Indisponivel ", nameof(request.Imovel_ID));

        var user = await usuarioRepository.BuscarPorIdAsync(locador_ID, cancellationToken);
        if (user == null)
            throw new ArgumentException("Locador Inexistente ", nameof(locador_ID));
        if (!user.PossuiFuncao(FuncaoUser.Locador))
            throw new ArgumentException("Usuario não é um locador ", nameof(locador_ID));

        var contrato = new Contrato(
            imovel_id: imovel.Imovel_ID
            , imovel: imovel
            , locador_id: user.Usuario_ID
            , titulo: request.Titulo
            , descricao: request.Descricao
            , urlContrato: request.UrlContrato
            , tipo: request.Tipo
            );

        await contratoRepository.AddAsync(contrato, cancellationToken);
        await commit.CommitAsync(cancellationToken);

        return new ContratoResponse()
        {
            Contrato_ID = contrato.Contrato_ID,
            Imovel_ID = contrato.Imovel_ID,
            Locador = new ContratoLocadorResponse()
            {
                Locador_ID = user.Usuario_ID,
                Email = user.Email,
                Nome = user.Nome
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
