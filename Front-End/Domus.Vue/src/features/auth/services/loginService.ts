import { HttpService } from "@/core/http/httpService";
import type { ResponseLogin } from "../types/responseLogin";
import type { RequestLogin } from "../types/requestLogin";
import { useAuthStore } from "@/core/configuration/authentication";

const DEFAUT_USER: ResponseLogin = {
    email: "",
    perfil: [],
    nome: "",
    usuario_id: ""
} 
export class LoginService {

    private readonly httpService: HttpService = new HttpService();
    private readonly authStore = useAuthStore();
    

    public async login(requestLogin: RequestLogin) : Promise<ResponseLogin>{
        
        try{
            const response = await this.httpService.PostAsync<ResponseLogin, RequestLogin>(
                "auth/login", requestLogin);

            if(response == null || response.usuario_id == "") throw Error("Sem dados retornados.");
            
            await this.authStore.setUserLogged(response);
            
            return response;
        }
        catch(error){
            return DEFAUT_USER;
        }
    }
}