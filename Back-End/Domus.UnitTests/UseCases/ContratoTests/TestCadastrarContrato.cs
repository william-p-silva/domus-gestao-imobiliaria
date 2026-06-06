using Domus.Application.Interfaces.Repositories;
using Domus.Application.UseCases.ContratoUseCase;
using Domus.Domain.Entity;
using Domus.UnitTests.Fixtures;
using Moq;

namespace Domus.UnitTests.UseCases.ContratoTests;

public class TestsCadastrarContrato(ContratoFixture contratoFixture) : IClassFixture<ContratoFixture>
{
    private readonly Mock<IContratoRepository> _contratoMock = new();
    private readonly Mock<IUsuarioRepository> _usuarioMock = new();
    private readonly Mock<IImovelRepository> _imovelMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();


    [Fact]
    public async Task DeveRetornarSucesso_QuandoDadosValidos()
    {
        //Arrange
        var contratoFake = contratoFixture.GerarContrato();
        var usuarioFake = contratoFixture.GerarUsuarioContrato();
        var imovelFake = contratoFixture.GerarImovelContrato(usuarioFake.Usuario_ID);
        var contratoRequest = contratoFixture.GerarRequestContrato(imovelFake.Imovel_ID);

        //Act
        _imovelMock.Setup(x => x.BuscarPorIdAsync(contratoRequest.Imovel_ID, It.IsAny<CancellationToken>())).ReturnsAsync(imovelFake);

        _usuarioMock.Setup(x => x.BuscarPorIdAsync(usuarioFake.Usuario_ID, It.IsAny<CancellationToken>())).ReturnsAsync(usuarioFake);

        var useCaseContrato = new CadastrarContratoUseCase(
            contratoRepository: _contratoMock.Object,
            usuarioRepository: _usuarioMock.Object,
            imovelRepository: _imovelMock.Object,
            commit: _unitOfWorkMock.Object
            );

        var contrato = await useCaseContrato.Execute(
            request: contratoRequest, 
            locador_ID: usuarioFake.Usuario_ID, 
            It.IsAny<CancellationToken>());

        //Asserts
        Assert.NotNull(contrato);
        Assert.Equal(contratoRequest.Titulo, contrato.Titulo);
        Assert.Equal(contratoRequest.UrlContrato, contrato.UrlContrato);
        Assert.Equal(contratoRequest.Imovel_ID, contrato.Imovel_ID);
        Assert.Equal(usuarioFake.Usuario_ID, contrato.Locador.Locador_ID);


        _usuarioMock.Verify(x => x.BuscarPorIdAsync(usuarioFake.Usuario_ID, It.IsAny<CancellationToken>()), Times.Once);
        _imovelMock.Verify(x => x.BuscarPorIdAsync(imovelFake.Imovel_ID, It.IsAny<CancellationToken>()), Times.Once);
        _contratoMock.Verify(x => x.AddAsync(It.IsAny<Contrato>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

