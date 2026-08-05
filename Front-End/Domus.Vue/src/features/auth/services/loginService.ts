import { HttpService } from "@/core/http/httpService";
import type { ResponseLogin } from "../types/responseLogin";
import type { RequestLogin } from "../types/requestLogin";
import { useAuthStore } from "@/core/configuration/authentication";


export class LoginService {

    private readonly httpService: HttpService = new HttpService();
    private readonly authStore = useAuthStore();

    public async login(requestLogin: RequestLogin) : Promise<boolean>{
        
        try{
            const response = await this.httpService.PostAsync<ResponseLogin, RequestLogin>(
                "auth/login", requestLogin);

            if(response == null || response.usuario_id == "") return false;
            
            const result = await this.authStore.setUserLogged(response);
            
            return true;
        }
        catch(error){
            return false;
        }
    }
}