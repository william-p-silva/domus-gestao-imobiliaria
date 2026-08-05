import type { ResponseLogin } from "@/features/auth/types/responseLogin";
import { HttpService } from "../http/httpService";




export class AuthService {
        private readonly _httpService: HttpService = new HttpService();
    
        public async VerifyIsLogged(): Promise<ResponseLogin | false> {
            try{
                const response = await this._httpService.PostAsync<ResponseLogin, undefined>(
                    "auth/me", undefined);
    
                if(response == null || response.usuario_id == "") return false;
                
                return response;
            }
            catch(error){
                return false
            }
        }
}