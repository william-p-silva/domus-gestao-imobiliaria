

using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.UseCases.CaracteristicaUseCase;
using Domus.Domain.Entity;
using Domus.Domain.Exceptions.Domain;
using Moq;

namespace Domus.UnitTests.UseCases.CaracteristicaImovel;

public class CadastrarCaracteristicaUseCaseTest
{
    private Mock<ICaracteristicaRepository> _caracteristicaRepository = new();


    [Fact]
    public async Task DeveRetornarSucesso_QuandoDadosValidos()
    {
        //Arrange
        var request = new CadastrarCaracteristicaRequest
        {
            Nome = "Teste"
        };

        _caracteristicaRepository.Setup(x => 
            x.BuscarPorNome(request.Nome, 
                It.IsAny<CancellationToken>())).ReturnsAsync((Caracteristica?) null);

        _caracteristicaRepository.Setup(x => 
            x.AddAsync(It.IsAny<Caracteristica>(), 
                It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        //Act
        var useCase = new CadastrarCaracteristicaUseCase(
            _caracteristicaRepository.Object
            );

        await useCase.ExecuteAsync(request);

        //Assert
        _caracteristicaRepository.Verify(x => 
            x.BuscarPorNome(request.Nome,
                It.IsAny<CancellationToken>()), Times.Once);

        _caracteristicaRepository.Verify(x =>
            x.AddAsync(It.IsAny<Caracteristica>(),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeveLancarException_QuandoCaracteristicaExistir()
    {
        //Arrange
        var request = new CadastrarCaracteristicaRequest
        {
            Nome = "Teste"
        };

        _caracteristicaRepository.Setup(x =>
            x.BuscarPorNome(request.Nome,
                It.IsAny<CancellationToken>())).ReturnsAsync(new Caracteristica("Teste"));

        _caracteristicaRepository.Setup(x =>
            x.AddAsync(It.IsAny<Caracteristica>(),
                It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        //Act
        var useCase = new CadastrarCaracteristicaUseCase(
            _caracteristicaRepository.Object
            );

        var result = await Assert.ThrowsAsync<BusinessRuleException>(() => useCase.ExecuteAsync(request));

        //Assert
        _caracteristicaRepository.Verify(x =>
            x.BuscarPorNome(request.Nome,
                It.IsAny<CancellationToken>()), Times.Once);

        _caracteristicaRepository.Verify(x =>
            x.AddAsync(It.IsAny<Caracteristica>(),
                It.IsAny<CancellationToken>()), Times.Never);
    }
}
