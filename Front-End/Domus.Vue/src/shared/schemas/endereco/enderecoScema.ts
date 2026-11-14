import z from "zod";

export const enderecoSchema = z.object({
    cep: z.string()
        .length(9, "O CEP deve possuir 8 números")
        .regex(/^\d{5}-\d{3}$/, "CEP inválido"),

    uf: z.string()
        .length(2, "A UF deve possuir 2 caracteres")
        .regex(/^[A-Za-z]{2}$/, "UF inválida"),

    cidade: z.string()
        .min(2, "Informe uma cidade"),

    bairro: z.string()
        .min(2, "Informe um bairro"),

    rua: z.string()
        .min(2, "Informe uma rua"),

    numero: z.string()
        .min(1, "Informe o número do imóvel"),

    complemento: z.string().nullable().optional(),
});

