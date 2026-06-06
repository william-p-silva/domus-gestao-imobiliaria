
using Domus.Application.DTOs.Contrato;
using Domus.Domain.Entity;

namespace Domus.UnitTests.Fixtures;

public class ContratoFixture
{
    public Contrato GerarContrato()
    {
        return new Contrato(
            imovel_id: Guid.NewGuid(),
            imovel: null,
            locador_id: Guid.NewGuid(),
            titulo: "Contrato de Locação",
            descricao: "Contrato de locação para o imóvel X",
            urlContrato: "http://exemplo.com/contrato.pdf",
            tipo: "Locação"
        );
    }

    public ContratoRequest GerarRequestContrato(Guid imovel_ID)
    {
        return new ContratoRequest()
        {
            Imovel_ID = imovel_ID,
            Titulo = "Contrato de Locação",
            Descricao = "Contrato de locação para o imóvel X",
            UrlContrato = "http://exemplo.com/contrato.pdf",
            Tipo = "Locação"
        };
    }

    public Imovel GerarImovelContrato(Guid locador_ID)
    {
        return new Imovel(
                usuario_id: locador_ID,
                endereco_id: Guid.NewGuid(),
                titulo: "Teste de Contrato",
                descricao: "Teste de descricao de contrato",
                comodos: 5,
                status: Domain.Enums.StatusImovel.Disponivel,
                valorAluguel: 1500
            );
    }

    public Usuario GerarUsuarioContrato()
    {
        var usuario = new Usuario(
            nome: "Tete de Locador dos Contratos da Silva",
            email: "testeContratoLocador@gmail.com",
            senhaHash: "jfgdhncxbaliubvdfjmkfkdbc"
            );

        var funcao = new Funcao(perfil: Domain.Enums.FuncaoUser.Locador);

        usuario.AddFuncaoUsuario(funcao);

        return usuario;
    }
}
