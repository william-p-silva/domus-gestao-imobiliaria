import { type ResponseLogin } from "@/features/auth/types/responseLogin";
import { defineStore } from "pinia";
import { ref } from "vue";
import { HttpService } from "../http/httpService";
import { LoginService } from "@/features/auth/services/loginService";



export const AuthenticationConfig = defineStore(('authConfig'), () => {
    const isLogged = ref<boolean>(false);
    const loginService = new LoginService(new HttpService());
    
    const userLogged = ref<ResponseLogin>({
        usuario_id: "",
        nome: "",
        email: "",
        perfil: [],
    });

    async function IsLoggedIn() {
        const result = await loginService.VerifyIsLogged();

        if(result === false){
            isLogged.value = false;
            userLogged.value = {
                usuario_id: "",
                nome: "",
                email: "",
                perfil: [],
            }
        }

        if(result !== false){
            isLogged.value = true;
            userLogged.value = result;
        }
    }

    return {
        isLogged,
        userLogged,
        IsLoggedIn
    }
});