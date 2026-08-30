
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.DTOs.Imovel.Listar;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class ListarImoveisComFiltroUseCase(IImovelRepository imovelRepository)
{
    public async Task<List<ImovelResponse>> Execute(FiltroImovel filtro, CancellationToken cancellationToken)
    {
        if(filtro is null)
            throw new ArgumentNullException("O filtro não pode ser nulo. ",nameof(filtro));
        var imoveis = await imovelRepository.ListarComFiltroAsync(filtro, cancellationToken);

        return imoveis.Select(i => new ImovelResponse
        {
            Imovel_ID = i.Imovel_ID,
            Locador = new ResponseUsuarioImovel
            {
                Usuario_ID = i.Usuario.Usuario_ID,
                Email = i.Usuario.Email.Endereco,
                Nome = i.Usuario.Nome.NomeCompleto
            },
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
