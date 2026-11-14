
import { enderecoSchema } from "@/shared/schemas/endereco/enderecoScema";
import z from "zod";

const statusImovelEnum = z.enum([
    "Alugado",
    "Disponivel",
    "Indisponivel",
    "Excluido",
]);

const tipoImovelEnum = z.literal([0, 1, 2, 3, 4]);

export const cadastrarImovelSchema = z.object({
    titulo: z.string()
        .min(5, "O título deve ter ao menos 5 caracteres")
        .max(200, "O título deve ter no máximo 200 caracteres"),

    descricao: z.string()
        .min(10, "A descrição deve ter no mínimo 10 caracteres")
        .max(1000, "A descrição deve ter no máximo 1000 caracteres"),

    comodos: z.int()
        .min(1, "Seu imóvel deve ter ao menos um cômodo")
        .max(200, "Seu imóvel não pode ter mais de 200 cômodos"),

    banheiros: z.int()
        .min(1, "Seu imóvel deve ter ao menos um banheiro")
        .max(200, "Seu imóvel não pode ter mais de 200 banheiros"),

    metrosQuadrados: z.number()
        .positive("A metragem deve ser maior que zero"),

    status: statusImovelEnum,

    valorAluguel: z.number()
        .positive("O valor do aluguel deve ser maior que zero"),

    tipoDoImovel: tipoImovelEnum,

    caracteristicas: z.array(
        z.uuid("Característica deve possuir um GUID válido")
    ),

    endereco: enderecoSchema,
});

