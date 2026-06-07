

using Domus.Application.Interfaces.Repositories;
using Domus.Application.Interfaces.Security;
using Domus.Application.UseCases.UsuarioUseCase.LocatarioUseCase;
using Domus.Domain.Entity;
using Domus.Domain.Enums;
using Domus.UnitTests.Fixtures;
using Moq;

namespace Domus.UnitTests.UseCases.UsuarioTests.LocatarioTests;

public class TestCadastroLocatario(UsuarioFixture fixture) : IClassFixture<UsuarioFixture>
{
    private readonly Mock<IUsuarioRepository> _userMock = new();
    private readonly Mock<IFuncaoRepository> _funcaoMock = new();
    private readonly Mock<IPasswordHasher> _hashMock = new();
    private readonly Mock<IUnitOfWork> _commitMock = new();
    

    [Fact]
    public async Task RetornarSucesso_QuandoDadosValidos()
    {
        //Arrange
        var requestLocatario = fixture.GerarRequestDTO("Locatario");
        var funcaoUser = fixture.GerarFuncao(FuncaoUser.Locatario);

        //Act
        _userMock.Setup(x => x.BuscarPorEmailAsync(requestLocatario.Email, It.IsAny<CancellationToken>())).ReturnsAsync((Usuario?) null);

        _hashMock.Setup(x => x.GerarHash(requestLocatario.Senha)).Returns("gfnckjvlnbkjnckjbnckjvnbjncvjbncnbc");

        _funcaoMock.Setup(x => x.BuscarPorNomeAsync("Locatario", It.IsAny<CancellationToken>())).ReturnsAsync(funcaoUser);

        var useCaseCadastroLocatario = new CadastrarLocatarioUseCase(
            usuarioRepository: _userMock.Object,
            passwordHasher: _hashMock.Object,
            funcaoRepository: _funcaoMock.Object,
            commit: _commitMock.Object
            );

        var user = await useCaseCadastroLocatario.Execute(requestLocatario, It.IsAny<CancellationToken>());

        //Asserts
        Assert.NotNull(user);
        Assert.Equal(requestLocatario.Nome, user.Nome);
        Assert.Equal(requestLocatario.Email, user.Email);
        Assert.Equal("Locatario", user.Perfil.FirstOrDefault());

        _commitMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _userMock.Verify(x => x.BuscarPorEmailAsync(requestLocatario.Email, It.IsAny<CancellationToken>()), Times.Once);
        _userMock.Verify(x => x.AddAsync(It.IsAny<Usuario>(), It.IsAny<CancellationToken>()), Times.Once);
        _funcaoMock.Verify(x => x.BuscarPorNomeAsync("Locatario", It.IsAny<CancellationToken>()), Times.Once);
        _hashMock.Verify(x => x.GerarHash(It.IsAny<string>()), Times.Once);
    }
}
