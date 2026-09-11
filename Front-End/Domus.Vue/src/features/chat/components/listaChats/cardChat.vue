<script setup lang="ts">
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';
import { useChat } from '../../hooks/useChat';
import type { UserChatsResponse } from '../../types/userChatsResponse';




const props = defineProps<{
    chat: UserChatsResponse,
}>();
const {selectChat, chatActive} = useChat();
const formatter = FormatterDate()

async function handleChat() {
    await selectChat(props.chat.chat_ID)
}
</script>





<template>
    <div class="w-full  px-4 py-4  border-y gap-2 cursor-pointer hover:bg-primary-light/10 border-primary/10 flex flex-row justify-between" @click="handleChat" :class="chat.chat_ID == chatActive?.chat_ID ? 'border-primary-light bg-primary-light/10 hover:bg-primary-light/20' : ''">
        <div class="flex items-center">
            <div class="border border-primary/60 w-12 h-12 rounded-2xl flex justify-center items-center">
                {{ chat.nomeChat[0] }}
            </div>
        </div>
        <div class="flex gap-2 flex-col">
            <h1 class="text-[16px] text-primary font-bold">
                {{ chat.nomeChat }}
            </h1>
            <p class="flex gap-1 text-text-muted w-full text-sm">
                <span class="font-bold text-primary/60">{{ chat.nomeUsuario }}:</span>
                {{ chat.textoMensagem }}
            </p>
        </div>
        <div class="text-primary flex flex-col justify-between items-end">
            <span>{{ formatter.dayMoth(chat.dataUltimaMensagem) }}</span>
            <span>{{ formatter.hora(chat.dataUltimaMensagem) }}</span>
        </div>

    </div>
</template>