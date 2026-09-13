<script setup lang="ts">
import { computed } from 'vue';
import { ShieldCheck, BadgeCheck } from '@lucide/vue';
import type { ResponseParticipantes } from '../../types/chatResponse';
import AppButton from '@/shared/components/buttons/appButton.vue';
import { useRouter } from 'vue-router';



const router = useRouter();

const props = defineProps<{
    locador?: ResponseParticipantes
}>()

const iniciais = computed(() => {
    return props.locador?.nome
        ?.split(' ')
        .slice(0, 2)
        .map((parte) => parte[0])
        .join('')
        .toUpperCase() ?? '';
});

const anoCadastro = computed(() => {
    if (!props.locador?.criadoEm) return '';
    return new Date(props.locador.criadoEm).getFullYear();
});

const isVerificado = computed(() => {
    return props.locador?.estado?.toLowerCase() === 'ativo';
});


function handleSubmit() {
    router.push({
        path: "/locador/perfil",
        query: { id: props.locador?.usuario_ID }
    });
}
</script>

<template>
    <div class="flex flex-col gap-4 w-full bg-white rounded-2xl shadow-lg shadow-primary/10 p-4">

        <h2 class="font-bold text-text text-base">Sobre o locador</h2>

        <div class="flex items-start gap-3">
            <div class="w-11 h-11 rounded-full bg-primary-soft flex items-center justify-center shrink-0">
                <span class="text-primary font-semibold text-sm">{{ iniciais }}</span>
            </div>

            <div class="flex flex-col gap-1">
                <p class="font-semibold text-text text-sm">{{ locador?.nome }}</p>
                <p class="text-text-muted text-xs">Anunciante desde {{ anoCadastro }}</p>

                <span v-if="isVerificado" class="flex items-center gap-1 text-primary text-xs font-medium mt-1">
                    <BadgeCheck :size="14" />
                    Verificado
                </span>
            </div>
        </div>

        <div class="flex justify-between border-y border-primary/10 py-3">
            <div class="flex flex-col items-center gap-0.5">
                <p class="font-bold text-text text-base">{{ locador?.qntImoveis }}</p>
                <span class="text-text-muted text-xs">Imóveis anunciados</span>
            </div>

            <div class="flex flex-col items-center gap-0.5">
                <p class="font-bold text-text text-base">{{ locador?.mediaAvaliacao }}</p>
                <span class="text-text-muted text-xs">Média de avaliações</span>
            </div>
        </div>

        <p class="text-text-muted text-sm leading-relaxed">
            {{ locador?.bio }}
        </p>

        <AppButton variant="outline" class="rounded-full" :action="handleSubmit">
            Ver perfil do locador
        </AppButton>


    </div>
    <div class="flex gap-3 bg-primary-soft rounded-xl p-3 items-center">
        <ShieldCheck :size="25" class="text-primary shrink-0 mt-0.5" />
        <div class="flex flex-col gap-0.5">
            <p class="font-semibold text-text text-sm">Negociação segura</p>
            <p class="text-text-muted text-xs leading-relaxed">
                Todas as conversas são protegidas e ficam registradas na plataforma.
            </p>
        </div>
    </div>
</template>