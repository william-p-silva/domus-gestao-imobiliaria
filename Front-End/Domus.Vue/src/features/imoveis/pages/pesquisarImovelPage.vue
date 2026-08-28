<script setup lang="ts">
import ImovelCard from '@/shared/components/cards/imovelCard.vue';
import FiltrosImoveis from '../components/filtro/filtrosImoveis.vue';
import { useImovel } from '../hooks/useImovel.ts';
import { onMounted, watch } from 'vue';
import { useFiltro } from '../hooks/filtro/useFiltro.ts';
import HeaderFiltro from '../components/filtro/headerFiltro.vue';



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
    <section class="flex">
        <FiltrosImoveis class="hidden lg:flex" />
        <article class="flex flex-col  w-full border-l border-primary/20">
          <HeaderFiltro />
          <article class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5 sm:gap-4 lg:gap-4 mx-auto py-4">
            <ImovelCard :imovel="imovelUnit" v-for="imovelUnit in imovel.imoveis.value" />

          </article>
            

        </article>
    </section>
</template>