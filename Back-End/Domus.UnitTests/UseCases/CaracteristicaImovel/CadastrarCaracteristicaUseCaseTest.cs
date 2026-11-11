

using Domus.Application.DTOs.Caracteristica.Request;
using Domus.Application.Interfaces.Repositories;
using Domus.Application.UseCases.CaracteristicaUseCase;
using Domus.Domain.Entity;
using Domus.Domain.Enums.Caracteristica;
using Domus.Domain.Exceptions.Domain;
using Moq;

namespace Domus.UnitTests.UseCases.CaracteristicaImovel;

public class CadastrarCaracteristicaUseCaseTest
{
    private Mock<ICaracteristicaRepository> _caracteristicaRepository = new();
    private Mock<IUnitOfWork> _unitOfWork = new();

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

        _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        //Act
        var useCase = new CadastrarCaracteristicaUseCase(
            _caracteristicaRepository.Object
            ,_unitOfWork.Object
            );

        var result = await useCase.ExecuteAsync(request, It.IsAny<CancellationToken>());

        //Assert
        Assert.Equal(request.Nome, result.Nome);
        Assert.Equal(EstadoCaracteristica.Ativa.ToString(), result.Estado);

        _caracteristicaRepository.Verify(x => 
            x.BuscarPorNome(request.Nome,
                It.IsAny<CancellationToken>()), Times.Once);

        _caracteristicaRepository.Verify(x =>
            x.AddAsync(It.IsAny<Caracteristica>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
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


        _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        //Act
        var useCase = new CadastrarCaracteristicaUseCase(
            _caracteristicaRepository.Object
            , _unitOfWork.Object
            );

        var result = await Assert.ThrowsAsync<BusinessRuleException>(() => useCase.ExecuteAsync(
            request, It.IsAny<CancellationToken>()));

        //Assert
        Assert.Equal("Característica já existe.", result.Message);

        _caracteristicaRepository.Verify(x =>
            x.BuscarPorNome(request.Nome,
                It.IsAny<CancellationToken>()), Times.Once);

        _caracteristicaRepository.Verify(x =>
            x.AddAsync(It.IsAny<Caracteristica>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _unitOfWork.Verify(x => 
            x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);

    }
}
