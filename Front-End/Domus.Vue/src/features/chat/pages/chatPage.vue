<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../hooks/useChat';
import SemChat from '../components/semChat.vue';



const {
    isLoading,
    error,
    chat,
    setChatByImovelId,
} = useChat();



onMounted(async () => {
    await setChatByImovelId();
});

</script>


<template>
    <p v-if="chat">{{ chat.nome }}</p>
    <SemChat v-else />
    <p>{{ error }}</p>

</template>