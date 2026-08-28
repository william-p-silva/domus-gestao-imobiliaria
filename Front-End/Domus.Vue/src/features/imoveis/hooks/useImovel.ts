import { mockListaImoveis } from "@/shared/data/mocks/imoveisMock";
import type { ImovelResponse } from "@/shared/types/imovel/imovelResponse"
import { ref } from "vue"
import { imovelService } from "../services/imovelService";
import { useRoute, useRouter } from "vue-router";
import type { FiltroImovelType } from "../schemas/filtro/filtroImovelSchema";


const imoveis = mockListaImoveis

const service = new imovelService();

export const useImovel = () => {
    const router = useRouter();
    const imoveis = ref<ImovelResponse[]>(mockListaImoveis);

    const route = useRoute();

    // Verifica se a URL tem algum parâmetro de busca ativo
    function temFiltroAtivo(): boolean {
        return Object.keys(route.query).length > 0;
    }

    async function setImoveis(filtro: FiltroImovelType) {
        const queryParams = new URLSearchParams();
    
        if (filtro.tipoImovel) queryParams.append("tipoImovel", filtro.tipoImovel);
        if (filtro.endereco) queryParams.append("endereco", filtro.endereco);
        if (filtro.comodos) queryParams.append("comodos", filtro.comodos.toString());
        if (filtro.banheiros) queryParams.append("banheiros", filtro.banheiros.toString());
        if (filtro.faixaPreco?.[0]) queryParams.append("precoMin", filtro.faixaPreco[0].toString());
        if (filtro.faixaPreco?.[1]) queryParams.append("precoMax", filtro.faixaPreco[1].toString());
    
        const imoveisResponse = await service.getImoveis(
          `imovel/get/listar/pesquisa?${queryParams.toString()}`
        );
    
        imoveis.value = imoveisResponse;
      }
    
      async function setImoveisNotFiltro() {
        const imoveisResponse = await service.getImoveis(`imovel/get/listar/aprovados`);
        imoveis.value = imoveisResponse;
      }

    function getFiltroUrl(): FiltroImovelType {
        const query = route.query;

        return {
            endereco: typeof query.endereco === 'string' ? query.endereco : '',
            tipoImovel: typeof query.tipoImovel === 'string' ? query.tipoImovel : 'Apartamento',
            faixaPreco: [
                query.precoMin ? Number(query.precoMin) : 0,
                query.precoMax ? Number(query.precoMax) : 0
            ],
            comodos: query.comodos ? Number(query.comodos) : 0,
            banheiros: query.banheiros ? Number(query.banheiros) : 0,
            areaM2: [
                query.areaMin ? Number(query.areaMin) : 0,
                query.areaMax ? Number(query.areaMax) : 0
            ]
        }
    }

    return {
        route,
        imoveis,
        temFiltroAtivo,
        getFiltroUrl,
        setImoveis,
        setImoveisNotFiltro,
      };
}