

using Domus.Application.DTOs.Endereco;
using Domus.Application.DTOs.Usuarios.Perfil;
using Domus.Application.Interfaces.Repositories;

namespace Domus.Application.UseCases.UsuarioUseCase.Listar;

public class BuscarPerfilUseCase(IUsuarioRepository usuarioRepository)
{

    public async Task<PerfilUsuarioResponse> Execute(Guid usuarioId, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.BuscarPorIdAsync(usuarioId, cancellationToken);
        if (usuario is null)
            throw new ArgumentException("Usuário não encontrado.", nameof(usuarioId));


        var cpfMascarado = string.IsNullOrEmpty(usuario.CPF.Numero) ? "" : (usuario.CPF.Numero.Substring(0, 3) + "****" + usuario.CPF.Numero.Substring(7));


        return new PerfilUsuarioResponse
        {
            Usuario_Id = usuario.Usuario_ID,
            Celular = usuario.Celular.Numero ?? "",
            Email = usuario.Email.Endereco,
            CriadoEm = usuario.CriadoEm,
            Nome = usuario.Nome.NomeCompleto,
            Endereco = usuario.Endereco_ID is null ? null : new EnderecoResponse
            {
                Endereco_ID = usuario.EnderecoUsuario.Endereco_ID,
                CEP = usuario.EnderecoUsuario.CEP,
                UF = usuario.EnderecoUsuario.UF,
                Cidade = usuario.EnderecoUsuario.Cidade,
                Bairro = usuario.EnderecoUsuario.Bairro,
                Rua = usuario.EnderecoUsuario.Rua,
                Numero = usuario.EnderecoUsuario.Numero,
                Complemento = usuario.EnderecoUsuario.Complemento,
            },
            CPFMascarado = cpfMascarado,
            Funcao = usuario.UsuarioFuncao.Select(uf => uf.Funcao.Nome.ToString()).ToList()
        };

    }
}
