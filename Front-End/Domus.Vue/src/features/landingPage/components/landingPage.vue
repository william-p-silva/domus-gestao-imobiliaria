<script setup lang="ts">
import { ArrowRight, ShieldCheck } from '@lucide/vue';
import HeaderLandingPage from './headerLandingPage.vue';
import heroImg from '@/assets/imgs/LandingPage/contratoImovel.jpeg'
import HeroSectionLanding from './heroSectionLanding.vue';
import BuscaImovel from './buscaImovel.vue';
import ImovelCard from '@/shared/components/cards/imovelCard.vue';
import { useLanding } from '../hooks/useLanding.ts';
import PassoAPasso from './cards/passoAPasso.vue';


const landing = useLanding();
</script>



<template>
    <main class="flex flex-col gap-6 w-full overflow-hidden bg-background ">

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

        <section class=" w-full px-10">
            <BuscaImovel />
        </section>


        <section class="bg-white border-t border-primary-light/20 py-6 w-full px-6 flex flex-col gap-4">
            <div class="flex justify-between ">
                <h1 class="font-bold text-xl text-text ">Imóveis em destaque</h1>
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

        <section class="w-full px-4 sm:px-6 lg:px-8">
            <div class="flex flex-col w-full justify-center items-center text-center">
                <h1 class="text-primary text-xl sm:text-2xl font-bold">Como Funciona a DOMUS?</h1>
                <p class="text-text text-base sm:text-lg">Do primeiro clique ao seu lar</p>
            </div>
            <div class="flex flex-col md:flex-row justify-center items-center pt-4 pb-12 md:pb-20 gap-4 flex-wrap">
                <PassoAPasso v-for="passo in landing.passoAPasso" :key="passo.num" :passo="passo" />
            </div>
        </section>

        <section>
            <div>
                <h1>Porque escolher a DOMUS?</h1>
                <div>
                    cards
                </div>
            </div>
            <div>
                
            </div>
        </section>
    </main>
</template>