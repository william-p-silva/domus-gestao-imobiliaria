<script setup lang="ts">
import { useRoute } from 'vue-router';
import NavLinkImovel from '../components/navLinkImovel.vue';
import { usePageImovel } from '../hooks/usePageImovel.ts';
import { onMounted } from 'vue';
import MainImovel from '../components/mainImovel.vue';
import ImovelNaoEncontrado from '../components/ImovelNaoEncontrado.vue';


const pageImovel = usePageImovel();

const id = pageImovel.route.query.imovel_id


onMounted(async () => {
    pageImovel.getImovelId();
})
</script>

<template>
    <main v-if="pageImovel.imovel.value" class="px-4">
        <NavLinkImovel />
        <MainImovel />
        oi imovel {{ id }}
    </main>
    <main v-else >
        <ImovelNaoEncontrado />
    </main>
</template>