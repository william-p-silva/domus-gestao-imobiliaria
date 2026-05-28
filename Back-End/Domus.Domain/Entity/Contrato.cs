

using Domus.Domain.Enums;

namespace Domus.Domain.Entity;

public class Contrato
{
    public Guid Contrato_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public Guid Locador_ID { get; private set; }
    public Guid? Locatario_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Tipo { get; private set; }
    public string UrlContrato { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataTermino { get; private set; }
    public StatusContrato Status { get; private set; }

    //Relacionamentos
    public List<ParcelaAluguel> ParcelasAluguel { get; private set; }
    public Imovel Imovel { get; private set; }
    public Usuario Locador { get; private set; }
    public Usuario Locatario { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Contrato() { }

    public Contrato
        ( Guid imovel_id, Guid locador_id, string titulo, string descricao, 
          string tipo, string urlContrato )
    {
        if (imovel_id == Guid.Empty)
            throw new ArgumentException("O ID do imóvel é obrigatório.", nameof(imovel_id)); 
        if (locador_id == Guid.Empty)
            throw new ArgumentException("O ID do locador é obrigatório.", nameof(locador_id));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do contrato é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição do contrato é obrigatória.", nameof(descricao));
        if (string.IsNullOrWhiteSpace(tipo))
            throw new ArgumentException("O tipo do contrato é obrigatório.", nameof(tipo));
        if (string.IsNullOrWhiteSpace(urlContrato))
            throw new ArgumentException("A URL do contrato é obrigatória.", nameof(urlContrato));

        Contrato_ID = Guid.NewGuid();
        Imovel_ID = imovel_id;
        Locador_ID = locador_id;
        Titulo = titulo;
        Descricao = descricao;
        Tipo = tipo;
        UrlContrato = urlContrato;
        Status = StatusContrato.Rascunho;
    }
}
