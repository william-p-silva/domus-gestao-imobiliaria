<script setup lang="ts">
import { useLanding } from '../../hooks/useLanding';
import PassoAPasso from '../cards/passoAPasso.vue';

const landing = useLanding();
</script>

<template>
    <section class="w-full px-4 sm:px-6 lg:px-8">
        <div class="flex flex-col w-full justify-center items-center text-center">
            <h1 class="text-primary text-xl sm:text-2xl font-bold">Como Funciona a DOMUS?</h1>
            <p class="text-text text-base sm:text-lg">Do primeiro clique ao seu lar</p>
        </div>
        <div class="flex flex-col md:flex-row justify-center items-center pt-4 gap-4 flex-wrap">
            <PassoAPasso v-for="passo in landing.passoAPasso" :key="passo.num" :passo="passo" />
        </div>
    </section>
</template>