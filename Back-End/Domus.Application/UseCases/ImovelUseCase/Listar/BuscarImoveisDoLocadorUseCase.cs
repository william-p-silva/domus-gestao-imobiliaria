

using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Imovel;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Mappers.Imovel;
using Domus.Domain.Entity;

namespace Domus.Application.UseCases.ImovelUseCase.Listar;

public class BuscarImoveisDoLocadorUseCase(
    IUsuarioRepository usuarioRepository,
    IImovelRepository imovelRepository
    )
{
    public async Task<List<ImovelResponse>> Execute(
        Guid locadorId, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(locadorId, cancellationToken);
        if (usuario is null)
            throw new ArgumentException($"Usuário com ID {locadorId} não encontrado.");
        if (!usuario.PossuiFuncao(Domus.Domain.Enums.FuncaoUser.Locador))
            throw new ArgumentException($"Usuário com ID {locadorId} não é um locador.");

        var imoveis = await imovelRepository.ListarImoveisLocador(locadorId, cancellationToken);

        return imoveis.Select(i => i.ToResponse()).ToList();
    }
}
