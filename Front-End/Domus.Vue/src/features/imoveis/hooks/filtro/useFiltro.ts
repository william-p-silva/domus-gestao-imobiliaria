import { ref } from "vue";
import type { TiposImovel } from "../../types/filtro/common"


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


export const useFiltro = () => {
    const tiposImoveis: TiposImovel[] = DEFAULT_TIPOS_IMOVEIS;
    const numObj = [1, 2, 3, 4]

    const tipoImovel = ref<string>(DEFAULT_TIPOS_IMOVEIS[0]?.value || "Apartamento")
    const faixaPreco = ref<[number, number]>([800, 8000]);
    const numQuartos = ref<number>(0);
    const numBanheiros = ref<number>(0);



    return {
        tiposImoveis,
        numObj,
        tipoImovel,
        faixaPreco,
        numQuartos,
        numBanheiros,
    }
}