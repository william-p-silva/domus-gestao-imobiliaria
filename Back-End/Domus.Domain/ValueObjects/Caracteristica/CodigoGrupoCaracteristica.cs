
using Domus.Domain.Exceptions.Domain;
using System.Globalization;
using System.Text;
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

        // Decompõe os caracteres acentuados.
        value = value.Normalize(NormalizationForm.FormD);

        // Remove os sinais diacríticos.
        var caracteres = value
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                != UnicodeCategory.NonSpacingMark);

        value = new string(caracteres.ToArray());

        // Retorna à forma Unicode composta.
        value = value.Normalize(NormalizationForm.FormC);

        // Substitui espaços por underline.
        value = Regex.Replace(value, @"\s+", "_");

        return value;
    }
}
