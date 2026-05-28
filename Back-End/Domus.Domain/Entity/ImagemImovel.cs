

namespace Domus.Domain.Entity;

public class ImagemImovel
{
    public Guid ImagemImovel_ID { get; private set; }
    public Guid Imovel_ID { get; set; }
    public string Titulo { get; private set; }
    public string UrlImagem { get; set; }

    //Relacionamentos
    public Imovel Imovel { get; private set; }

    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected ImagemImovel() { }

    public ImagemImovel(Guid imovel_id, string titulo, string urlImagem)
    {
        if (imovel_id == Guid.Empty)
            throw new ArgumentException("O ID do imóvel é obrigatório.", nameof(imovel_id));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título da imagem é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(urlImagem))
            throw new ArgumentException("A URL da imagem é obrigatória.", nameof(urlImagem));

        ImagemImovel_ID = Guid.NewGuid();
        Imovel_ID = imovel_id;
        Titulo = titulo;
        UrlImagem = urlImagem;
    }
}
