
using System.ComponentModel.DataAnnotations;

namespace Domus.Domain.ValueObjects;

public sealed record Email
{
    public string Endereco { get; }

    private Email(string endereco) { Endereco = endereco; }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

        var validator = new EmailAddressAttribute();
        if (validator.IsValid(email)) throw new ArgumentException("Email inválido.");

        return new Email(email);
    }
}
