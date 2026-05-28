

namespace Domus.Domain.Entity;

public class Chat
{
    public Guid Chat_ID { get; private set; }
    public Guid Imovel_ID { get; private set; }

    //Relacionamentos
    public Imovel Imovel { get; private set; }
    public ICollection<MensagemChat> MensagensChat { get; private set; } = new List<MensagemChat>();
    public ICollection<UsuarioChat> UsuarioChats { get; private set; } = new List<UsuarioChat>();
}
