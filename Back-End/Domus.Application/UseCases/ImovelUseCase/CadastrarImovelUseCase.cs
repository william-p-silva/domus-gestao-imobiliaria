
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Enums;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;
using Domus.Application.Mappers.Imovel;

namespace Domus.Application.UseCases.ImovelUseCase;

public class CadastrarImovelUseCase(
    IImovelRepository imovelRepository, 
    IUsuarioRepository usuarioRepository, 
    IUnitOfWork commit)
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
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
    public async Task<ImovelResponse> Execute(ImovelRequest request, Guid user_id, CancellationToken cancellationToken)
    {
        var user = await _usuarioRepository.BuscarPorIdAsync(user_id, cancellationToken);
        if (user == null)
            throw new NotFoundException("Usuário não encontrado");

        if (!user.PossuiFuncao(FuncaoUser.Locador))
            throw new BusinessRuleException("Usuário não tem permissão para cadastrar um imóvel");


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
            usuario_id: user.Usuario_ID,
            endereco_id: endereco.Endereco_ID,
            titulo: request.Titulo,
            descricao: request.Descricao,
            comodos: request.Comodos,
            status: request.Status,
            valorAluguel: request.ValorAluguel,
            banheiros: request.Banheiros,
            metrosQuadrados: request.MetrosQuadrados,
            tipo: request.TipoDoImovel,
            endereco: endereco,
            usuario: user
        );

        await _imovelRepository.AddAsync(imovel, cancellationToken);

        await _commit.CommitAsync(cancellationToken);

        return imovel.ToResponse();
    }
}
