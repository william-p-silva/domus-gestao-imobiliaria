<script setup lang="ts">
import { Bath, BedDouble, MapPin, MessageCircleMore, Ruler, ShieldCheck } from '@lucide/vue';
import { usePageImovel } from '../hooks/usePageImovel';
import img from "@/assets/imgs/LandingPage/encontrandoImovel.jpeg"
import { FormatterNumber } from '@/shared/utils/formatter/formatterNumber';
import AppButton from '@/shared/components/buttons/appButton.vue';

const imovelPage = usePageImovel();
const formmater = FormatterNumber();
const nums = 5
</script>

<template>

    <section class="w-full flex flex-col lg:flex-row gap-4 lg:gap-6 text-primary">
        <!-- Coluna de imagens -->
        <article class="w-full lg:w-3/5 lg:shrink-0">
            <div class="flex flex-col gap-3 w-full">
                <img :src="img" alt="" class="rounded-2xl w-full aspect-4/3 sm:aspect-video object-cover">
                    <div class="grid grid-cols-5 gap-8 sm:gap-8">
                        <div v-for="num in nums" :key="num" class=" sm:h-22 bg-primary rounded-lg" />
                    </div>
            </div>
        </article>
        <!-- Coluna de detalhes -->
        <article class="flex flex-col gap-5 sm:gap-6 w-full border border-primary/20 rounded-2xl p-4 sm:p-5">
            <div class="flex">
                <p
                    class="bg-primary-light/30 text-xs sm:text-sm text-primary px-3 sm:px-4 py-0.5 font-semibold rounded-md">
                    {{ imovelPage.imovel.value?.tipoDoImovel }}
                </p>
            </div>
            <div class="flex flex-col gap-3 sm:gap-4">
                <h3 class="text-primary-dark text-xl sm:text-2xl font-bold leading-snug">
                    {{ imovelPage.imovel.value?.titulo }}
                </h3>
                <div class="text-xs sm:text-sm flex flex-wrap gap-x-6 gap-y-1">
                    <p class="flex gap-1 items-center">
                        <MapPin :size="15" class="shrink-0" />
                        <span>
                            {{ imovelPage.imovel.value?.endereco.bairro }},
                            {{ imovelPage.imovel.value?.endereco.cidade }},
                            {{ imovelPage.imovel.value?.endereco.uf }}
                        </span>
                    </p>
                    <p class="text-primary-light cursor-pointer">Ver no mapa</p>
                </div>
            </div>
            <div class="flex flex-col gap-2">
                <h2 class="font-bold text-2xl sm:text-3xl text-primary-dark">
                    {{ formmater.moeda(imovelPage.imovel.value?.valorAluguel) }}
                    <span class="text-base sm:text-lg font-medium">/mês</span>
                </h2>
                <p class="text-sm sm:text-base text-text-muted">
                    {{ imovelPage.imovel.value?.descricao }}
                </p>
            </div>
            <ul class="w-full flex flex-wrap justify-between gap-y-3 gap-x-4 text-xs sm:text-sm">
                <li class="flex flex-col gap-1">
                    <span class="flex gap-2 items-center font-semibold">
                        <BedDouble :size="20" />
                        {{ imovelPage.imovel.value?.comodos }}
                    </span>
                    <p class="text-text-muted">cômodos</p>
                </li>
                <li class="flex flex-col gap-1">
                    <span class="flex gap-2 items-center font-semibold">
                        <Bath :size="20" />
                        {{ imovelPage.imovel.value?.banheiros }}
                    </span>
                    <p class="text-text-muted">banheiros</p>
                </li>
                <li class="flex flex-col gap-1">
                    <span class="flex gap-2 items-center font-semibold">
                        <Ruler :size="20" />
                        {{ formmater.metrosQuadrados(imovelPage.imovel.value?.metrosQuadrados) }}
                    </span>
                    <p class="text-text-muted">área útil</p>
                </li>
            </ul>
            <div class="flex flex-col gap-2">
                <AppButton variant="primary">
                    <span class="flex gap-2 items-center justify-center">
                        Conversar com locador
                        <MessageCircleMore :size="18" />
                    </span>
                </AppButton>
                <AppButton variant="outline">
                    <span class="flex gap-2 items-center justify-center">
                        Agendar visita
                        <MapPin :size="18" />
                    </span>
                </AppButton>
            </div>
            <div class="bg-primary-soft/80 flex flex-row gap-3 p-4 rounded-2xl">
                <ShieldCheck :size="36" class="shrink-0" />
                <div>
                    <p class="text-sm font-bold text-primary-dark">Negociação segura</p>
                    <p class="text-sm text-primary">Seus dados protegidos durante todo o processo</p>
                </div>
            </div>
        </article>
    </section>

</template>