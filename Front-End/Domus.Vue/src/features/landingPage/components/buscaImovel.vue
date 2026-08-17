<script setup lang="ts">
import { useLanding } from '../hooks/useLanding.ts';
import InputBusca from './inputBusca.vue';
const landing = useLanding()
</script>




<template>
    <div class="grid grid-cols-4 justify-between items-center gap-4 px-10 py-4 bg-background rounded-2xl border border-primary-dark/20 shadow-lg shadow-primary/25">
        <InputBusca v-for="infos in landing.infosBuscaImovel" :infos="infos" />


        <div class="flex flex-col pt-5.5">
            <button class="p-4 bg-primary text-white font-bold cursor-pointer rounded-2xl">
                Buscar
            </button>
        </div>
    </div>
</template>