

using Domus.Domain.Exceptions.Domain;

namespace Domus.Domain.Entity;

public class CaracteristicaImovel
{
    public Guid CaracteristicaImovel_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public Guid Caracteristica_ID { get; private set; }

    public DateTime CriadoEm { get; private set; }
    public DateTime? DeletadoEm { get; private set; }

    public Caracteristica Caracteristica { get; private set; }
    public Imovel Imovel { get; private set; }

    protected CaracteristicaImovel() { }

    public CaracteristicaImovel(
        Caracteristica caracteristica
        ,Imovel imovel
        )
    {
        if (imovel is null)
            throw new ValidationException("Imóvel não pode ser nulo");
        if (caracteristica is null)
            throw new ValidationException("Característica não pode ser nula");

        if (imovel.Imovel_ID == Guid.Empty)
            throw new ValidationException("Id do imóvel inválido");
        if (caracteristica.Caracteristica_ID == Guid.Empty)
            throw new ValidationException("Id da característica inválido");

        CaracteristicaImovel_ID = Guid.NewGuid();
        Imovel_ID = imovel.Imovel_ID;
        Caracteristica_ID = caracteristica.Caracteristica_ID;
        CriadoEm = DateTime.UtcNow;

        Caracteristica = caracteristica;
        Imovel = imovel;
    }
}
