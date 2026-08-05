import type z from "zod";
import type { SchemeLoginRequest } from "../scheme/schemeLogin";


export type RequestLogin = z.infer<typeof SchemeLoginRequest>;