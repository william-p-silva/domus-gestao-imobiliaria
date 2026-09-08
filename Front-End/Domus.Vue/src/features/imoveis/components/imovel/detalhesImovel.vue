<script setup lang="ts">
import { Bath, BedDouble, MapPin, MessageCircleMore, Ruler, ShieldCheck } from '@lucide/vue';

import { FormatterNumber } from '@/shared/utils/formatter/formatterNumber';
import { usePageImovel } from '../../hooks/usePageImovel';
import AppButton from '@/shared/components/buttons/appButton.vue';


const { imovel, createChatImovel } = usePageImovel();

const formmater = FormatterNumber();

async function handleChat(){
    await createChatImovel();
}
</script>




<template>
    <article class="flex flex-col gap-5 sm:gap-6 w-full border border-primary/20 rounded-2xl p-4 sm:p-5">
        <div class="flex">
            <p class="bg-primary-light/30 text-xs sm:text-sm text-primary px-3 sm:px-4 py-0.5 font-semibold rounded-md">
                {{ imovel?.tipoDoImovel }}
            </p>
        </div>
        <div class="flex flex-col gap-3 sm:gap-4">
            <h3 class="text-primary-dark text-xl sm:text-2xl font-bold leading-snug">
                {{ imovel?.titulo }}
            </h3>
            <div class="text-xs sm:text-sm flex flex-wrap gap-x-6 gap-y-1">
                <p class="flex gap-1 items-center">
                    <MapPin :size="15" class="shrink-0" />
                    <span>
                        {{ imovel?.endereco.bairro }},
                        {{ imovel?.endereco.cidade }},
                        {{ imovel?.endereco.uf }}
                    </span>
                </p>
                <p class="text-primary-light cursor-pointer">Ver no mapa</p>
            </div>
        </div>
        <div class="flex flex-col gap-2">
            <h2 class="font-bold text-2xl sm:text-3xl text-primary-dark">
                {{ formmater.moeda(imovel?.valorAluguel) }}
                <span class="text-base sm:text-lg font-medium">/mês</span>
            </h2>
            <p class="text-sm sm:text-base text-text-muted">
                {{ imovel?.descricao }}
            </p>
        </div>
        <ul class="w-full flex flex-wrap justify-between gap-y-3 gap-x-4 text-xs sm:text-sm">
            <li class="flex flex-col gap-1">
                <span class="flex gap-2 items-center font-semibold">
                    <BedDouble :size="20" />
                    {{ imovel?.comodos }}
                </span>
                <p class="text-text-muted">cômodos</p>
            </li>
            <li class="flex flex-col gap-1">
                <span class="flex gap-2 items-center font-semibold">
                    <Bath :size="20" />
                    {{ imovel?.banheiros }}
                </span>
                <p class="text-text-muted">banheiros</p>
            </li>
            <li class="flex flex-col gap-1">
                <span class="flex gap-2 items-center font-semibold">
                    <Ruler :size="20" />
                    {{ formmater.metrosQuadrados(imovel?.metrosQuadrados) }}
                </span>
                <p class="text-text-muted">área útil</p>
            </li>
        </ul>
        <div class="flex flex-col gap-2">
            <AppButton variant="primary" :action="handleChat">
                <span class="flex gap-2 items-center justify-center">
                    Conversar com locador
                    <MessageCircleMore :size="18" />
                </span>
            </AppButton>
            <AppButton variant="outline" :action="handleChat">
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
</template>