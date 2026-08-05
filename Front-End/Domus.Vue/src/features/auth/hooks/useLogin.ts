import { ref } from "vue";
import { type RequestLogin } from "../types/requestLogin";
import { LoginService } from "../services/loginService";
import { HttpService } from "@/core/http/httpService";
import { AuthenticationConfig } from "@/core/configuration/authentication";
import { useRouter } from "vue-router";




export const useLogin = () => {
    const loginService = new LoginService(new HttpService());
    const auth = AuthenticationConfig();
    const router = useRouter();

    const errorLogin = ref<string>("");
    const isLoading = ref<boolean>(false);

    const requestLogin = ref<RequestLogin>({
        email: "",
        senha: ""
    });

    if(auth.isLogged){
        router.push("/home");
    }

}