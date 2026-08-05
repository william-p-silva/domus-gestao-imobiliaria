import type { HttpService } from "@/core/http/httpService";
import type { ResponseLogin } from "../types/responseLogin";


export class LoginService {

    private readonly httpService: HttpService;

    constructor(httpService: HttpService) {
        this.httpService = httpService;
    }

    public async VerifyIsLogged(): Promise<ResponseLogin | false> {
        try{
            const response = await this.httpService.PostAsync<ResponseLogin, undefined>(
                "auth/me", undefined);

            if(response == null || response.usuario_id == "") return false;
            
            return response;
        }
        catch(error){
            return false
        }
    }
}