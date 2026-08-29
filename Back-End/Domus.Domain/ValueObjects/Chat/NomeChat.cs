

using Domus.Domain.Exceptions.Domain;
using System.Text.RegularExpressions;

namespace Domus.Domain.ValueObjects.Chat;

public sealed record NomeChat
{
    private static readonly string padrao = @"^[a-zA-ZÀ-ÿ\s'-]+$";

    public string Nome { get; private set; }

    private NomeChat(string nome) { Nome = nome; }

    protected NomeChat() { }

    public static NomeChat Create(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ValidationException("O nome não pode ser vazio.");

        int lenght = nome.Length;
        if (lenght <= 4)
            throw new ValidationException("Nome muito curto.");
        if (lenght >= 151)
            throw new ValidationException("Nome muito longo.");

        if (!Regex.IsMatch(nome, padrao))
            throw new ValidationException("Nome inválido.");

        return new NomeChat(nome);
    }
}
