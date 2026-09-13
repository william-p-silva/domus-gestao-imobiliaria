<script setup lang="ts">
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';
import { useChat } from '../../hooks/useChat';
import type { UserChatsResponse } from '../../types/userChatsResponse';
import imovelSemImagem from '@/assets/imgs/Imovel/imovel_sem_imagem_card.png'




const props = defineProps<{
    chat: UserChatsResponse,
}>();

const { selectChat, chatActive } = useChat();
const formatter = FormatterDate()

async function handleChat() {
    await selectChat(props.chat.chat_ID)
}
</script>

<template>
    <div @click="handleChat" class="
            w-full
            px-4
            py-4
            border-y
            gap-2
            cursor-pointer
            h-30
            hover:bg-primary-light/10
            border-primary/10
            flex
            flex-row
            justify-between
            items-center
        " :class="chat.chat_ID == chatActive?.chat_ID
            ? 'border-primary-light bg-primary-light/10 hover:bg-primary-light/20'
            : ''">
        <!-- Avatar -->
        <div class="flex items-center shrink-0">
            <div class=" w-16 h-16 rounded-2xl flex justify-center items-center bg-cover">
                <img :src="imovelSemImagem" alt="">
            </div>
        </div>

        <!-- Nome + última mensagem -->
        <div class="flex gap-1 flex-col flex-1 min-w-0">
            <h1 class="text-[16px] text-primary font-bold truncate">
                {{ chat.nomeChat }}
            </h1>
            <p class="text-text-muted text-sm min-w-0 line-clamp-2">
                <span class="font-bold text-primary/60">{{ chat.nomeUsuario }}:</span>
                {{ chat.textoMensagem }}
            </p>
        </div>

        <!-- Data / hora -->
        <div class="text-primary flex flex-col justify-between items-end shrink-0">
            <span class="text-xs">{{ formatter.dayMoth(chat.dataUltimaMensagem) }}</span>
            <span class="text-xs">{{ formatter.hora(chat.dataUltimaMensagem) }}</span>
        </div>
    </div>
</template>