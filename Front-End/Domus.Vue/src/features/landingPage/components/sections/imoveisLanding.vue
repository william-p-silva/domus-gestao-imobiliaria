<script setup lang="ts">
import ImovelCard from '@/shared/components/cards/imovelCard.vue';
import { ArrowRight } from '@lucide/vue';
import { useLanding } from '../../hooks/useLanding';

const landing = useLanding();

</script>


<template>
    <section class="bg-white border-t border-primary-light/20 py-6 w-full px-6 flex flex-col gap-4">
        <div class="flex justify-between ">
            <h1 class="font-bold text-xl text-primary ">Imóveis em destaque</h1>
            <div class="flex gap-3 text-primary">
                <p>Ver todos os imóveis </p>
                <ArrowRight />
            </div>
        </div>
        <div class="flex justify-center items-center w-full ">
            <div class="flex gap-4 w- overflow-x-auto pb-4">
                <ImovelCard :imovel="imovel" v-for="imovel in landing.imoveisFakes" />
            </div>
        </div>
    </section>
</template>