import { ref } from "vue";
import { type CadastrarImovelDTO } from "../types/cadastrarImovel";

export const DEFAULT_REQUEST: CadastrarImovelDTO = {
    titulo: "",
    descricao: "",
    comodos: 1,
    banheiros: 1,
    metrosQuadrados: 0,
    valorAluguel: 0,
    status: "Disponivel",
    tipoDoImovel: 0,
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

export const useCadastrarImovel = () => {
    const isLoading = ref<boolean>(false);
    const error = ref<string>("");

    const requestCadastro = ref<CadastrarImovelDTO>(DEFAULT_REQUEST);

    return {
        isLoading,
        error,
        requestCadastro,
    };
};
