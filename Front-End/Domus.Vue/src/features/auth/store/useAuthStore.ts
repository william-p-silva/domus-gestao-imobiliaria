import { defineStore } from "pinia";
import { ref, watch } from "vue"
import { useRoute, useRouter } from "vue-router";

export const authStore = defineStore(('useAuth'), () => {
    const route = useRoute();
    const router = useRouter();
    const form = ref<'login' | 'cadastro' >(route.name === 'Cadastro' ? 'cadastro' : 'login');
    const isReset = ref<boolean>(false);

    watch(
        () => route.name,
        (routeName) => {
            if (routeName === "Cadastro") {
                form.value = "cadastro";
            } else if (routeName === "Login") {
                form.value = "login";
            }
        }
    );
    
    function showLogin() {
        form.value = 'login';
        isReset.value = false;
    }
    
    function showCadastro() {
        form.value = 'cadastro';
        isReset.value = false;
    }

    function clear(){
        isReset.value = false;
    }

    return{
        form,
        isReset,
        router,
        showCadastro,
        clear,
        showLogin
    }
})