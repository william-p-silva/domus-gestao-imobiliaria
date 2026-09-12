<script setup lang="ts">
import { Paperclip, Send } from '@lucide/vue';
import { useChat } from '../../hooks/useChat';

const { request, enviarMensagem } = useChat();
</script>

<template>
    <div class="w-full p-4 ">
        <div class="shrink-0 border border-primary/15 p-4 rounded-2xl shadow-2xl shadow-primary/15">
            <div class="flex gap-6 items-center px-6">
                <Paperclip class="text-primary" />
                <input
                    type="text"
                    v-model="request.texto"
                    placeholder="Digite uma mensagem..."
                    class="
                        flex-1
                        rounded-lg
                        px-4
                        py-3
                        border
                        border-primary/20
                        bg-white/0
                        text-text
                        focus:outline-none
                        focus:border-primary
                    "
                    @keyup.enter="enviarMensagem"
                />
                <button
                    type="button"
                    @click="enviarMensagem"
                    class="
                        bg-primary
                        text-white
                        rounded-lg
                        px-5
                        py-3
                        hover:bg-primary-dark cursor-pointer
                        transition-colors justify-center items-center flex
                    "
                >
                    <Send />
                </button>
            </div>
        </div>
    </div>
</template>