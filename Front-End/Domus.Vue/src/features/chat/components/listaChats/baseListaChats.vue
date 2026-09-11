<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../../hooks/useChat';
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';


const {
    isLoading,
    error,
    chatsUser,
    setUserChats,
} = useChat()

const formatter = FormatterDate()


onMounted(async () => {
    await setUserChats();
});

</script>


<template>
    <aside class="w-[30vw] bg-primary h-full ">

    </aside>

    <div class="flex flex-col gap-4 ">

        <div v-for="chat in chatsUser" class="flex flex-col gap-1 bg-accent">
            <p>{{ chat.nomeChat }}</p>
            <p>{{ chat.funcao }}</p>
            <p>{{ chat.textoMensagem }}</p>
            <p>{{ formatter.dataHora(chat.dataUltimaMensagem) }}</p>
        </div>
    </div>

</template>