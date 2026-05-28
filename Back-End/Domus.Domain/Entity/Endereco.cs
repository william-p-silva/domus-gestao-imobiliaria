
namespace Domus.Domain.Entity;

public class Endereco
{
    public Guid Endereco_ID { get; private set; }
    public string CEP { get; private set; }
    public string UF { get; private set; }
    public string Cidade { get; private set; }
    public string Bairro { get; private set; }
    public string Rua { get; private set; }
    public string Numero { get; private set; }
    public string? Complemento { get; private set; }

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Endereco() { }

    public Endereco 
        ( string cep, string uf, string cidade, string bairro,
          string rua, string numero, string? complemento)
    {
        if (string.IsNullOrWhiteSpace(cep))
            throw new ArgumentException("O CEP é obrigatório.", nameof(cep));
        if (string.IsNullOrWhiteSpace(uf))
            throw new ArgumentException("A UF é obrigatória.", nameof(uf));
        if (string.IsNullOrWhiteSpace(cidade))
            throw new ArgumentException("A cidade é obrigatória.", nameof(cidade));
        if (string.IsNullOrWhiteSpace(bairro))
            throw new ArgumentException("O bairro é obrigatório.", nameof(bairro));
        if (string.IsNullOrWhiteSpace(rua))
            throw new ArgumentException("A rua é obrigatória.", nameof(rua));
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("O número é obrigatório.", nameof(numero));

        Endereco_ID = Guid.NewGuid();
        CEP = cep;
        UF = uf;
        Cidade = cidade;
        Bairro = bairro;
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
    }
}