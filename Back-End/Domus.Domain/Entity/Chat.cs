

using Domus.Domain.Enums.Chat;
using Domus.Domain.Exceptions.Domain;

namespace Domus.Domain.Entity;

public class Chat
{
    public Guid Chat_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }
    public string Nome { get; private set; }
    public EstadoChat Estado { get; private set; }
    public DateTime? DeletadoEm { get; private set; }
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    //Relacionamentos
    public Imovel Imovel { get; private set; }
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();
    public ICollection<UsuarioChat> UsuarioChats { get; private set; } = new List<UsuarioChat>();


    /// <summary>
    /// Construtor privado/protegido exigido pelo Entity Framework Core.
    /// O EF usa este construtor e preenche as propriedades via reflection.
    /// </summary>
    protected Chat() { }

    public Chat(Imovel imovel)
    {
        if (imovel is null)
            throw new NotFoundException("Imóvel não encontrado.");

        Chat_ID = Guid.NewGuid();
        Imovel_ID = imovel.Imovel_ID;
        Nome = imovel.Titulo;
        Estado = EstadoChat.Ativo;
    }
}
