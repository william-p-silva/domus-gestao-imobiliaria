import { HttpService } from "@/core/http/httpService";
import type { RequestCadastro } from "../types/requestCadastro";


export async function Cadastrar(request: RequestCadastro){
    const httpService = new HttpService();

    const response = await httpService.postAsync<any, RequestCadastro>("Locatario/post/locatario", request);
}