<script setup lang="ts">
import { useAuthStore } from '@/core/configuration/authentication';
import { FormatterDate } from '@/shared/utils/formatter/formatterDate';
import type { ResponseMensagens } from '../../types/chatResponse';

const props = defineProps<{
    mensagem: ResponseMensagens,
}>();

const { userLogged } = useAuthStore();
const formatter = FormatterDate();
</script>

<template>
    <div class="flex w-full" :class="userLogged.usuario_ID === mensagem.usuario_ID
        ? 'justify-end'
        : 'justify-start'">
        <div class="flex items-end gap-2 max-w-[70%]" :class="userLogged.usuario_ID === mensagem.usuario_ID
            ? 'flex-row-reverse'
            : 'flex-row'">
            <!-- Avatar -->
            <div class="
                            w-8 h-8
                            rounded-full
                            shrink-0
                            bg-primary-light
                            flex items-center justify-center
                            text-xs
                            text-white
                            font-semibold
                        " :class="mensagem.usuario_ID === userLogged.usuario_ID ? 'hidden' : ''">
                {{ mensagem.usuario_ID === userLogged.usuario_ID ? 'Eu' : 'C' }}
            </div>

            <!-- Bolha -->
            <div class="
                            rounded-2xl
                            px-4
                            py-2
                            flex
                            flex-col
                            max-w-85
                        " :class="userLogged.usuario_ID === mensagem.usuario_ID
                            ? 'bg-primary text-white rounded-br-sm'
                            : 'bg-primary-soft text-text rounded-bl-sm'">
                <p class="text-sm whitespace-pre-wrap wrap-break-word">
                    {{ mensagem.texto }}
                </p>

                <span class="text-[11px] mt-1 self-end" :class="userLogged.usuario_ID === mensagem.usuario_ID
                    ? 'text-primary-soft/70'
                    : 'text-text-muted'">
                    {{ formatter.dataHora(mensagem.dataEnvio) }}
                </span>
            </div>
        </div>
    </div>
</template>