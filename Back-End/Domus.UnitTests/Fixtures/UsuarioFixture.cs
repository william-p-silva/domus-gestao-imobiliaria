

using Domus.Application.DTOs.Usuarios.LocatarioDTOs;
using Domus.Domain.Entity;
using Domus.Domain.Enums;

namespace Domus.UnitTests.Fixtures;

public class UsuarioFixture
{
    public UsuarioRequest GerarRequestDTO(string nome)
    {
        return new UsuarioRequest()
        {
            Email = $"teste{nome.ToLower()}@gmail.com",
            Nome = "Teste " + nome,
            Senha = "123456"
        };
    }

    public Funcao GerarFuncao(FuncaoUser funcaoUser)
    {
        return new Funcao(funcaoUser);
    }

    public Usuario GerarUsuariolocador()
    {
        var usuario = new Usuario(
            nome: "Teste de Locador da Silva",
            email: "testeLocador@gmail.com",
            senhaHash: "jfgdhncxbaliubvdfjmkfkdbc"
            );

        var funcao = new Funcao(perfil: FuncaoUser.Locador);

        usuario.AddFuncaoUsuario(funcao);

        return usuario;
    }

    public Usuario GerarUsuarioLocatario()
    {
        var usuario = new Usuario(
            nome: "Teste de Locatario da Silva",
            email: "testeLocatario@gmail.com",
            senhaHash: "jfgdhncxbaliubvdfjmkfkdbc"
            );

        var funcao = new Funcao(perfil: FuncaoUser.Locatario);

        usuario.AddFuncaoUsuario(funcao);

        return usuario;
    }

    public Usuario GerarUsuarioAdmin()
    {
        var usuario = new Usuario(
            nome: "Teste de Administrador da Silva",
            email: "testeAdministrador@gmail.com",
            senhaHash: "jfgdhncxbaliubvdfjmkfkdbc"
            );

        var funcao = new Funcao(perfil: FuncaoUser.Administrador);

        usuario.AddFuncaoUsuario(funcao);

        return usuario;
    }


}
