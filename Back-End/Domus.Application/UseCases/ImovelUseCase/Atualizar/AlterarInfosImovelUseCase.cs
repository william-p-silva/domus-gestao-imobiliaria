

using Domus.Application.DTOs.Imovel.Atualizar;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;

namespace Domus.Application.UseCases.ImovelUseCase.Atualizar;

public class AlterarInfosImovelUseCase(
    IPasswordHasher passwordHasher,
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork,
    IImovelRepository imovelRepository)
{

    public async Task<string> Execute(
        RequestAlterarInfosImovel request, Guid usuarioId, 
        CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuarioId, cancellationToken);
        if (usuario is null)
            throw new ArgumentException("Usuário não encontrado", nameof(usuarioId));
        if (!passwordHasher.VerificarSenha(usuario.SenhaHash, request.ConfirmaSenha))
            throw new ArgumentException("Senha incorreta", nameof(request.ConfirmaSenha));

        var imovel = await imovelRepository.BuscarPorIdAsync(request.Imovel_ID, cancellationToken);
        if (imovel is null)
            throw new ArgumentException("Imóvel não encontrado", nameof(request.Imovel_ID));
        if(imovel.Status != Domain.Enums.StatusImovel.Disponivel)
            throw new ArgumentException("Imóvel não está disponível para alteração de informações", nameof(imovel.Status));

        imovel.VerificarProprietario(usuarioId);


        bool alterou = false;
        if (!string.IsNullOrWhiteSpace(request.Titulo) 
            && request.Titulo != imovel.Titulo
            )
        {
            imovel.AlterarTitulo(request.Titulo);
            alterou = true;
        }

        if (!string.IsNullOrWhiteSpace(request.Descricao)
            && request.Descricao != imovel.Descricao
            )
        {
            imovel.AlterarDescricao(request.Descricao);
            alterou = true;
        }

        if (request.Tipo is not null && request.Tipo.Value != imovel.Tipo)
        {
            imovel.AlterarTipo(request.Tipo.Value);
            alterou = true;
        }

        if (request.Banheiros is not null && request.Banheiros.Value != imovel.Banheiros)
        {
            imovel.AlterarBanheiros(request.Banheiros.Value);
            alterou = true;
        }

        if (request.Comodos is not null && request.Comodos.Value != imovel.Comodos)
        {
            imovel.AlterarComodos(request.Comodos.Value);
            alterou = true;
        }

        if (request.MetrosQuadrados is not null && request.MetrosQuadrados.Value != imovel.MetrosQuadrados)
        {
            imovel.AlterarMetrosQuadrados(request.MetrosQuadrados.Value);
            alterou = true;
        }

        if (request.ValorAluguel is not null && request.ValorAluguel.Value != imovel.ValorAluguel)
        {
            imovel.AlterarValorAluguel(request.ValorAluguel.Value);
            alterou = true;
        }

        if (!alterou)
            throw new ArgumentException("Nenhum campo foi alterado", nameof(request));

        await unitOfWork.CommitAsync(cancellationToken);

        return "Informações do imóvel alteradas com sucesso";

    }
}
