import z from "zod";


export const FiltroImovelSchema = z.object({
    endereco: z.string().optional(),

    tipoImovel: z.string().optional(),

    faixaPreco: z
    .tuple([z.number().min(0), z.number().min(0)])
    .refine(([min, max]) => min <= max, {
      message: "O preço mínimo não pode ser maior que o preço máximo",
    })
    .optional(),

    quartos: z.number().optional(),

    banheiros: z.number().optional(),

    areaM2: z
    .tuple([z.number().min(0).optional(), z.number().min(0).optional()])
    .refine(([de, ate]) => !de || !ate || de <= ate, {
      message: "A área mínima não pode ser maior que a área máxima",
    })
    .optional(),
});

export type FiltroImovelType = z.infer<typeof FiltroImovelSchema>;