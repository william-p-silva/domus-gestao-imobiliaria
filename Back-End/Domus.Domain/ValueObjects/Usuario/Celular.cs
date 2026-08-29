using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domus.Domain.ValueObjects.Usuario;

public sealed record Celular
{
    public string Numero { get; }

    private Celular(string celular) {  Numero = celular; }

    public static Celular Create(string celular)
    {
        if (string.IsNullOrWhiteSpace(celular)) 
            throw new ArgumentException("Número de celular inválido");

        celular = Regex.Replace(celular, "[^0-9]", "");

        if (!Validate(celular)) throw new ArgumentException("Número de celular inválido");

        return new Celular(celular);

    }

    public static bool Validate(string numero)
    {
        if (numero.Length != 11) return false;

        if(numero.All(n => n == numero[0])) return false;

        if (numero[2] != 9) return false;

        return true;
    }

    public override string ToString()
    {
        return $"({Numero[..2]}) {Numero.Substring(2, 5)}-{Numero.Substring(7)}";
    }
}
