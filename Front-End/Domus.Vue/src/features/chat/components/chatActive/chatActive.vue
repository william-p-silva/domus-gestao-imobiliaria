<script setup lang="ts">
import { useAuthStore } from '@/core/configuration/authentication';
import { useChat } from '../../hooks/useChat';
import { nextTick, ref, watch } from 'vue';

const { chatActive, request, enviarMensagem } = useChat();
const { userLogged } = useAuthStore()

const mensagensRef = ref<HTMLElement>();

watch(() => chatActive.value?.mensagens.length, async () => {
    await nextTick();
    mensagensRef.value?.scrollTo({ top: mensagensRef.value.scrollHeight, behavior: 'smooth' });
});
</script>

<template>
    <main>
        <div ref="mensagensRef" class="overflow-y-auto flex-1">
            <div v-for="mensagem in chatActive?.mensagens" :key="mensagem.mensagemChat_ID">
                <p class="text-primary text-end">
                    {{ mensagem.texto }}
                </p>

            </div>
        </div>

        <input type="text" v-model="request.texto">
        <button type="button" @click="enviarMensagem">
            Enviar msg
        </button>
    </main>
</template>