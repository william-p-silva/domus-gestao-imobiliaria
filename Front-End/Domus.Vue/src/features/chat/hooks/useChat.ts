import { ref } from "vue"
import type { ResponseChat } from "../types/chatResponse";
import { useRoute } from "vue-router";
import { ChatService } from "../service/chatService";


const isLoading = ref<boolean>(false)
const error = ref<string>('');
const chat = ref<ResponseChat>();

const service = new ChatService();

export const useChat = () => {
    const route = useRoute();


    async function setChatByImovelId() {
        isLoading.value = true;
        const query = route.query
        const imovel_ID = query.imovel_ID?.toString() ?? '';

        try{
            const result = await service.getChatByImovelId(imovel_ID.toString());

            chat.value = result;
        }catch(erro){
            if(erro instanceof Error){
                error.value = erro.message;
            }
        }
        finally{
            isLoading.value = false;
        }
    }

    return {
        isLoading,
        error,
        chat,
        setChatByImovelId,
    }
}