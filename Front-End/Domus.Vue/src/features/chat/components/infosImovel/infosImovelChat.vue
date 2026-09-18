<script setup lang="ts">
import { FormatterNumber } from '@/shared/utils/formatter/formatterNumber';
import { IconBanheiros, IconComodos, IconMetros } from '@/shared/UI/icons/imovel';
import AppButton from '@/shared/components/buttons/appButton.vue';
import { ArrowRight, Heart, MapPin } from '@lucide/vue';
import imovelSemImagem from '@/assets/imgs/Imovel/imovel_sem_imagem_card.png'
import type { ResponseChat } from '../../types/chatResponse';
import { useRouter } from 'vue-router';

const formatter = FormatterNumber();
const sizeIcon = 18;
const router = useRouter();

const props = defineProps<{
    chatActive?: ResponseChat
}>();

function handleSubmit() {
    router.push({
        path: "/imovel",
        query: { imovel_id: props.chatActive?.imovel.imovel_ID }
    });
}
</script>

<template>

    <div class="flex flex-col w-full bg-white rounded-2xl shadow-lg shadow-primary/10 overflow-hidden">

        <div class="relative">
            <img :src="chatActive?.imovel.imagens[0]?.UrlImagem ?? imovelSemImagem" alt=""
                class="w-full h-40 object-cover" />

            <button type="button"
                class="absolute top-3 right-3 bg-white/90 rounded-full p-2 shadow-sm flex items-center justify-center hover:bg-white transition-colors">
                <Heart :size="18" class="text-primary" />
            </button>

            <span v-if="chatActive?.imovel.imagens?.length"
                class="absolute bottom-3 right-3 bg-black/60 text-white text-xs font-medium px-2 py-0.5 rounded-md">
                1/{{ chatActive?.imovel.imagens.length }}
            </span>
        </div>

        <div class="flex flex-col gap-3 px-4 pt-3 pb-4">
            <div>
                <h1 class="font-bold text-primary text-base leading-snug line-clamp-2">
                    {{ chatActive?.imovel.titulo }}
                </h1>
                <p class="flex items-center gap-1 text-text-muted text-sm mt-1">
                    <MapPin :size="14" />
                    {{ chatActive?.imovel.descricao }}
                </p>
            </div>

            <div class="flex justify-between w-full text-primary pt-1">
                <div class="flex flex-col items-start gap-0.5">
                    <p class="flex items-center gap-1 text-sm font-medium">
                        <IconComodos :size="sizeIcon" />
                        {{ chatActive?.imovel.comodos }}
                    </p>
                    <span class="text-xs text-text-muted">quartos</span>
                </div>

                <div class="flex flex-col items-start gap-0.5">
                    <p class="flex items-center gap-1 text-sm font-medium">
                        <IconBanheiros :size="sizeIcon" />
                        {{ chatActive?.imovel.banheiros }}
                    </p>
                    <span class="text-xs text-text-muted">banheiros</span>
                </div>

                <div class="flex flex-col items-start gap-0.5">
                    <p class="flex items-center gap-1 text-sm font-medium">
                        <IconMetros :size="sizeIcon" />
                        {{ formatter.metrosQuadrados(chatActive?.imovel.metrosQuadrados) }}
                    </p>
                    <span class="text-xs text-text-muted">área</span>
                </div>
            </div>

            <p class="text-primary">
                <span class="font-bold text-xl">{{ formatter.moeda(chatActive?.imovel.valorAluguel) }}</span>
                <span class="text-sm text-text-muted font-normal"> /mês</span>
            </p>

            <AppButton variant="outline" class="rounded-full" :action="handleSubmit">
                <span class="flex items-center justify-center gap-2">
                    Ver detalhes do aluguel
                    <ArrowRight :size="16" />
                </span>
            </AppButton>
        </div>
    </div>

</template>