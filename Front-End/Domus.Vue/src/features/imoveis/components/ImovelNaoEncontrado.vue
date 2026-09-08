<script setup lang="ts">
import { SearchX } from '@lucide/vue';
import { useRouter } from 'vue-router';
import VoltarBuscaButton from '@/shared/components/buttons/voltarButton.vue';

const router = useRouter();

const props = withDefaults(defineProps<{
    title?: string,
    text?: string,
    route?: string
}>(), {
    title: "Imóvel não encontrado",
    text: 'Esse imóvel pode ter sido removido ou o link que você acessou está incorreto.',
    route: '/imoveis'
});

function voltarParaBusca() {
    router.push({ path: props.route });
}
</script>

<template>
    <div class="w-full flex flex-col items-center justify-center text-center gap-4 py-16 sm:py-24 px-4">
        <div class="bg-primary-soft/80 rounded-full p-4">
            <SearchX :size="36" class="text-primary" />
        </div>

        <div class="flex flex-col gap-1">
            <h2 class="text-primary-dark text-xl sm:text-2xl font-bold">
                {{ title }}
            </h2>
            <p class="text-sm sm:text-base text-text-muted max-w-sm">
                {{ text }}
            </p>
        </div>

        <VoltarBuscaButton @click="voltarParaBusca" />
    </div>
</template>