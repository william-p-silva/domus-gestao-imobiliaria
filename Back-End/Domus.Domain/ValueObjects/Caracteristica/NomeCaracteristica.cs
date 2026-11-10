

using Domus.Domain.Exceptions.Domain;
using System.Text.RegularExpressions;

namespace Domus.Domain.ValueObjects.Caracteristica;

public sealed record NomeCaracteristica
{
    private static readonly string padrao = @"^[a-zA-ZÀ-ÿ\s'-]+$";

    public string Nome { get; private set; }

    private NomeCaracteristica(string nome) { Nome = nome; }

    protected NomeCaracteristica() { }

    public static NomeCaracteristica Create(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ValidationException("O nome de uma característica não pode ser vazio");
        if (!Regex.IsMatch(nome, padrao))
            throw new ValidationException("Nome característica inválido");

        int lenght = nome.Length;
        if (lenght <= 2)
            throw new ValidationException("Nome da característica muito curto");
        if (lenght > 70)
            throw new ValidationException("Nome da característica muito longo");

        return new NomeCaracteristica(nome);
    }
}
