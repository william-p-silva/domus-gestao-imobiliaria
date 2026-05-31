

using Domus.Application.DTOs.Avaliacoes;
using Domus.Application.Interfaces.Repositories;
using Domus.Domain.Entity;


namespace Domus.Application.UseCases.AvaliacaoUseCases;

public class CriarAvaliacaoUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IImovelRepository _imovelRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly IUnitOfWork _commit;

    public CriarAvaliacaoUseCase( 
        IAvaliacaoRepository avaliacaoRepository
        ,IUnitOfWork commit
        ,IUsuarioRepository usuarioRepository
        ,IImovelRepository imovelRepository
        ,IContratoRepository contratoRepository
        )
    {
        _avaliacaoRepository = avaliacaoRepository;
        _commit = commit;
        _usuarioRepository = usuarioRepository;
        _imovelRepository = imovelRepository;
        _contratoRepository = contratoRepository;
    }

    public async Task<AvaliacaoResponse> Execute(AvaliacaoRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(request.Usuario_ID, cancellationToken);
        if (usuario == null) 
            throw new ArgumentException("Usuario Inexistente ", nameof(request.Usuario_ID));

        var imovel = await _imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if (imovel == null)
            throw new ArgumentException("Imovel inexistente ", nameof(request.Imovel_ID));

        var contrato = await _contratoRepository.BuscarPorIdAsync(request.Contrato_ID, cancellationToken);
        if (contrato == null)
            throw new ArgumentException("Contrato inexistente ", nameof(request.Contrato_ID));

        Avaliacao avaliacao = new Avaliacao(
            usuario_id: request.Usuario_ID
            ,imovel_id: request.Imovel_ID
            ,contrato_id: request.Contrato_ID
            ,titulo: request.Titulo
            ,descricao: request.Descricao
            ,nota: request.Nota
            );

        await _avaliacaoRepository.AddAsync(avaliacao, cancellationToken);

        await _commit.CommitAsync(cancellationToken);

        return new AvaliacaoResponse()
        {
            Avaliacao_ID = avaliacao.Avaliacao_ID,
            Usuario_ID = avaliacao.Usuario_ID,
            Imovel_ID = avaliacao.Imovel_ID,
            Contrato_ID = avaliacao.Contrato_ID,
            Titulo = avaliacao.Titulo,
            Descricao = avaliacao.Descricao,
            Nota = avaliacao.Nota,
            PublicadoEm = avaliacao.PublicadoEm,
        };
    }
}
