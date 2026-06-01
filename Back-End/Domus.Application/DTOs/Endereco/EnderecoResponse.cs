

namespace Domus.Application.DTOs.Endereco;

public class EnderecoResponse
{
    public Guid Endereco_ID { get; set; }
    public string CEP { get; set; }
    public string UF { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string? Complemento { get; set; }
}
