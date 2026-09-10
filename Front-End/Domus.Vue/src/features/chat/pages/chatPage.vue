<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../hooks/useChat';
import SemChat from '../components/semChat.vue';
import BaseListaChats from '../components/listaChats/baseListaChats.vue';
import InfosImovelChat from '../components/infosImovel/infosImovelChat.vue';
import ChatActive from '../components/chatActive/chatActive.vue';



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
    
    <main class="flex flex-row">
        <BaseListaChats />
        <ChatActive />
        <InfosImovelChat />
    </main>

    <p v-if="chatActive">{{ chatActive.nome }}</p>
    <SemChat v-else />
    <p>{{ error }}</p>

</template>