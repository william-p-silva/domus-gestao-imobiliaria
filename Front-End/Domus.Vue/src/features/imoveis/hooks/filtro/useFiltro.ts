import { ref, toValue } from "vue";
import type { TiposImovel } from "../../types/filtro/common"
import { FiltroImovelSchema, type FiltroImovelType } from "../../schemas/filtro/filtroImovelSchema";
import { useRoute, useRouter } from "vue-router";


const DEFAULT_TIPOS_IMOVEIS: TiposImovel[] = [
    {
        label: "Todos",
        value: ""
    },
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
    tipoImovel: "",
    minPreco: 0,
    maxPreco: 8000,
    comodos: 0,
    banheiros: 0,
    minArea: 0,
    maxArea: 0
}

const endereco = ref<string>(DEFAULT_FILTRO.endereco ?? "");
const tipoImovel = ref<string>(DEFAULT_FILTRO.tipoImovel ?? "Apartamento");
const faixaPreco = ref<[number, number]>([800, 8000]);
const numQuartos = ref<number>(DEFAULT_FILTRO.comodos ?? 0);
const numBanheiros = ref<number>(DEFAULT_FILTRO.banheiros ?? 0);
const minArea = ref<number>(DEFAULT_FILTRO.minArea ?? 0);
const maxArea = ref<number>(DEFAULT_FILTRO.maxArea ?? 0);


export const useFiltro = () => {
    const tiposImoveis: TiposImovel[] = DEFAULT_TIPOS_IMOVEIS;
    const numObj = [1, 2, 3, 4]
    const isLoading = ref(false);
    const errorFiltro = ref("");

    const router = useRouter();
    const route = useRoute();

    const filtro = ref<FiltroImovelType>(DEFAULT_FILTRO);

    async function submit() {
        isLoading.value = true;

        filtro.value = {
            endereco: toValue(endereco),
            tipoImovel: toValue(tipoImovel),
            minPreco: faixaPreco.value[0],
            maxPreco: faixaPreco.value[1],
            comodos: toValue(numQuartos),
            banheiros: toValue(numBanheiros),
            minArea: minArea.value,
            maxArea: maxArea.value
        };

        if (!verifyFiltro())
            return false;

        // Monta o objeto de Query Params limpo para a URL
        const queryParams: Record<string, string | number> = {};

        if (filtro.value.endereco) queryParams.endereco = filtro.value.endereco;
        if (filtro.value.tipoImovel) queryParams.tipoImovel = filtro.value.tipoImovel;

        if (filtro.value.minPreco) queryParams.minPreco = filtro.value.minPreco;
        if (filtro.value.maxPreco) queryParams.maxPreco = filtro.value.maxPreco;

        if (filtro.value.comodos) queryParams.comodos = filtro.value.comodos;
        if (filtro.value.banheiros) queryParams.banheiros = filtro.value.banheiros;

        if (filtro.value.minArea) queryParams.minArea = filtro.value.minArea;
        if (filtro.value.maxArea) queryParams.maxArea = filtro.value.maxArea;
        // Atualiza a URL e recarrega os dados através do evento/navegação
        await router.push({ path: "/imoveis", query: queryParams });

        isLoading.value = false;
    }

    function verifyFiltro() {
        const result = FiltroImovelSchema.safeParse(filtro.value);

        if (!result.success) {
            errorFiltro.value = result.error.issues[0]!.message;
            return false;
        }

        return true;
    }


    function carregarFiltroDaUrl() {
        const q = route.query;
        if (q.endereco) endereco.value = String(q.endereco);
        if (q.tipoImovel) tipoImovel.value = String(q.tipoImovel);

        if (q.minPreco || q.maxPreco) {
            faixaPreco.value = [
                q.minPreco ? Number(q.minPreco) : 0,
                q.maxPreco ? Number(q.maxPreco) : 8000,
            ];
        }

        if (q.comodos) numQuartos.value = Number(q.comodos);
        if (q.banheiros) numBanheiros.value = Number(q.banheiros);

        if (q.minArea || q.maxArea) {
            minArea.value = q.minArea ? Number(q.minArea) : 0;
            maxArea.value = q.maxArea ? Number(q.maxArea) : 0;
        }
    }

    function limparFiltros() {
        endereco.value = DEFAULT_FILTRO.endereco ?? "";
        tipoImovel.value = DEFAULT_FILTRO.tipoImovel ?? "";
        faixaPreco.value = [800, 8000];
        numQuartos.value = DEFAULT_FILTRO.comodos ?? 0;
        numBanheiros.value = DEFAULT_FILTRO.banheiros ?? 0;
        minArea.value = DEFAULT_FILTRO.minArea ?? 0;
        maxArea.value = DEFAULT_FILTRO.maxArea ?? 0;

        router.push({ path: "/imoveis" });
    }

    return {
        isLoading,
        errorFiltro,
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
        carregarFiltroDaUrl,
        submit,
    };
}