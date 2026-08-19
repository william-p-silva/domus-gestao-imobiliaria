<script setup lang="ts">
import BuscaImovel from './buscaImovel.vue';
import { useLanding } from '../hooks/useLanding.ts';
import Comecar from './sections/comecar.vue';
import MainLanding from './sections/mainLanding.vue';
import Passos from './sections/passos.vue';
import BeneficiosLanding from './sections/beneficiosLanding.vue';
import ImoveisLanding from './sections/imoveisLanding.vue';

const landing = useLanding();
</script>



<template>
    <main class="flex flex-col gap-6 w-full overflow-hidden bg-background ">

        <MainLanding />

        <section class=" w-full px-10">
            <BuscaImovel />
        </section>


        <ImoveisLanding />

        <Passos />

        <BeneficiosLanding />

        <Comecar />
    </main>
</template>