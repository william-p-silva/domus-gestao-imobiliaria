import { ref, watch } from "vue";
import { z } from "zod";
import { type CadastrarImovelDTO } from "../types/cadastrarImovel";
import { cadastrarImovelSchema } from "../schemas/cadastrarImovelSchema"; // ajuste o caminho


export const DEFAULT_REQUEST: CadastrarImovelDTO = {
    titulo: "",
    descricao: "",
    comodos: 1,
    banheiros: 1,
    metrosQuadrados: 0,
    valorAluguel: 0,
    status: "Disponivel",
    tipoDoImovel: "Apartamento",
    caracteristicas: [],
    endereco: {
        cep: "",
        uf: "",
        cidade: "",
        bairro: "",
        rua: "",
        numero: "",
        complemento: "",
    },
};


type Erros = Partial<Record<keyof CadastrarImovelDTO, string>>;

export const useCadastrarImovel = () => {
    const isLoading = ref<boolean>(false);
    const error = ref<string>(""); // erro geral (ex.: falha na API)
    const errors = ref<Erros>({}); // erro por campo

    // cópia, para não mutar a constante
    const requestCadastro = ref<CadastrarImovelDTO>(structuredClone(DEFAULT_REQUEST));

    let tentouEnviar = false;

    function validar() {
        const result = cadastrarImovelSchema.safeParse(requestCadastro.value);

        if (result.success) {
            errors.value = {};
            return result.data;
        }

        const { fieldErrors } = z.flattenError(result.error);
        errors.value = Object.fromEntries(
            Object.entries(fieldErrors).map(([campo, msgs]) => [campo, msgs?.[0]])
        ) as Erros;

        return null;
    }

    // depois da primeira tentativa de envio, revalida enquanto o usuário corrige
    watch(
        requestCadastro,
        () => {
            if (tentouEnviar) validar();
        },
        { deep: true }
    );

    function enviar() {
        tentouEnviar = true;
        const dados = validar();
        if (!dados) return;

        // aqui você chama a API com `dados` (já validado e tipado)
    }

    return {
        isLoading,
        error,
        errors,
        requestCadastro,
        enviar,
    };
};