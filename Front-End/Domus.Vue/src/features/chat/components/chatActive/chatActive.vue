<script setup lang="ts">
import { useChat } from '../../hooks/useChat';
import { nextTick, ref, watch } from 'vue';
import BolhaMsg from './bolhaMsg.vue';
import InputChat from './inputChat.vue';

const mensagensRef = ref<HTMLElement>();
const { chatActive } = useChat();

function scrollToBottom(smooth = true) {
    mensagensRef.value?.scrollTo({
        top: mensagensRef.value.scrollHeight,
        behavior: smooth ? 'smooth' : 'auto',
    });
}

// dispara ao trocar de chat (abre já na última msg, sem animação)
// e também quando chegam novas mensagens no chat ativo (com animação)
watch(
    () => chatActive.value?.chat_ID,
    async () => {
        await nextTick();
        scrollToBottom(false);
    },
    { immediate: true }
);

watch(
    () => chatActive.value?.mensagens.length,
    async () => {
        await nextTick();
        scrollToBottom(true);
    }
);
</script>

<template>
    <main
        class="
            flex-1
            min-w-0
            min-h-0
            h-full
            flex
            flex-col
            overflow-hidden
            bg-background
        "
    >
        <!-- Mensagens -->
        <div
            ref="mensagensRef"
            class="
                flex-1
                min-h-0
                w-full
                overflow-y-auto
                overflow-x-hidden
                p-4
                flex
                flex-col
                gap-3
            "
        >
            <BolhaMsg
                v-for="mensagem in chatActive?.mensagens"
                :key="mensagem.mensagemChat_ID"
                :mensagem="mensagem"
            />
        </div>

        <!-- Input -->
        <InputChat />
    </main>
</template>