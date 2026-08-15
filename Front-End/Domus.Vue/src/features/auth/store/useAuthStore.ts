import { defineStore } from "pinia";
import { ref } from "vue"
import { useRoute, useRouter } from "vue-router";

export const authStore = defineStore(('useAuth'), () => {
    const route = useRoute();
    const router = useRouter();
    const form = ref<'login' | 'cadastro' >(route.name === 'Cadastro' ? 'cadastro' : 'login');
    const isReset = ref<boolean>(false);
    
    function showLogin() {
        form.value = 'login';
        isReset.value = false;
    }
    
    function showCadastro() {
        form.value = 'cadastro';
        isReset.value = false;
    }

    return{
        form,
        isReset,
        router,
        showCadastro,
        showLogin
    }
})