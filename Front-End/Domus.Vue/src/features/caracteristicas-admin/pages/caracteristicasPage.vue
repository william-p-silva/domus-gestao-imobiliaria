<script setup lang="ts">
import MainTitle from '@/shared/components/title/mainTitle.vue';
import CaracteristicasTable from '../components/tables/caracteristicasTable.vue';
import { useCaracteristicas } from '../hooks/useCaracteristicas';
import SimpleCard from '@/shared/components/cards/base/simpleCard.vue';
import { Building2, Check, X } from '@lucide/vue';



const {
    caracteristicas,
    estatisticas,

    filtroEstado,
    carregando,
    carregandoEstatisticas,

    erro,
    erroEstatisticas,

    total,

    carregar,
    carregarEstatisticas,
    alterarFiltro,
    recarregar
} = useCaracteristicas();
</script>








<template>
    <main class="px-4">
        <MainTitle title="Gerenciamento de Características"
            sub-title="Cadastre e administre as comodidades e atributos pré-definidos disponíveis para seleção rápida nos anúncios de locação e venda pelos proprietários parceiros."
            :mode-indicator="{
                color: 'primary',
                label: `${caracteristicas.filter((i) => i.estado === 'Ativa').length} características no catálogo`
            }" />
        <section v-if="!carregandoEstatisticas && estatisticas" class="grid grid-cols-4 px-4 gap-4 pb-4">
            <SimpleCard icon-variant="primary" :icon="Check" label="Ativas" :title="estatisticas.ativas.toString()"
                :description="`${estatisticas.porcentagemAtivas} %`" />

            <SimpleCard icon-variant="danger" :icon="X" label="Inativas" :title="estatisticas.inativas.toString()"
                :description="`Estão desativas`" />

            <SimpleCard icon-variant="primary" :icon="Building2" label="Total de imóveis com características"
                :title="estatisticas.totalImoveis.toString()" :description="`Imóveis usando características`" />

            <SimpleCard icon-variant="orange" :icon="Check" label="Mais Comun" :title="estatisticas.top.nome"
                :description="estatisticas.top.grupoCaracteristica.nome" />
        </section>
        <CaracteristicasTable class="px-4" :caracteristicas="caracteristicas"
            :editar-action="(i) => console.log(i.nome)" />
    </main>
</template>