<script setup lang="ts">
import { useRoute } from 'vue-router';
import NavLinkImovel from '../components/navLinkImovel.vue';
import { usePageImovel } from '../hooks/usePageImovel.ts';
import { onMounted } from 'vue';
import ImovelNaoEncontrado from '../components/ImovelNaoEncontrado.vue';
import PageLoading from '@/shared/components/loading/pageLoading.vue';
import MainImovel from '../components/imovel/mainImovel.vue';


const {
        isLoading,
        erro,
        imovel,
        route,
        getImovelId
    } = usePageImovel();

const id = route.query.imovel_id


onMounted(async () => {
    await getImovelId();
})
</script>

<template>
    <PageLoading :visible="isLoading" />
    <main v-if="imovel" class="px-4">
        <NavLinkImovel />
        <MainImovel />
        oi imovel {{ id }}
    </main>
    <main v-else >
        <ImovelNaoEncontrado />
    </main>
</template>