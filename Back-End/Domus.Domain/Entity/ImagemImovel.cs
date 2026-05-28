

namespace Domus.Domain.Entity;

public class ImagemImovel
{
    public Guid ImagemImovel_ID { get; private set; }
    public Guid Imovel_ID { get; set; }
    public string Titulo { get; private set; }
    public string UrlImagem { get; set; }

    //Relacionamentos
    public Imovel Imovel { get; private set; }
}
