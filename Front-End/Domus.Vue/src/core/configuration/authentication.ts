import { type ResponseLogin } from "@/features/auth/types/responseLogin";
import { defineStore } from "pinia";
import { ref } from "vue";
import { AuthService } from "./authService";

const DEFAULT_USER: ResponseLogin = {
    usuario_id: "",
    nome: "",
    email: "",
    perfil: [],
}

const authService = new AuthService();


export const useAuthStore = defineStore(('useAuthStore'), () => {
    const isLogged = ref<boolean>(false);
    const isCheckingAuth = ref<boolean>(true);

    const userLogged = ref<ResponseLogin>(DEFAULT_USER);

    async function checkAuth(): Promise<boolean> {
        isCheckingAuth.value = true;
        try{
            const result = await authService.VerifyIsLogged();

            if (result) {
                setUserLogged(result);
            } else {
                logout();
            }
    
            return isLogged.value;
        }
        catch {
            return false;
        }
        finally{
            isCheckingAuth.value = false;
        }

    }

    function setUserLogged(user: ResponseLogin) {
        isLogged.value = true;
        userLogged.value = user;
    }

    async function logout() {
        isLogged.value = false;
        userLogged.value = { ...DEFAULT_USER };
    }

    return {
        isLogged,
        userLogged,
        checkAuth,
        setUserLogged,
        logout
    }
});