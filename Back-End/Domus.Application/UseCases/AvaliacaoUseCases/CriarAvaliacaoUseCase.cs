

using Domus.Application.DTOs.Avaliacoes;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;


namespace Domus.Application.UseCases.AvaliacaoUseCases;

public class CriarAvaliacaoUseCase(
        IAvaliacaoRepository avaliacaoRepository
        , IUnitOfWork commit
        , IUsuarioRepository usuarioRepository
        , IImovelRepository imovelRepository
        , IContratoRepository contratoRepository
        )
{
    private readonly IAvaliacaoRepository _avaliacaoRepository = avaliacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IImovelRepository _imovelRepository = imovelRepository;
    private readonly IContratoRepository _contratoRepository = contratoRepository;
    private readonly IUnitOfWork _commit = commit;

    public async Task<AvaliacaoResponse> Execute(AvaliacaoRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(request.Usuario_ID, cancellationToken);
        if (usuario == null)
            throw new ArgumentException("Usuario Inexistente ", nameof(request.Usuario_ID));

        if (!usuario.PossuiFuncao(Domain.Enums.FuncaoUser.Locatario))
            throw new ArgumentException("Usuario não é um locatário ", nameof(request.Usuario_ID));


        var imovel = await _imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if (imovel == null)
            throw new ArgumentException("Imovel inexistente ", nameof(request.Imovel_ID));

        if (imovel.Status == Domain.Enums.StatusImovel.Indisponivel)
            throw new ArgumentException("Imovel não disponível para avaliação ", nameof(request.Imovel_ID));


        var contrato = await _contratoRepository.BuscarPorIdAsync(request.Contrato_ID, cancellationToken);
        if (contrato == null)
            throw new ArgumentException("Contrato inexistente ", nameof(request.Contrato_ID));

        if (contrato.Status == Domain.Enums.StatusContrato.Inativo || contrato.Status == Domain.Enums.StatusContrato.Rascunho)
            throw new ArgumentException("Contrato não ativo ", nameof(request.Contrato_ID));

        if (contrato.Locatario_ID == usuario.Usuario_ID)
            throw new ArgumentException("O usuario que fará a avaliação deve ser o mesmo do contrato ", nameof(request.Usuario_ID));


        if (imovel.Imovel_ID == contrato.Imovel_ID)
            throw new ArgumentException("O imovel deve ser o mesmo do contrato ", nameof(request.Imovel_ID));


        Avaliacao avaliacao = new Avaliacao(
            usuario_id: request.Usuario_ID
            , imovel_id: request.Imovel_ID
            , contrato_id: request.Contrato_ID
            , titulo: request.Titulo
            , descricao: request.Descricao
            , nota: request.Nota
            );

        await _avaliacaoRepository.AddAsync(avaliacao, cancellationToken);

        await _commit.CommitAsync(cancellationToken);

        return new AvaliacaoResponse()
        {
            Avaliacao_ID = avaliacao.Avaliacao_ID,
            Usuario = new AvaliacaoUsuarioResponse
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Usuario_ID = usuario.Usuario_ID
            },
            Imovel = new AvaliacaoImovelResponse
            {
                Imovel_ID = imovel.Imovel_ID,
                Locador_ID = imovel.Usuario_ID,
                Titulo = imovel.Titulo
            },
            Contrato = new AvaliacaoContratoResponse
            {
                CriadoEm = contrato.CriadoEm,
                Contrato_ID = contrato.Contrato_ID,
                DataInicio = contrato.DataInicio,
                DataTermino = contrato.DataTermino
            },
            Titulo = avaliacao.Titulo,
            Descricao = avaliacao.Descricao,
            Nota = avaliacao.Nota,
            PublicadoEm = avaliacao.PublicadoEm,
        };
    }
}
