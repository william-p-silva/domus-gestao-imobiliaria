<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { useChat } from '../../hooks/useChat';
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';
import HeaderListaChats from './headerListaChats.vue';
import CardChat from './cardChat.vue';
import { PanelLeftOpen, X } from '@lucide/vue';

const {
    isLoading,
    error,
    chatsUser,
    chatActive,
    setUserChats,
} = useChat()

const formatter = FormatterDate()

const listaAberta = ref(false);

onMounted(async () => {
    await setUserChats();
});

// fecha a lista automaticamente ao selecionar um chat (abaixo de lg)
watch(
    () => chatActive?.value?.chat_ID,
    () => {
        listaAberta.value = false;
    }
);
</script>


<template>
    <!-- Botão para abrir a lista (só existe abaixo de lg) -->
    <button v-if="!listaAberta" type="button" @click="listaAberta = true" class="
            lg:hidden
            fixed
            bottom-4
            left-4
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
        <PanelLeftOpen :size="20" />
    </button>

    <!-- Backdrop (só abaixo de lg, quando a lista está aberta) -->
    <div v-if="listaAberta" @click="listaAberta = false" class="
            lg:hidden
            fixed
            inset-0
            bg-black/40
            z-40
        " />

    <aside :class="[
        `
            w-80
            bg-background
            shrink-0
            h-full
            min-h-0
            flex
            flex-col
            border-r
            border-primary/10
            overflow-hidden
            pt-4

            fixed
            top-0
            left-0
            z-50
            transition-transform
            duration-300
            ease-in-out

            lg:static
            lg:translate-x-0
            lg:z-auto
        `,
        listaAberta ? 'translate-x-0' : '-translate-x-full'
    ]">
        <div class="flex items-center justify-between px-2">
            <HeaderListaChats class="flex-1" />

            <!-- Botão fechar (só abaixo de lg) -->
            <button type="button" @click="listaAberta = false" class="
                    lg:hidden
                    text-text-muted
                    hover:text-text
                    transition-colors
                    shrink-0
                    ml-2
                ">
                <X :size="20" />
            </button>
        </div>

        <div class="flex-1 min-h-0 overflow-y-auto py-6">
            <CardChat
                v-for="chat in chatsUser"
                :key="chat.chat_ID"
                :chat="chat"
            />
        </div>
    </aside>
</template>