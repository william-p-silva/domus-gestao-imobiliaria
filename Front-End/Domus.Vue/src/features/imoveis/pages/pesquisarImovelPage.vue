<script setup lang="ts">
import ImovelCard from '@/shared/components/cards/imovelCard.vue';
import FiltrosImoveis from '../components/filtro/filtrosImoveis.vue';
import { useImovel } from '../hooks/useImovel.ts';
import { onMounted, watch } from 'vue';
import { useFiltro } from '../hooks/filtro/useFiltro.ts';



const imovel = useImovel();
const filtroStore = useFiltro();

async function carregarImoveis() {
  if (imovel.temFiltroAtivo()) {
    filtroStore.carregarFiltroDaUrl();
    const filtroData = imovel.getFiltroUrl();
    await imovel.setImoveis(filtroData);
  } else {
    await imovel.setImoveisNotFiltro();
  }
}

onMounted(async () => {
  await carregarImoveis();
});

// Atualiza automaticamente os imóveis quando a URL/Query mudar
watch(
  () => imovel.route.query,
  async () => {
    await carregarImoveis();
  }
);
</script>




<template>
    <section class="flex ">
        <FiltrosImoveis class="hidden lg:flex" />
        <article class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 space-x-6 space-y-6 mx-auto py-4">
            <ImovelCard :imovel="imovelUnit" v-for="imovelUnit in imovel.imoveis.value" />
            

        </article>
    </section>
</template>