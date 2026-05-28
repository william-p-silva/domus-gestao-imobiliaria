

namespace Domus.Domain.Entity;

public class Chat
{
    public Guid Chat_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }

    //Relacionamentos
    public Imovel Imovel { get; private set; }
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();
    public ICollection<UsuarioChat> UsuarioChats { get; private set; } = new List<UsuarioChat>();


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Chat() { }

    public Chat( Guid imovel_id)
    {
        if (imovel_id == Guid.Empty)
            throw new ArgumentException("O ID do imóvel é obrigatório.", nameof(imovel_id));

        Chat_ID = Guid.NewGuid();
        Imovel_ID = imovel_id;
    }
}
