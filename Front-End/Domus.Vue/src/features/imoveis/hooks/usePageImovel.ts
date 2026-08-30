import { type ImovelResponse } from "@/shared/types/imovel/imovelResponse";
import { ref } from "vue";
import { useRoute } from "vue-router";
import { imovelService } from "../services/imovelService";


const service = new imovelService()

const isLoading = ref(false);
const erro = ref('')
const imovel = ref<ImovelResponse>();

export const usePageImovel = () => {
    const route = useRoute();


    async function getImovelId() {
        const id = route.query.imovel_id;

        if(id != null)
        {
            const result = await service.getImovel(id.toString());
            imovel.value = result;
        }
    }


    return {
        isLoading,
        erro,
        imovel,
        route,
        getImovelId
    }
}