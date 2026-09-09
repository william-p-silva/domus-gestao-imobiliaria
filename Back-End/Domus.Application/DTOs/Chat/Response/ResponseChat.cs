
namespace Domus.Application.DTOs.Chat.Response;

public record ResponseChat
{
    public Guid Chat_ID { get; set; }
    public Guid Imovel_ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public List<ResponseUsuariosChat> Participantes { get; set; } = new List<ResponseUsuariosChat>();
    public List<EnviarMensagemResponse> Mensagens { get; set; } = new List<EnviarMensagemResponse>();
}
