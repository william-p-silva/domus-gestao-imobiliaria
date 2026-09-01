<script setup lang="ts">
import { useRoute } from 'vue-router';
import NavLinkImovel from '../components/navLinkImovel.vue';
import { usePageImovel } from '../hooks/usePageImovel.ts';
import { onMounted } from 'vue';
import MainImovel from '../components/mainImovel.vue';


const pageImovel = usePageImovel();

const id = pageImovel.route.query.imovel_id


onMounted(async () => {
    pageImovel.getImovelId();
})
</script>

<template>
    <NavLinkImovel  />
    <main class="px-4">
        <MainImovel /> 
    </main>
    oi imovel {{ id }}
</template>