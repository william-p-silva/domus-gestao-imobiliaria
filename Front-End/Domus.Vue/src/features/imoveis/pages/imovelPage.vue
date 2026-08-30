<script setup lang="ts">
import { useRoute } from 'vue-router';
import NavLinkImovel from '../components/navLinkImovel.vue';
import { usePageImovel } from '../hooks/usePageImovel.ts';
import { onMounted } from 'vue';


const pageImovel = usePageImovel();

const id = pageImovel.route.query.imovel_id


onMounted(async () => {
    pageImovel.getImovelId();
})
</script>

<template>
    <NavLinkImovel  />
    oi imovel {{ id }}
</template>