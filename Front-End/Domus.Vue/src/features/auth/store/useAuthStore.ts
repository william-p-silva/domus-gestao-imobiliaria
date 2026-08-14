import { defineStore } from "pinia";
import { ref } from "vue"

export const authStore = defineStore(('useAuth'), () => {
    const form = ref<'login' | 'cadastro' >('login');
    
    function showLogin() {
        form.value = 'login';
    }
    
    function showCadastro() {
        form.value = 'cadastro';
    }

    return{
        form,
        showCadastro,
        showLogin
    }
})