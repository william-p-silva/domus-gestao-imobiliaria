import { type ImovelResponse } from "@/shared/types/imovel/imovelResponse";
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { imovelService } from "../services/imovelService";
import type { CreateImovelChat } from "../types/createChatImovel";


const service = new imovelService()

const isLoading = ref<boolean>(true);
const erro = ref<string>('')
const imovel = ref<ImovelResponse>();

export const usePageImovel = () => {
    const router = useRouter();
    const route = useRoute();

    async function getImovelId() {
        const id = route.query.imovel_id;

        if(id != null)
        {
            const result = await service.getImovel(id.toString());
            imovel.value = result;
        }

        isLoading.value = false;
    }

    async function createChatImovel() {
        isLoading.value = true;
        if(imovel.value?.imovel_ID !== undefined){
            const request: CreateImovelChat = {
                imovel_ID: imovel.value?.imovel_ID
            }
            const { response, success } = await service.postChatImovel(request)

            if(!success){
                erro.value = response
                isLoading.value = false;
                return;
            }

            router.push({path: `/chat?${response}`})
        }
        isLoading.value = false;
    }


    return {
        isLoading,
        erro,
        imovel,
        route,
        getImovelId,
        createChatImovel
    }
}