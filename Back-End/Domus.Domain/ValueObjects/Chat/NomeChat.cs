

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
            throw new ArgumentNullException("O nome não pode ser vazio.");

        int lenght = nome.Length;
        if (lenght <= 4)
            throw new ArgumentException("Nome muito curto.");
        if (lenght >= 151)
            throw new ArgumentException("Nome muito longo.");

        if (!Regex.IsMatch(nome, padrao))
            throw new ArgumentException("Nome inválido.");

        return new NomeChat(nome);
    }
}
