import { ref, toValue } from "vue";
import type { TiposImovel } from "../../types/filtro/common"
import { FiltroImovelSchema, type FiltroImovelType } from "../../schemas/filtro/filtroImovelSchema";


const DEFAULT_TIPOS_IMOVEIS: TiposImovel[] = [
    {
        label: "Apartamento",
        value: "Apartamento"
    },
    {
        label: "Casa",
        value: "Casa"
    },
    {
        label: "Studio",
        value: "Studio"
    },
    {
        label: "KitNet",
        value: "KitNet"
    },
    {
        label: "Sobrado",
        value: "Sobrado"
    },
]

const DEFAULT_FILTRO: FiltroImovelType = {
    endereco: "",
    tipoImovel: "Apartamento",
    faixaPreco: [0, 8000],
    comodos: 0,
    banheiros: 0,
    areaM2: [0, 0],
}

const endereco = ref<string>(DEFAULT_FILTRO.endereco ?? "");
const tipoImovel = ref<string>(DEFAULT_FILTRO.tipoImovel ?? "Apartamento");
const faixaPreco = ref<[number, number]>(DEFAULT_FILTRO.faixaPreco ?? [800, 8000]);
const numQuartos = ref<number>(DEFAULT_FILTRO.comodos ?? 0);
const numBanheiros = ref<number>(DEFAULT_FILTRO.banheiros ?? 0);
const minArea = ref<number>(DEFAULT_FILTRO.areaM2?.[0] ?? 0);
const maxArea = ref<number>(DEFAULT_FILTRO.areaM2?.[1] ?? 0);


export const useFiltro = () => {
    const tiposImoveis: TiposImovel[] = DEFAULT_TIPOS_IMOVEIS;
    const numObj = [1, 2, 3, 4]
    const isLoading = ref(false);
    const errorFiltro = ref("");

    const filtro = ref<FiltroImovelType>(DEFAULT_FILTRO);

    async function submit() {
        isLoading.value = true;

        filtro.value = {
            endereco: toValue(endereco),
            tipoImovel: toValue(tipoImovel),
            faixaPreco: toValue(faixaPreco),
            comodos: toValue(numQuartos),
            banheiros: toValue(numBanheiros),
            areaM2: [toValue(minArea), toValue(maxArea)],
        };

        if(!verifyFiltro())
            return false;

        console.log(filtro.value);
        isLoading.value = false;
    }

    function verifyFiltro(){
        const result = FiltroImovelSchema.safeParse(filtro.value);

        if (!result.success) {
            errorFiltro.value = result.error.issues[0]!.message;
            return false;
        }

        return true;
    }


    function limparFiltros() {
        endereco.value = DEFAULT_FILTRO.endereco ?? "";
        tipoImovel.value = DEFAULT_FILTRO.tipoImovel ?? "Apartamento";
        faixaPreco.value = [...(DEFAULT_FILTRO.faixaPreco ?? [800, 8000])];
        numQuartos.value = DEFAULT_FILTRO.comodos ?? 0;
        numBanheiros.value = DEFAULT_FILTRO.banheiros ?? 0;
        minArea.value = DEFAULT_FILTRO.areaM2?.[0] ?? 0;
        maxArea.value = DEFAULT_FILTRO.areaM2?.[1] ?? 0;
    }

    return {
        isLoading,
        tiposImoveis,
        endereco,
        numObj,
        tipoImovel,
        faixaPreco,
        numQuartos,
        numBanheiros,
        minArea,
        maxArea,
        limparFiltros,
        submit
    }
}