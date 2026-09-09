<script setup lang="ts">
import { onMounted } from 'vue';
import { useChat } from '../hooks/useChat';



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
    <p>{{ chat?.nome ?? "sem chat" }}</p>
    <p>{{ error }}</p>

</template>