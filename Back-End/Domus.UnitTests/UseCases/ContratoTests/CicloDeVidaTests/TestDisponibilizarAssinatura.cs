

using Domus.Application.Interfaces.Repositories;
using Domus.Application.UseCases.ContratoUseCase.CicloDeVida;
using Domus.UnitTests.Fixtures;
using Moq;

namespace Domus.UnitTests.UseCases.ContratoTests.CicloDeVidaTests;

public class TestDisponibilizarAssinatura(ContratoFixture fixture) : IClassFixture<ContratoFixture>
{
    private readonly Mock<IUsuarioRepository> mockUsuario = new();
    private readonly Mock<IContratoRepository> mockContrato = new();
    private readonly Mock<IUnitOfWork> mockCommit = new();

    [Fact]
    public async Task RetornarSucesso_QuandoDadosValidos()
    {
        //Arrange
        var requestFake = fixture.disponibilizarAssinatura();
        var locadorIdFake = Guid.Parse("43dc0319-6a36-4996-ac30-8a339994b22b");
        var contratoFake = fixture.GerarContrato();
        var locatario = fixture.GerarUsuarioContrato(Domain.Enums.FuncaoUser.Locatario);
        var locador = fixture.GerarUsuarioContrato(Domain.Enums.FuncaoUser.Locador);

        //Act
        mockContrato.Setup(x => x.BuscarPorIdAsync(requestFake.Contrato_ID, It.IsAny<CancellationToken>())).ReturnsAsync(contratoFake);

        mockUsuario.Setup(x => x.BuscarPorIdAsync(requestFake.Locatario_ID, It.IsAny<CancellationToken>())).ReturnsAsync(locatario);

        mockUsuario.Setup(x => x.BuscarPorIdAsync(locadorIdFake, It.IsAny<CancellationToken>())).ReturnsAsync(locador);

        var useCase = new DisponibilizarParaAssinaturaUseCase(
            contratoRepository: mockContrato.Object,
            usuarioRepository: mockUsuario.Object,
            commit: mockCommit.Object
            );

        var contrato = await useCase.Execute(requestFake, locadorIdFake, It.IsAny<CancellationToken>());

        //Assert
        Assert.NotNull(contrato);

        mockUsuario.Verify(x => x.BuscarPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Exactly(2));

        mockCommit.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

}
