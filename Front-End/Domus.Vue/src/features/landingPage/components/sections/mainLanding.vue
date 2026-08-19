<script setup lang="ts">
import HeaderLandingPage from '../headerLandingPage.vue';
import HeroSectionLanding from '../heroSectionLanding.vue';
import heroImg from '@/assets/imgs/LandingPage/contratoImovel.jpeg'

</script>





<template>
    <section class="relative h-full w-full overflow-hidden bg-slate-50  pb-6">
        <!-- Imagem do lado direito -->
        <img :src="heroImg" alt="Ilustração Domus" class="absolute right-0 top-0 w-full object-cover" />

        <!-- Gradiente sobre a imagem -->
        <div class="absolute inset-0 bg-linear-to-r
                from-white via-white/80 via-45% to-transparent" />

        <!-- Header por cima da imagem -->
        <HeaderLandingPage class="relative z-10" />

        <!-- Conteúdo -->
        <HeroSectionLanding class="relative z-10" />

    </section>
</template>