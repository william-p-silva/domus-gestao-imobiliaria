using System.Text.RegularExpressions;

namespace Domus.Domain.ValueObjects.Usuario;

public sealed record CPF
{
    public string Numero { get; }

    private CPF(string numero)
    {
        Numero = numero;
    }

    public static CPF Create(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF é obrigatório.");

        // Remove máscara
        cpf = Regex.Replace(cpf, "[^0-9]", "");

        if (!Validar(cpf))
            throw new ArgumentException("CPF inválido.");

        return new CPF(cpf);
    }

    private static bool Validar(string cpf)
    {
        if (cpf.Length != 11)
            return false;

        // Evita CPFs como 11111111111
        if (cpf.All(c => c == cpf[0]))
            return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf[..9];

        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += (tempCpf[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        int digito = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += (tempCpf[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        digito = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito.ToString());
    }

    public override string ToString()
    {
        return Convert.ToUInt64(Numero)
            .ToString(@"000\.000\.000\-00");
    }
}
