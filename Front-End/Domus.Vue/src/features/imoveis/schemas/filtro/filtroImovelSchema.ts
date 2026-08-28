import z from "zod";


export const FiltroImovelSchema = z.object({
    endereco: z.string().optional(),

    tipoImovel: z.string().optional(),

    minPreco: z.number().optional(),

    maxPreco: z.number().optional(),

    comodos: z.number().optional(),

    banheiros: z.number().optional(),

    minArea: z.number().optional(),

    maxArea: z.number().optional(),
});

export type FiltroImovelType = z.infer<typeof FiltroImovelSchema>;