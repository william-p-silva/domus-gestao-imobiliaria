

namespace Domus.Application.Interfaces.Email;

public interface IEmailService
{
    Task EnviarAsync(string destinatario, string assunto, string corpo);
}
