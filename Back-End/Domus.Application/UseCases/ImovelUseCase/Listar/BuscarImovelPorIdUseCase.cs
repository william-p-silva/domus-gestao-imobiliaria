
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class BuscarImovelPorIdUseCase(
    IImovelRepository imovelRepository,
    IUsuarioRepository usuarioRepository
    )
{
    public async Task<ImovelResponse> Execute(Guid ImovelId, CancellationToken cancellationToken)
    {
        var imovel = await imovelRepository.BuscarPorIdAsync(ImovelId, cancellationToken);
        if (imovel is null)
            throw new ArgumentException("Imovel não encontrado. ", nameof(ImovelId));

        return new ImovelResponse
        {
            Imovel_ID = imovel.Imovel_ID,
            Locador = new ResponseUsuarioImovel
            {
                Usuario_ID = imovel.Usuario.Usuario_ID,
                Email = imovel.Usuario.Email.Endereco,
                Nome = imovel.Usuario.Nome.NomeCompleto
            },
            Titulo = imovel.Titulo,
            Descricao = imovel.Descricao,
            Comodos = imovel.Comodos,
            Status = imovel.Status.ToString(),
            ValorAluguel = imovel.ValorAluguel,
            CriadoEm = imovel.CriadoEm,
            Aprovado = imovel.Aprovado,
            Avaliado = imovel.Avaliado,
            MetrosQuadrados = imovel.MetrosQuadrados,
            Banheiros = imovel.Banheiros,
            TipoDoImovel = imovel.Tipo.ToString(),
            Endereco = new EnderecoResponse
            {
                Endereco_ID = imovel.Endereco_ID,
                CEP = imovel.Endereco.CEP,
                UF = imovel.Endereco.UF,
                Cidade = imovel.Endereco.Cidade,
                Bairro = imovel.Endereco.Bairro,
                Rua = imovel.Endereco.Rua,
                Numero = imovel.Endereco.Numero,
                Complemento = imovel.Endereco.Complemento
            }
        };
    }
}
