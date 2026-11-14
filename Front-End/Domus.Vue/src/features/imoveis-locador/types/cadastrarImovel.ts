import type z from "zod";
import type { cadastrarImovelSchema } from "../schemas/cadastrarImovelSchema";


export type CadastrarImovelDTO = z.infer<typeof cadastrarImovelSchema>;
