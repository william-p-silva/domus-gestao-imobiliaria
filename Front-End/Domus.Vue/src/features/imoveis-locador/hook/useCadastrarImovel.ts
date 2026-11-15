import { ref, watch, type Ref } from "vue";
import { z } from "zod";
import { type CadastrarImovelDTO } from "../types/cadastrarImovel";
import { cadastrarImovelSchema } from "../schemas/cadastrarImovelSchema"; // ajuste o caminho
import { enderecoSchema } from "@/shared/schemas/endereco/enderecoScema";


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


type Endereco = CadastrarImovelDTO["endereco"];

// campos simples (exceto endereço) + erros do endereço aninhados
export type Erros = Partial<
    Record<Exclude<keyof CadastrarImovelDTO, "endereco">, string>
> & {
    endereco?: Partial<Record<keyof Endereco, string>>;
};

export const useCadastrarImovel = () => {
    const isLoading = ref<boolean>(false);
    const error = ref<string>("");
    const errors = ref<Erros>({});

    const requestCadastro = ref<CadastrarImovelDTO>(structuredClone(DEFAULT_REQUEST));

    let tentouEnviar = false;

    function validar() {
        const result = cadastrarImovelSchema.safeParse(requestCadastro.value);

        if (result.success) {
            errors.value = {};
            return result.data;
        }

        const novosErros: Erros = {};

        for (const issue of result.error.issues) {
            const [campo, subcampo] = issue.path;

            if (campo === "endereco" && typeof subcampo === "string") {
                novosErros.endereco ??= {};
                // guarda só a primeira mensagem de cada campo
                novosErros.endereco[subcampo as keyof Endereco] ??= issue.message;
            } else if (typeof campo === "string") {
                const chave = campo as Exclude<keyof CadastrarImovelDTO, "endereco">;
                novosErros[chave] ??= issue.message;
            }
        }

        errors.value = novosErros;
        return null;
    }

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

export type CadastrarImovelForm = {
    isLoading: Ref<boolean>;
    error: Ref<string>;
    errors: Ref<Erros>;
    requestCadastro: Ref<CadastrarImovelDTO>;
    enviar: () => void;
};
