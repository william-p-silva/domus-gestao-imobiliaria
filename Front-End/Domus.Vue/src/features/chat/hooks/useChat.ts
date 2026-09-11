import { ref } from "vue"
import type { ResponseChat } from "../types/chatResponse";
import { useRoute } from "vue-router";
import { ChatService } from "../service/chatService";
import { type UserChatsResponse } from "../types/userChatsResponse";
import type { InputBuscaImovel } from "@/features/landingPage/types/common";


const isLoading = ref<boolean>(false)
const error = ref<string>('');
const chatActive = ref<ResponseChat>();
const chatsUser = ref<UserChatsResponse[]>([]);

const service = new ChatService();

export const useChat = () => {
    const route = useRoute();

    const inputFiltro: InputBuscaImovel = {
        text: "",
        placeholder: "Buscar conversas...",
        icon: "location"
    }


    async function setUserChats() {
        isLoading.value = true;

        try{
            const result = await service.getUserChats();

            chatsUser.value = result;
        }catch(err){
            if(err instanceof Error){
                error.value = err.message;
            }
        }finally{
            isLoading.value = false;
        }
    }


    async function setChatByImovelId() {
        isLoading.value = true;
        const query = route.query
        const imovel_ID = query.imovel_ID?.toString();

        try{
            if(imovel_ID !== undefined){
                const result = await service.getChatByImovelId(imovel_ID.toString());
                
                chatActive.value = result;
            }

        }catch(erro){
            if(erro instanceof Error){
                error.value = erro.message;
            }
        }
        finally{
            isLoading.value = false;
        }
    }


    async function selectChat(chat_id: string) {
        console.log("oi", chat_id)
    }

    return {
        isLoading,
        error,
        chatActive,
        chatsUser,
        selectChat,
        inputFiltro,
        setUserChats,
        setChatByImovelId,
    }
}