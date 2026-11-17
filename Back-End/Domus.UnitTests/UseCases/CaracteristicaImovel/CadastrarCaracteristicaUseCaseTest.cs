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
    private readonly Mock<ICaracteristicaRepository> _caracteristicaRepository = new();
    private readonly Mock<IGrupoCaracteristicaRepository> _grupoCaracteristicaRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private CadastrarCaracteristicaUseCase CriarUseCase() =>
        new(
            _caracteristicaRepository.Object,
            _grupoCaracteristicaRepository.Object,
            _unitOfWork.Object
        );

    [Fact]
    public async Task DeveRetornarSucesso_QuandoDadosValidos()
    {
        // Arrange
        var request = new CadastrarCaracteristicaRequest
        {
            Nome = "Teste",
            GrupoCaracteristica_ID = Guid.NewGuid()
        };

        var grupoMock = new GrupoCaracteristica("Grupo Teste", "Descricao");

        _caracteristicaRepository
            .Setup(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Caracteristica?)null);

        _grupoCaracteristicaRepository
            .Setup(x => x.BuscarPorId(request.GrupoCaracteristica_ID, EstadoCaracteristica.Ativa, It.IsAny<CancellationToken>()))
            .ReturnsAsync(grupoMock);

        _caracteristicaRepository
            .Setup(x => x.AddAsync(It.IsAny<Caracteristica>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWork
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        // Act
        var result = await useCase.ExecuteAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Nome, result.Nome);
        Assert.Equal(EstadoCaracteristica.Ativa.ToString(), result.Estado);

        _caracteristicaRepository.Verify(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()), Times.Once);
        _grupoCaracteristicaRepository.Verify(x => x.BuscarPorId(request.GrupoCaracteristica_ID, EstadoCaracteristica.Ativa, It.IsAny<CancellationToken>()), Times.Once);
        _caracteristicaRepository.Verify(x => x.AddAsync(It.IsAny<Caracteristica>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeveLancarException_QuandoCaracteristicaJaExistirEAtiva()
    {
        // Arrange
        var request = new CadastrarCaracteristicaRequest
        {
            Nome = "Teste",
            GrupoCaracteristica_ID = Guid.NewGuid()
        };

        var grupoMock = new GrupoCaracteristica("Grupo Teste", "Descricao");
        var caracteristicaExistente = new Caracteristica(request.Nome, grupoMock);
        // Garantindo que está Ativa (por padrão pode vir ativa ou podemos ajustar se houver método, assumindo o estado inicial)

        _caracteristicaRepository
            .Setup(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()))
            .ReturnsAsync(caracteristicaExistente);

        var useCase = CriarUseCase();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessRuleException>(() =>
            useCase.ExecuteAsync(request, CancellationToken.None));

        Assert.Equal("Característica já cadastrada.", exception.Message);

        _caracteristicaRepository.Verify(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()), Times.Once);
        _caracteristicaRepository.Verify(x => x.AddAsync(It.IsAny<Caracteristica>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeveAtivarCaracteristica_QuandoExistirMasEstiverInativa()
    {
        // Arrange
        var request = new CadastrarCaracteristicaRequest
        {
            Nome = "Teste",
            GrupoCaracteristica_ID = Guid.NewGuid()
        };

        var grupoMock = new GrupoCaracteristica("Grupo Teste", "Descricao");
        var caracteristicaInativa = new Caracteristica(request.Nome, grupoMock);

        // Simulando que a característica está inativa (ajuste o método/propriedade conforme sua entidade)
        caracteristicaInativa.Inativar(); // Supondo que exista esse método ou similar na entidade

        _caracteristicaRepository
            .Setup(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()))
            .ReturnsAsync(caracteristicaInativa);

        _unitOfWork
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        // Act
        var result = await useCase.ExecuteAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(EstadoCaracteristica.Ativa.ToString(), result.Estado);

        _caracteristicaRepository.Verify(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _caracteristicaRepository.Verify(x => x.AddAsync(It.IsAny<Caracteristica>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeveLancarNotFoundException_QuandoGrupoCaracteristicaNaoForEncontrado()
    {
        // Arrange
        var request = new CadastrarCaracteristicaRequest
        {
            Nome = "Teste",
            GrupoCaracteristica_ID = Guid.NewGuid()
        };

        _caracteristicaRepository
            .Setup(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Caracteristica?)null);

        _grupoCaracteristicaRepository
            .Setup(x => x.BuscarPorId(request.GrupoCaracteristica_ID, EstadoCaracteristica.Ativa, It.IsAny<CancellationToken>()))
            .ReturnsAsync((GrupoCaracteristica?)null);

        var useCase = CriarUseCase();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            useCase.ExecuteAsync(request, CancellationToken.None));

        Assert.Equal("Grupo de característica não encontrado.", exception.Message);

        _caracteristicaRepository.Verify(x => x.BuscarPorNome(request.Nome, It.IsAny<CancellationToken>()), Times.Once);
        _grupoCaracteristicaRepository.Verify(x => x.BuscarPorId(request.GrupoCaracteristica_ID, EstadoCaracteristica.Ativa, It.IsAny<CancellationToken>()), Times.Once);
        _caracteristicaRepository.Verify(x => x.AddAsync(It.IsAny<Caracteristica>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}