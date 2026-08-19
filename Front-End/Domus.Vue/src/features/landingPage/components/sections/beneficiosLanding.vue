<script setup lang="ts">
import { ArrowRight, ShieldCheck } from '@lucide/vue';
import { useLanding } from '../../hooks/useLanding.ts';
import Beneficios from '../cards/beneficios.vue';

const landing = useLanding();

</script>



<template>
    <section class="w-full px-4 sm:px-6 lg:px-8 pb-10 flex flex-col lg:flex-row gap-4 md:gap-0">
        <div>
            <h1 class="text-primary text-xl sm:text-2xl font-bold mb-6">Por que escolher a DOMUS?</h1>
            <div class="flex flex-col lg:flex-row gap-6 lg:items-stretch items-center">
                <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 flex-1">
                    <Beneficios v-for="beneficio in landing.beneficios" :key="beneficio.title" :beneficio="beneficio" />
                </div>
            </div>
        </div>

        <div class="w-full lg:w-72 shrink-0 bg-primary-light/15 p-5 rounded-2xl flex flex-col gap-3 text-text">
            <div class="flex items-center gap-2 text-primary">
                <ShieldCheck :size="28" class="shrink-0" />
                <h3 class="font-bold leading-tight">Sua segurança é nossa prioridade</h3>
            </div>
            <p class="text-sm">
                Ambiente protegido, dados seguros e uma experiência transparente do início ao fim.
            </p>
            <p class="flex gap-2 text-primary items-center text-sm font-medium">
                Saiba mais
                <ArrowRight :size="16" />
            </p>
        </div>
    </section>
</template>