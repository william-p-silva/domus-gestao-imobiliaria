

using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class ListarImoveisNaoAvaliadosUseCase(
    IImovelRepository imovelRepository
    )
{
    public async Task<List<ImovelResponse>> Execute(CancellationToken cancellationToken)
    {
        var imoveis = await imovelRepository.ListarAvaliadosAsync(avaliados: false, cancellationToken);

        return imoveis.Select(i => new ImovelResponse
        {
            Imovel_ID = i.Imovel_ID,
            Usuario_ID = i.Usuario_ID,
            Titulo = i.Titulo,
            Descricao = i.Descricao,
            Comodos = i.Comodos,
            Status = i.Status.ToString(),
            ValorAluguel = i.ValorAluguel,
            CriadoEm = i.CriadoEm,
            Aprovado = i.Aprovado,
            Avaliado = i.Avaliado,
            MetrosQuadrados = i.MetrosQuadrados,
            Banheiros = i.Banheiros,
            TipoDoImovel = i.Tipo.ToString(),
            Endereco = new EnderecoResponse
            {
                Endereco_ID = i.Endereco_ID,
                CEP = i.Endereco.CEP,
                UF = i.Endereco.UF,
                Cidade = i.Endereco.Cidade,
                Bairro = i.Endereco.Bairro,
                Rua = i.Endereco.Rua,
                Numero = i.Endereco.Numero,
                Complemento = i.Endereco.Complemento
            }
        }).ToList();
    }
}
