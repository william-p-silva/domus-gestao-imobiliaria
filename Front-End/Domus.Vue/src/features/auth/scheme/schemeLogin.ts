import z, { email, uuid } from "zod";


export const SchemeLoginRequest = z.object({
    email: z.email({ message: "Email inválido" }),

    senha: z.string().min(3, { message: "Senha deve ter no mínimo 6 caracteres" })
});


export const SchemeLoginResponse = z.object({
    usuario_id: z.uuid({ message: "UUID inválido" }),

    nome: z.string({ message: "Nome inválido" }).min(3, { message: "Nome deve ter no mínimo 3 caracteres" }),

    email: z.email(),

    perfil: z.array(z.string({ message: "Perfil inválido" }).min(3, { message: "Perfil deve ter no mínimo 3 caracteres" })),

});