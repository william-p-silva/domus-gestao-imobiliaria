

using Domus.Domain.Exceptions.Domain;
using System.Text.RegularExpressions;

namespace Domus.Domain.ValueObjects.Usuario;

public sealed record Nome
{
    private static readonly string padrao = @"^[a-zA-ZÀ-ÿ\s'-]+$";
    public string NomeCompleto{ get; private set; }

    private Nome(string nome) { NomeCompleto = nome; }

    protected Nome() {  }

    public static Nome Create(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ValidationException("O nome não pode ser vazio.");
        if (!Regex.IsMatch(nome, padrao))
            throw new ValidationException("Nome inválido.");

        int lenght = nome.Length;
        if (lenght <= 4)
            throw new ValidationException("Nome muito curto.");
        if (lenght > 150)
            throw new ValidationException("Nome muito longo.");

        return new Nome(nome);
    }
}
