<script setup lang="ts">
import imgEncontrando from '@/assets/imgs/LandingPage/encontrandoImovel.jpeg'
import imgProcurando from '@/assets/imgs/LandingPage/procurandoImovel.jpeg'
import imgContratando from '@/assets/imgs/LandingPage/contratoImovel.jpeg'
import type { PassoPassoCard } from '../../types/common';


const props = defineProps<{
    passo: PassoPassoCard
}>();

const img = props.passo.img === 'contratando' ? imgContratando : props.passo.img === 'encontrar' ? imgEncontrando : imgProcurando

</script>



<template>
    <div class="rounded-2xl p-2 flex flex-col items-center md:items-start w-full max-w-xs sm:max-w-sm">
        <div class="w-10 h-10 rounded-full bg-primary flex justify-center items-center text-white font-bold">
            0{{ passo.num }}
        </div>
        <div class="flex flex-col justify-self-start w-full text-center md:text-left">
            <h3 class="text-text font-bold">{{ passo.title }}</h3>
            <p class="text-primary-light">
                {{ passo.text }}
            </p>
        </div>
        <div
            class="w-full sm:w-72 md:w-80 lg:w-96 h-48 sm:h-64 md:h-80 bg-cover  rounded-2xl mt-2"
            :style="{ backgroundImage: `url(${img})` }"
        />
    </div>
</template>