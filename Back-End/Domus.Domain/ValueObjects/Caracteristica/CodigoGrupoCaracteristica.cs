
using Domus.Domain.Exceptions.Domain;
using System.Text.RegularExpressions;

namespace Domus.Domain.ValueObjects.Caracteristica;

public sealed record CodigoGrupoCaracteristica
{
    private static readonly string _padrao = @"^[A-Z0-9_]+$";

    public string Value { get; private set; } = string.Empty;

    private CodigoGrupoCaracteristica(string value)
    {
        Value = value;
    }

    private CodigoGrupoCaracteristica() { }

    public static CodigoGrupoCaracteristica Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("Código não pode ser vazio.");

        value = Normalizar(value);

        if (value.Length > 50)
            throw new ValidationException(
                "Código não pode ultrapassar 50 caracteres.");

        if (!Regex.IsMatch(value, _padrao))
            throw new ValidationException("Código inválido.");

        return new CodigoGrupoCaracteristica(value);
    }

    private static string Normalizar(string value)
    {
        value = value.Trim().ToUpperInvariant();

        value = Regex.Replace(value, @"\s+", "_");

        return value;
    }
}
