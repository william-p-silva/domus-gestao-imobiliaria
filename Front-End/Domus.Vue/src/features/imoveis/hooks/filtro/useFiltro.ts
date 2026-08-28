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

    const router = useRouter();
    const route = useRoute();

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

        if (!verifyFiltro())
            return false;

        // Monta o objeto de Query Params limpo para a URL
        const queryParams: Record<string, string | number> = {};

        if (filtro.value.endereco) queryParams.endereco = filtro.value.endereco;
        if (filtro.value.tipoImovel) queryParams.tipoImovel = filtro.value.tipoImovel;

        if (filtro.value.faixaPreco?.[0]) queryParams.faixaPreco = filtro.value.faixaPreco[0];
        if (filtro.value.faixaPreco?.[1]) queryParams.faixaPreco = filtro.value.faixaPreco[1];

        if (filtro.value.comodos) queryParams.comodos = filtro.value.comodos;
        if (filtro.value.banheiros) queryParams.banheiros = filtro.value.banheiros;

        if (filtro.value.areaM2?.[0]) queryParams.faixaPreco = filtro.value.areaM2[0];
        if (filtro.value.areaM2?.[1]) queryParams.faixaPreco = filtro.value.areaM2[1];

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
        if (q.precoMin || q.precoMax) {
          faixaPreco.value = [
            q.precoMin ? Number(q.precoMin) : 0,
            q.precoMax ? Number(q.precoMax) : 8000,
          ];
        }
        if (q.comodos) numQuartos.value = Number(q.comodos);
        if (q.banheiros) numBanheiros.value = Number(q.banheiros);
        if (q.areaMin || q.areaMax) {
          minArea.value = q.areaMin ? Number(q.areaMin) : 0;
          maxArea.value = q.areaMax ? Number(q.areaMax) : 0;
        }
      }
    
      function limparFiltros() {
        endereco.value = DEFAULT_FILTRO.endereco ?? "";
        tipoImovel.value = DEFAULT_FILTRO.tipoImovel ?? "Apartamento";
        faixaPreco.value = [...(DEFAULT_FILTRO.faixaPreco ?? [800, 8000])];
        numQuartos.value = DEFAULT_FILTRO.comodos ?? 0;
        numBanheiros.value = DEFAULT_FILTRO.banheiros ?? 0;
        minArea.value = DEFAULT_FILTRO.areaM2?.[0] ?? 0;
        maxArea.value = DEFAULT_FILTRO.areaM2?.[1] ?? 0;
        
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