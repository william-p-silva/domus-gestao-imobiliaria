import type z from "zod";
import type { SchemeLoginResponse } from "../scheme/schemeLogin";



export type ResponseLogin = z.infer<typeof SchemeLoginResponse>;