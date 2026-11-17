
using Domus.Domain.Enums.Caracteristica;
using Domus.Domain.Exceptions.Domain;
using Domus.Domain.ValueObjects.Caracteristica;

namespace Domus.Domain.Entity;

public class Caracteristica
{
    public Guid Caracteristica_ID { get; private set; }
    public Guid GrupoCaracteristica_ID { get; private set; }

    public NomeCaracteristica Nome { get; private set; }

    public EstadoCaracteristica Estado { get; private set; }

    public DateTime CriadoEm { get; private set; }
    public DateTime? DeletadoEm { get; private set; }

    public ICollection<CaracteristicaImovel> CaracteristicasImovel { get; private set; } = new List<CaracteristicaImovel>();

    public GrupoCaracteristica GrupoCaracteristica { get; private set; }

    protected Caracteristica() { }

    public Caracteristica(
        string nome,
        GrupoCaracteristica grupoCaracteristica
        )
    {
        if (grupoCaracteristica is null)
            throw new ValidationException("Uma característica precisa pertencer a um grupo.");
        if (grupoCaracteristica.GrupoCaracteristica_ID == Guid.Empty)
            throw new ValidationException("Grupo de característica inválido.");

        Nome = NomeCaracteristica.Create(nome);

        GrupoCaracteristica_ID = grupoCaracteristica.GrupoCaracteristica_ID;
        GrupoCaracteristica = grupoCaracteristica;

        Caracteristica_ID = Guid.NewGuid();
        Estado = EstadoCaracteristica.Ativa;
        CriadoEm = DateTime.UtcNow;
    }

    public void Ativar()
    {
        if (Estado == EstadoCaracteristica.Ativa)
            throw new ValidationException("Característica já está ativa.");

        Estado = EstadoCaracteristica.Ativa;
    }

    public void Inativar()
    {
        Estado = EstadoCaracteristica.Inativa;
    }
}
