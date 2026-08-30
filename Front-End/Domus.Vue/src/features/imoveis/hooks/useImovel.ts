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
  const imoveis = ref<ImovelResponse[]>();

  const route = useRoute();

  // Verifica se a URL tem algum parâmetro de busca ativo
  function temFiltroAtivo(): boolean {
    return Object.keys(route.query).length > 0;
  }

  async function setImoveis(filtro: FiltroImovelType) {
    const queryParams = new URLSearchParams();

    if (filtro.tipoImovel) queryParams.append("tipoImovel", filtro.tipoImovel);
    if (filtro.endereco) queryParams.append("endereco", filtro.endereco);
    if (filtro.comodos !== undefined && filtro.comodos > 0) queryParams.append("comodos", filtro.comodos.toString());
    if (filtro.banheiros !== undefined && filtro.banheiros > 0) queryParams.append("banheiros", filtro.banheiros.toString());
    if (filtro.minPreco !== undefined && filtro.minPreco > 0) queryParams.append("minPreco", filtro.minPreco.toString());
    if (filtro.maxPreco !== undefined && filtro.maxPreco > 0) queryParams.append("maxPreco", filtro.maxPreco.toString());
    if (filtro.minArea !== undefined && filtro.minArea > 0) queryParams.append("minArea", filtro.minArea.toString());
    if (filtro.maxArea !== undefined && filtro.maxArea > 0) queryParams.append("maxArea", filtro.maxArea.toString());

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
      tipoImovel: typeof query.tipoImovel === 'string' ? query.tipoImovel : '',
      minPreco: query.minPreco ? Number(query.minPreco) : 0,
      maxPreco: query.maxPreco ? Number(query.maxPreco) : 0,
      comodos: query.comodos ? Number(query.comodos) : 0,
      banheiros: query.banheiros ? Number(query.banheiros) : 0,
      minArea: query.minArea ? Number(query.minArea) : 0,
      maxArea: query.maxArea ? Number(query.maxArea) : 0,
    };
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