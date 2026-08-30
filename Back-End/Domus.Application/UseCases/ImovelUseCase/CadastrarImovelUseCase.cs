
using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Enums;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.ImovelUseCase;

public class CadastrarImovelUseCase(
    IImovelRepository imovelRepository, 
    IUsuarioRepository usuarioRepository, 
    IUnitOfWork commit, 
    IEnderecoRepository enderecoRepository)
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IEnderecoRepository _enderecoRepository = enderecoRepository;
    private readonly IUnitOfWork _commit = commit;



    /// <summary>
    /// Caso de Uso: Realiza o cadastramento e publicação de um novo imóvel no sistema, criando também o seu respectivo endereço.
    /// </summary>
    /// <remarks>
    /// O fluxo garante a atomicidade da operação salvando o endereço e o imóvel sob a mesma transação, 
    /// além de certificar que apenas usuários com o perfil de Locador possam disponibilizar propriedades.
    /// </remarks>
    /// <param name="request">DTO com as informações estruturais do imóvel e os dados aninhados do endereço.</param>
    /// <param name="cancellationToken">Token de cancelamento para controle de concorrência e aborto da operação.</param>
    /// <returns>Um <see cref="ImovelResponse"/> contendo o ID gerado para o imóvel e os detalhes do endereço persistido.</returns>
    /// <exception cref="ArgumentException">
    /// Lançada caso o identificador do usuário não seja encontrado ou se o usuário não possuir o perfil <see cref="FuncaoUser.Locador"/>.
    /// </exception>
    public async Task<ImovelResponse> Execute(ImovelRequest request, CancellationToken cancellationToken)
    {
        var user = await _usuarioRepository.BuscarPorIdAsync(request.Usuario_ID, cancellationToken);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado", nameof(request.Usuario_ID));

        if (!user.PossuiFuncao(FuncaoUser.Locador))
            throw new ArgumentException("Usuário não tem permissão para cadastrar um imóvel", nameof(request.Usuario_ID));


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
            valorAluguel: request.ValorAluguel,
            banheiros: request.Banheiros,
            metrosQuadrados: request.MetrosQuadrados,
            tipo: request.TipoDoImovel
        );


        await _enderecoRepository.AddAsync(endereco, cancellationToken);
        await _imovelRepository.AddAsync(imovel, cancellationToken);

        await _commit.CommitAsync(cancellationToken);

        return new ImovelResponse()
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
            },
            Aprovado = imovel.Aprovado,
            Avaliado = imovel.Avaliado,
            Banheiros = imovel.Banheiros,
            MetrosQuadrados = imovel.MetrosQuadrados,
            TipoDoImovel = imovel.Tipo.ToString()
        };
    }
}
