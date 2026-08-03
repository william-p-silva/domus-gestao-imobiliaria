import type z from "zod";
import { SchemeCadastro } from "../scheme/schemeCadastro";


export type RequestCadastro = z.infer<typeof SchemeCadastro>;