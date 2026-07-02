
using Domus.Application.DTOs.Contrato;
using Domus.Application.DTOs.Contrato.CicloDeVida;
using Domus.Domain.Entity;
using Domus.Domain.Enums;

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
                banheiros: 2,
                tipo: TipoImovel.Casa,
                metrosQuadrados: 25.25m,
                usuario_id: locador_ID,
                endereco_id: Guid.NewGuid(),
                titulo: "Teste de Contrato",
                descricao: "Teste de descricao de contrato",
                comodos: 5,
                status: Domain.Enums.StatusImovel.Disponivel,
                valorAluguel: 1500
            );
    }

    public Usuario GerarUsuarioContrato(FuncaoUser funcaoFake = FuncaoUser.Locador)
    {
        var usuario = new Usuario(
            nome: "Teste de Locador dos Contratos da Silva",
            emailAConfirmar: "testeContratoLocador@gmail.com",
            senhaHash: "jfgdhncxbaliubvdfjmkfkdbc"
            );

        var funcao = new Funcao(perfil: funcaoFake);

        usuario.AddFuncaoUsuario(funcao);

        return usuario;
    }

    public RequestDisponibilizarAssinatura disponibilizarAssinatura()
    {
        return new RequestDisponibilizarAssinatura()
        {
            Contrato_ID = Guid.Parse("9CAD6F47-6F28-4F4B-BE96-A4C5435F54AC"),
            Locatario_ID = Guid.Parse("d162af62-af03-419b-a58a-7f894992bf05")
        };
    }
}
