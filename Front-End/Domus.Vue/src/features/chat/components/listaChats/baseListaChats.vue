<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../../hooks/useChat';
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';
import HeaderListaChats from './headerListaChats.vue';
import CardChat from './cardChat.vue';


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
    <aside class="w-88 w py-6 h-screen  overflow-y-auto border-r border-primary/10 ">
        <HeaderListaChats />

        <CardChat v-for="chat in chatsUser" :chat="chat" />


    </aside>

</template>