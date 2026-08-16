<script setup lang="ts">
import { ShieldCheck } from '@lucide/vue';
import HeaderLandingPage from './headerLandingPage.vue';
import heroImg from '@/assets/imgs/LandingPage/contratoImovel.jpeg'
import HeroSectionLanding from './heroSectionLanding.vue';
</script>



<template>
    <main class="h-screen w-screen">

        <section class="relative h-full w-full overflow-hidden bg-slate-50">

            <!-- Imagem do lado direito -->
            <img :src="heroImg" alt="Ilustração Domus" class="absolute right-0 top-0  w-[80%] object-cover" />

            <!-- Gradiente sobre a imagem -->
            <div class="absolute inset-0 bg-linear-to-r
               from-white via-white/80 via-45% to-transparent"></div>

            <!-- Header por cima da imagem -->
            <HeaderLandingPage class="relative z-10" />

            <!-- Conteúdo -->
            <HeroSectionLanding class="relative z-10" />


        </section>

    </main>
</template>