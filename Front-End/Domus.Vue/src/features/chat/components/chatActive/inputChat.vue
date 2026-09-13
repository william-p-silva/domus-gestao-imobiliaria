<script setup lang="ts">
import { Paperclip, Send } from '@lucide/vue';
import { useChat } from '../../hooks/useChat';

const { request, enviarMensagem } = useChat();
</script>

<template>
    <div class="w-full p-2 sm:p-4">
        <div class="shrink-0 border border-primary/15 p-2 sm:p-4 rounded-2xl shadow-2xl shadow-primary/15">
            <div class="flex gap-2 sm:gap-4 items-center ">
                <button
                    type="button"
                    class="shrink-0 text-primary hover:text-primary-dark transition-colors cursor-pointer"
                >
                    <Paperclip class="w-5 h-5 sm:w-6 sm:h-6" />
                </button>

                <input
                    type="text"
                    v-model="request.texto"
                    placeholder="Digite uma mensagem..."
                    class="
                        flex-1
                        min-w-0
                        rounded-lg
                        px-3
                        py-2.5
                        sm:px-4
                        sm:py-3
                        border
                        border-primary/20
                        bg-white/0
                        text-text
                        text-sm
                        sm:text-base
                        focus:outline-none
                        focus:border-primary
                    "
                    @keyup.enter="enviarMensagem"
                />

                <button
                    type="button"
                    @click="enviarMensagem"
                    class="
                        shrink-0
                        bg-primary
                        text-white
                        rounded-lg
                        p-2.5
                        sm:px-5
                        sm:py-3
                        hover:bg-primary-dark cursor-pointer
                        transition-colors justify-center items-center flex
                    "
                >
                    <Send class="w-4 h-4 sm:w-5 sm:h-5" />
                </button>
            </div>
        </div>
    </div>
</template>