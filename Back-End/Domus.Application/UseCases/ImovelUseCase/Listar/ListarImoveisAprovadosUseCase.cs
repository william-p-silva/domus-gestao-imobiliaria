using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class ListarImoveisAprovadosUseCase(IImovelRepository imovelRepository)
{

    public async Task<List<ImovelResponse>> Execute()
    {
        var imoveis = await imovelRepository.ListarAsync(aprovados: true);

        return imoveis.Select(x => new ImovelResponse
        {
            Aprovado = x.Aprovado,
            Avaliado = x.Avaliado,
            Comodos = x.Comodos,
            CriadoEm = x.CriadoEm,
            Descricao = x.Descricao,
            Endereco = new EnderecoResponse
            {
                CEP = x.Endereco.CEP,
                UF = x.Endereco.UF,
                Cidade = x.Endereco.Cidade,
                Bairro = x.Endereco.Bairro,
                Rua = x.Endereco.Rua,
                Numero = x.Endereco.Numero,
                Complemento = x.Endereco.Complemento,
                Endereco_ID = x.Endereco_ID
            },
            Imovel_ID = x.Imovel_ID,
            Status = x.Status.ToString(),
            Titulo = x.Titulo,
            Usuario_ID = x.Usuario_ID,
            ValorAluguel = x.ValorAluguel,
            Banheiros = x.Banheiros,
            MetrosQuadrados = x.MetrosQuadrados,
            TipoDoImovel = x.Tipo.ToString()
        }).ToList();
    }
}
