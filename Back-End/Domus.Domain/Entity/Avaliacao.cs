
namespace Domus.Domain.Entity;

public class Avaliacao
{
    public Guid Avaliacao_ID { get; private set; }
    public Guid Usuario_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public Guid Contrato_ID { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public int Nota { get; private set; }
    public DateTime PublicadoEm { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Usuario Usuario { get; private set; }
    public Imovel Imovel { get; private set; }
    public Contrato Contrato { get; private set; }


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Avaliacao() { }

    public Avaliacao
        ( Guid usuario_id, Guid imovel_id, Guid contrato_id, 
          string titulo, string descricao, int nota )
    {
        if (usuario_id == Guid.Empty)
            throw new ArgumentException("O ID do usuário é obrigatório.", nameof(usuario_id));
        if (imovel_id == Guid.Empty)    
            throw new ArgumentException("O ID do imóvel é obrigatório.", nameof(imovel_id));
        if (contrato_id == Guid.Empty)    
            throw new ArgumentException("O ID do contrato é obrigatório.", nameof(contrato_id));
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título da avaliação é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(descricao)) 
            throw new ArgumentException("A descrição da avaliação é obrigatória.", nameof(descricao));
        if (nota < 1 || nota > 5)
            throw new ArgumentOutOfRangeException(nameof(nota), "A nota deve ser entre 1 e 5.");

        Avaliacao_ID = Guid.NewGuid();
        Usuario_ID = usuario_id;
        Imovel_ID = imovel_id;
        Contrato_ID = contrato_id;
        Titulo = titulo;
        Descricao = descricao;
        Nota = nota;
    }


}
