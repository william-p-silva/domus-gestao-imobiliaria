<script setup lang="ts">
import { ArrowLeft, ChevronRight, Heart, Share2 } from '@lucide/vue';
import { usePageImovel } from '../hooks/usePageImovel';


const pageImovel = usePageImovel();
</script>



<template>
    <nav class="w-full py-4 flex justify-between items-center">
        <div class="flex gap-4 sm:gap-8 justify-start w-full ">
            <RouterLink to="/imoveis" class="flex gap-2 text-primary justify-center items-center">
                <ArrowLeft class="" />
                <p class="hidden sm:flex">Voltar <span class="hidden md:flex">para Imóveis</span></p>
            </RouterLink >
            <ul class="flex gap-3 text-text-muted text-sm justify-center items-center">

                <RouterLink class="hover:text-primary hidden md:flex" to="/">
                    Inicio
                </RouterLink >

                <ChevronRight />

                <RouterLink class="hover:text-primary" to="/imoveis">
                    Imoveis
                </RouterLink>

                <ChevronRight />

                <RouterLink class="hover:text-primary" to="/imoveis">
                    {{ pageImovel.imovel.value?.endereco.cidade }}, 
                    {{ pageImovel.imovel.value?.endereco.uf }}
                </RouterLink >

                <ChevronRight class="hidden xl:flex" />

                <li class="hidden xl:flex">
                    {{ pageImovel.imovel.value?.titulo }}
                </li>
            </ul>
        </div>
        <div class="flex gap-4 ">
            <div class="flex gap-2 text-primary justify-center items-center">
                <Heart />
                <p class="hidden sm:flex">Favoritar</p>
            </div>
            <div class="gap-2 hidden sm:flex flex-row text-primary justify-center items-center">
                <Share2 />
                <p>Compartilhar</p>
            </div>
        </div>
    </nav>
</template>