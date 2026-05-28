
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
    public string Complemento { get; private set; }
}