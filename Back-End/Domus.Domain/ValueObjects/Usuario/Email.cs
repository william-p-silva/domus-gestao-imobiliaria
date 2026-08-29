
using System.ComponentModel.DataAnnotations;

namespace Domus.Domain.ValueObjects.Usuario;

public sealed record Email
{
    public string Endereco { get; }

    private Email(string endereco) { Endereco = endereco; }

    protected Email() { }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new Exceptions.Domain.ValidationException("O E-mail não pode ser vazio.");

        var validator = new EmailAddressAttribute();
        if (validator.IsValid(email)) throw new Exceptions.Domain.ValidationException("Email inválido.");

        return new Email(email);
    }
}
