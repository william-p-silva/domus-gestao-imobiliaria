
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.ImovelUseCase;

public class CadastrarImovelUseCase(IImovelRepository imovelRepository, IUsuarioRepository usuarioRepository, IUnitOfWork commit, IEnderecoRepository enderecoRepository)
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;
    private readonly IUnitOfWork _commit = commit;

    public async Task<ImovelResponse> Execute(ImovelRequest request, CancellationToken cancellationToken)
    {
        var user = await _usuarioRepository.BuscarPorIdAsync(request.Usuario_ID, cancellationToken);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado", nameof(request.Usuario_ID));

        var endereco = new Endereco(
            cep: request.Endereco.CEP,
            uf: request.Endereco.UF,
            cidade: request.Endereco.Cidade,
            bairro: request.Endereco.Bairro,
            rua: request.Endereco.Rua,
            numero: request.Endereco.Numero,
            complemento: request.Endereco.Complemento
        );

        var imovel = new Imovel(
            usuario_id: request.Usuario_ID,
            endereco_id: endereco.Endereco_ID,
            titulo: request.Titulo,
            descricao: request.Descricao,
            comodos: request.Comodos,
            status: request.Status,
            valorAluguel: request.ValorAluguel
        );


        await _enderecoRepository.AddAsync(endereco, cancellationToken);
        await _imovelRepository.AddAsync(imovel, cancellationToken);

        await _commit.CommitAsync(cancellationToken);

        return new ImovelResponse()
        {
            Imovel_ID = imovel.Imovel_ID,
            Usuario_ID = imovel.Usuario_ID,
            Titulo = imovel.Titulo,
            Descricao = imovel.Descricao,
            Comodos = imovel.Comodos,
            Status = imovel.Status,
            ValorAluguel = imovel.ValorAluguel,
            CriadoEm = imovel.CriadoEm,
            Endereco = new EnderecoResponse()
            {
                Endereco_ID = endereco.Endereco_ID,
                CEP = endereco.CEP,
                UF = endereco.UF,
                Cidade = endereco.Cidade,
                Bairro = endereco.Bairro,
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento
            }
        };
    }
}
