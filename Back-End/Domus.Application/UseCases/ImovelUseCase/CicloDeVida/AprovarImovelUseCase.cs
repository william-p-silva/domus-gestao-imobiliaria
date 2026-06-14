
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.CicloDeVida;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Enums;

namespace Domus.Application.UseCases.ImovelUseCase.CicloDeVida;

public class AprovarImovelUseCase(
    IUsuarioRepository usuarioRepository,
    IImovelRepository imovelRepository,
    IUnitOfWork commit
    )
{
    

    public async Task<ImovelResponse> Execute(
        RequestAprovarImovel request, Guid admId, 
        CancellationToken cancellationToken)
    {
        var admin = await usuarioRepository.BuscarPorIdAsync(admId, cancellationToken);
        if (admin == null || !admin.PossuiFuncao(FuncaoUser.Administrador))
            throw new ArgumentException("Usuario inválido ", nameof(admId));

        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if (imovel == null)
            throw new ArgumentException("Imovel inválido ", nameof(request.Imovel_ID));

        imovel.AvaliarImovel(request.Aprovado);

        await commit.CommitAsync(cancellationToken);

        return new ImovelResponse()
        {
            Imovel_ID = imovel.Imovel_ID,
            Titulo = imovel.Titulo,
            Descricao = imovel.Descricao,
            Endereco = new EnderecoResponse()
            {
                CEP = imovel.Endereco.CEP,
                UF = imovel.Endereco.UF,
                Cidade = imovel.Endereco.Cidade,
                Bairro = imovel.Endereco.Bairro,
                Rua = imovel.Endereco.Rua,
                Numero = imovel.Endereco.Numero,
                Complemento = imovel.Endereco.Complemento,
                Endereco_ID = imovel.Endereco_ID,
            },
            Comodos = imovel.Comodos,
            CriadoEm = imovel.CriadoEm,
            Status = imovel.Status.ToString(),
            Usuario_ID = imovel.Usuario_ID,
            ValorAluguel = imovel.ValorAluguel,
            Aprovado = imovel.Aprovado,
            Avaliado = imovel.Avaliado
        };
    }
}
