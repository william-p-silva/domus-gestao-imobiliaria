import { ref } from "vue"
import type { ResponseChat, ResponseMensagens } from "../types/chatResponse";
import { useRoute } from "vue-router";
import { ChatService } from "../service/chatService";
import { type UserChatsResponse } from "../types/userChatsResponse";
import type { InputBuscaImovel } from "@/features/landingPage/types/common";
import ChatHub from "../hubs/chatHub";
import type { EnviarMensagem } from "../types/chatRequest";


const chatHub = new ChatHub();
const hubIniciado = ref<boolean>();

const isLoading = ref<boolean>(false);
const error = ref<string>('');
const chatActive = ref<ResponseChat>();
const chatsUser = ref<UserChatsResponse[]>([]);

const service = new ChatService();



function handleNovaMensagem(mensagem: ResponseMensagens){
    if(chatActive.value && chatActive.value.chat_ID === mensagem.chat_ID){
        chatActive.value.mensagens.push(mensagem);
    } else {
        console.log('NÃO bateu — ids diferentes');
    }
}


async function garantirConexao() {
    if (!hubIniciado.value) {
        try {
            chatHub.onReceberMensagem(handleNovaMensagem);
            await chatHub.start();
            hubIniciado.value = true;

        } catch (err) {
            console.error('Falha ao conectar no SignalR:', err);
        }
    }
}

async function trocarChatAtivo(
    novoChatId: string,
    novoChat: ResponseChat
) {
    await garantirConexao();

    if (
        chatActive.value &&
        chatActive.value.chat_ID !== novoChatId
    ) {
        await chatHub.leaveGroupChat(
            chatActive.value.chat_ID
        );
    }

    await chatHub.joinGroupChat(novoChatId);

    chatActive.value = novoChat;
}
export const useChat = () => {
    const route = useRoute();

    const request = ref<EnviarMensagem>({
        chat_ID: '',
        texto: ''
    });

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
                await trocarChatAtivo(result.chat_ID, result);
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
        isLoading.value = true;
    
        try {
            const result = await service.getChat(chat_id);
    
            await trocarChatAtivo(
                result.chat_ID,
                result
            );
        } catch (err) {
            if (err instanceof Error) {
                error.value = err.message;
            }
        } finally {
            isLoading.value = false;
        }
    }


    async function fecharChatAtivo() {
        if (chatActive.value) {
            await chatHub.leaveGroupChat(chatActive.value.chat_ID);
            chatActive.value = undefined;
        }
    }


    async function enviarMensagem() {
        request.value.chat_ID = chatActive.value?.chat_ID ?? '';
    
        try {
            await service.enviarMensagemAsync(request.value);
            request.value.texto = '';
        } catch (err) {
            if (err instanceof Error) {
                error.value = err.message;
            }
        }
    }

    return {
        request,
        isLoading,
        error,
        chatActive,
        chatsUser,
        selectChat,
        inputFiltro,
        setUserChats,
        setChatByImovelId,
        fecharChatAtivo,
        enviarMensagem
    }
}