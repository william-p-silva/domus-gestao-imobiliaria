
namespace Domus.Application.DTOs.Chat.Response;

public record ResponseChat
{
    public Guid Chat_ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public List<ResponseUsuariosChat> Participantes { get; set; } = new List<ResponseUsuariosChat>();
    public List<ResponseMensagemChat> Mensagens { get; set; } = new List<ResponseMensagemChat>();
    public ResponseImovelChat imovel { get; set; } = new ResponseImovelChat();
}