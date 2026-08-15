import { handleError, ref } from "vue";
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

    const resetPassword = ref<string>('');

    async function handleResetPassword() {
        isLoading.value = true;
        console.log(resetPassword.value);
        isLoading.value = false;
    }

    async function handleLogin() : Promise<boolean>{
        isLoading.value = true;
        errorLogin.value = "";        

        const result = await loginService.login(requestLogin.value);

        if (result !== null && result.perfil.length > 0) {
            router.push({ name: result.perfil[0] });
        } else {
            errorLogin.value = "Falha ao realizar login. Verifique suas credenciais.";
        }

        isLoading.value = false;

        return true;
    };

    return {
        requestLogin,
        errorLogin,
        isLoading,
        router,
        resetPassword,
        handleResetPassword,
        handleLogin
    }

}