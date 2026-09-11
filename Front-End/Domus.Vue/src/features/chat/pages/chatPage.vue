<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../hooks/useChat';
import SemChat from '../components/semChat.vue';
import BaseListaChats from '../components/listaChats/baseListaChats.vue';
import InfosImovelChat from '../components/infosImovel/infosImovelChat.vue';
import ChatActive from '../components/chatActive/chatActive.vue';
import PageLoading from '@/shared/components/loading/pageLoading.vue';



const {
    isLoading,
    error,
    chatActive,
    setChatByImovelId,
} = useChat();



onMounted(async () => {
    await setChatByImovelId();
});

</script>


<template>
    <PageLoading :visible="isLoading" />
    
    <main class="flex flex-row">
        <BaseListaChats />
        <ChatActive />
        <InfosImovelChat />
    </main>



</template>