import { ref } from "vue";
import { type RequestLogin } from "../types/requestLogin";
import { LoginService } from "../services/loginService";
import { useRouter } from "vue-router";




export const useLogin = () => {
    const loginService = new LoginService();
    const router = useRouter();

    const errorLogin = ref<string>("");
    const isLoading = ref<boolean>(false);

    const requestLogin = ref<RequestLogin>({
        email: "",
        senha: ""
    });

    async function handleLogin() : Promise<boolean>{
        isLoading.value = true;
        errorLogin.value = "";        

        const result = await loginService.login(requestLogin.value);

        if (result) {
            router.push({ name: "home" });
        } else {
            errorLogin.value = "Falha ao realizar login. Verifique suas credenciais.";
        }

        isLoading.value = false;

        return result;
    };

    return {
        requestLogin,
        errorLogin,
        isLoading,
        router,
        handleLogin
    }

}