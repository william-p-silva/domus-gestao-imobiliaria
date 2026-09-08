<script setup lang="ts">
import { usePageImovel } from '../../hooks/usePageImovel.js';
import { FormatterNumber } from '@/shared/utils/formatter/formatterNumber';
import ImagensImovel from './imagensImovel.vue';
import DetalhesImovel from './detalhesImovel.vue';

const imovelPage = usePageImovel();
const formmater = FormatterNumber();
const nums = 5
</script>

<template>

    <section class="w-full flex flex-col lg:flex-row gap-4 lg:gap-6 text-primary">
        <!-- Coluna de imagens -->
        <ImagensImovel :imgsUrl="nums" />

        <!-- Coluna de detalhes -->
        <DetalhesImovel />
    </section>

</template>