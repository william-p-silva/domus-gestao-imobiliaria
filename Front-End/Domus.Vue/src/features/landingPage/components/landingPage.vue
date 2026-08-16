<script setup lang="ts">
import HeaderLandingPage from './headerLandingPage.vue';
import heroImg from '@/assets/imgs/LandingPage/contratoImovel.jpeg'
</script>



<template>
    <main class="h-screen w-screen">

        <section class="relative h-full w-full overflow-hidden bg-slate-50">

            <!-- Imagem do lado direito -->
            <img :src="heroImg" alt="Ilustração Domus" class="absolute right-0 top-0  w-[70%] object-cover" />

            <!-- Header por cima da imagem -->
            <HeaderLandingPage class="relative z-10" />

            <!-- Conteúdo -->
            <div class="relative z-10 flex h-full items-center ">

                <div class="ml-12 w-[45%] backdrop-blur-sm bg-white/20 rounded-full" >
                    <p class="text-sm text-blue-900">
                        O caminho para o seu novo lar começa aqui
                    </p>

                    <h1 class="mt-4 text-6xl font-bold text-blue-950">
                        Encontre o lar
                        <br />
                        perfeito para você.
                    </h1>

                    <p class="mt-6 max-w-lg text-lg text-slate-600">
                        A DOMUS conecta você aos melhores imóveis
                        para alugar com praticidade, segurança e confiança.
                    </p>
                </div>

            </div>

        </section>

    </main>
</template>