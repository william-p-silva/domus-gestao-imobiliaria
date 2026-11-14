import type { enderecoSchema } from "@/shared/schemas/endereco/enderecoScema";
import type z from "zod";


export interface EnderecoResponse {
    endereco_id: string,
    cep: string,
    uf: string,
    cidade: string,
    bairro: string,
    rua: string,
    numero: string,
    complemento?: string
}

export type EnderecoDTO = z.infer<typeof enderecoSchema>;