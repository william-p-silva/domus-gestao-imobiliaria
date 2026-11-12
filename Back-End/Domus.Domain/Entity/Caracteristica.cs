
using Domus.Domain.Enums.Caracteristica;
using Domus.Domain.Exceptions.Domain;
using Domus.Domain.ValueObjects.Caracteristica;

namespace Domus.Domain.Entity;

public class Caracteristica
{
    public Guid Caracteristica_ID { get; private set; }

    public NomeCaracteristica Nome { get; private set; }

    public EstadoCaracteristica Estado { get; private set; }

    public DateTime CriadoEm { get; private set; }
    public DateTime? DeletadoEm { get; private set; }

    public ICollection<CaracteristicaImovel> CaracteristicasImovel { get; private set; } = new List<CaracteristicaImovel>();

    protected Caracteristica() { }

    public Caracteristica(
        string nome
        )
    {
        Caracteristica_ID = Guid.NewGuid();
        Estado = EstadoCaracteristica.Ativa;
        CriadoEm = DateTime.UtcNow;

        Nome = NomeCaracteristica.Create(nome);
    }

    public void Ativar()
    {
        if (Estado == EstadoCaracteristica.Ativa)
            throw new ValidationException("Característica já está ativa.");

        Estado = EstadoCaracteristica.Ativa;
    }
}
