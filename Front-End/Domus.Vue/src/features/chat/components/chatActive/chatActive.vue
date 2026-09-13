<script setup lang="ts">
import { useChat } from '../../hooks/useChat';
import { computed, nextTick, ref, watch } from 'vue';
import BolhaMsg from './bolhaMsg.vue';
import InputChat from './inputChat.vue';
import InfosImovelChat from '../infosImovel/infosImovelChat.vue';
import InfosLocador from '../infosLocador/infosLocador.vue';
import { useAuthStore } from '@/core/configuration/authentication.ts';
import { PanelRightOpen, X } from '@lucide/vue';

const mensagensRef = ref<HTMLElement>();
const { chatActive } = useChat();

const sidebarAberta = ref(false);

const locador = computed(() => {
    return chatActive.value?.participantes?.find(
        (participante) => participante.funcao.toLowerCase() === 'locador'
    );
});

function scrollToBottom(smooth = true) {
    mensagensRef.value?.scrollTo({
        top: mensagensRef.value.scrollHeight,
        behavior: smooth ? 'smooth' : 'auto',
    });
}

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

// fecha a sidebar automaticamente ao trocar de chat (abaixo de lg)
watch(
    () => chatActive.value?.chat_ID,
    () => {
        sidebarAberta.value = false;
    }
);
</script>

<template>
    <main class="
            flex-1
            min-w-0
            min-h-0
            h-full
            flex
            flex-col
            overflow-hidden
            bg-background
        ">
        <!-- Mensagens -->
        <div ref="mensagensRef" class="
                flex-1
                min-h-0
                w-full
                overflow-y-auto
                overflow-x-hidden
                p-4
                flex
                flex-col
                gap-3
            ">
            <BolhaMsg v-for="mensagem in chatActive?.mensagens" :key="mensagem.mensagemChat_ID" :mensagem="mensagem" />
        </div>

        <!-- Input -->
        <InputChat />
    </main>

    <!-- Botão para abrir a sidebar de infos (só existe abaixo de lg) -->
    <button v-if="!sidebarAberta" type="button" @click="sidebarAberta = true" class="
            lg:hidden
            fixed
            bottom-4
            right-4
            z-40
            bg-primary
            text-white
            rounded-full
            p-3
            shadow-lg
            shadow-primary/30
            flex
            items-center
            justify-center
        ">
        <PanelRightOpen :size="20" />
    </button>

    <!-- Backdrop (só abaixo de lg, quando a sidebar está aberta) -->
    <div v-if="sidebarAberta" @click="sidebarAberta = false" class="
            lg:hidden
            fixed
            inset-0
            bg-black/40
            z-40
        " />

    <aside :class="[
        `
            w-80
            bg-text-muted/6
            px-2
            shrink-0
            h-full
            min-h-0
            flex
            flex-col
            border-r
            border-primary/10
            overflow-y-auto
            overflow-x-hidden
            gap-4
            pt-4
            pb-4

            fixed
            top-0
            right-0
            z-50
            transition-transform
            duration-300
            ease-in-out

            lg:static
            lg:translate-x-0
            lg:z-auto
        `,
        sidebarAberta ? 'translate-x-0' : 'translate-x-full'
    ]">
        <!-- Botão fechar (só abaixo de lg) -->
        <button type="button" @click="sidebarAberta = false" class="
                lg:hidden
                self-end
                text-text-muted
                hover:text-text
                transition-colors
                shrink-0
            ">
            <X :size="20" />
        </button>

        <InfosImovelChat :chatActive="chatActive" class="shrink-0" />
        <InfosLocador v-if="locador" :locador="locador" class="shrink-0" />
    </aside>
</template>