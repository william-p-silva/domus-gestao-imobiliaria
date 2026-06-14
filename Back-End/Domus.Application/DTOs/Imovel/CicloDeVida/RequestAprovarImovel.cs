

namespace Domus.Application.DTOs.Imovel.CicloDeVida;

public class RequestAprovarImovel
{
    public bool Aprovado { get; set; }
    public Guid Imovel_ID { get; set; }
}
