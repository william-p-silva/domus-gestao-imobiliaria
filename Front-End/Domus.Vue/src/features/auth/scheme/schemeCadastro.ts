import z, { email } from "zod";


export const SchemeCadastro = z.object({
    nome: z.string()
    .min(3, { message: "O nome deve ter no mínimo 3 caracteres" })
    .max(150, { message: "O nome deve ter no máximo 150 caracteres" })
    .regex(/^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$/, { message: "O nome deve conter apenas letras e espaços" }),

    email: z.string().email({ message: "O email deve ser válido" }),

    senha: z.string()
    .min(8, { message: "A senha deve ter no mínimo 8 caracteres" })
    .max(30, { message: "A senha deve ter no máximo 30 caracteres" }),

})