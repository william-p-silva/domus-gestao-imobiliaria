

using Domus.Application.Interfaces.Email;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace Domus.Infrastructure.Data.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    public EmailService (IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task EnviarAsync(string destinatario, string assunto, string corpo)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(destinatario));
        email.Subject = assunto;
        email.Body = new TextPart("html") { Text = corpo };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _settings.SmtpHost,
            _settings.SmtpPort,
            SecureSocketOptions.StartTls
        );

        await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.SenderPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
