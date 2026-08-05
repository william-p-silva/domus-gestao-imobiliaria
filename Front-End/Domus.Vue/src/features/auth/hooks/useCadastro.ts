import { ref } from "vue";
import type { RequestCadastro } from "../types/requestCadastro";
import { SchemeCadastro } from "../scheme/schemeCadastro";
import { Cadastrar } from "../services/cadastroService";


export const useCadastro = () => {

    const errorCadastro = ref("");
    const isLoading = ref(false);

    const requestCadastro = ref<RequestCadastro>(
        {
            email: "",
            nome: "",
            senha: "",
        }
    );

    async function handleSubmit(): Promise<boolean> {
        isLoading.value = true;
        errorCadastro.value = "";

        await submitCadastro();

        return true;
    }

    function verifyRequestCadastro(): boolean {

        const result = SchemeCadastro.safeParse(requestCadastro.value);

        if (!result.success) {
            errorCadastro.value = result.error.issues[0]!.message;
            return false;
        }

        return true;
    }

    async function submitCadastro(): Promise<boolean> {
        if (!verifyRequestCadastro()) {
            isLoading.value = false;
            return false
        };

        try{
            await Cadastrar(requestCadastro.value);
        }catch(error){
            errorCadastro.value = "Erro ao cadastrar usuário";
            return false;
        }finally{
            isLoading.value = false;
            resetRequestCadastro();
        }

        return true;
    }

    function resetRequestCadastro() {
        requestCadastro.value = {
            email: "",
            nome: "",
            senha: "",
        }
    }

    return{
        isLoading,
        errorCadastro,
        requestCadastro,
        handleSubmit,        
    }
}