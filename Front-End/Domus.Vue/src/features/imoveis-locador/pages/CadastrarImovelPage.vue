<script setup lang="ts">
import MainTitle from '@/shared/components/title/mainTitle.vue';
import { useCadastrarImovel } from '../hook/useCadastrarImovel';
import BaseForm from '@/shared/components/forms/templates/baseForm.vue';
import HeaderForm from '@/shared/components/forms/templates/headerForm.vue';
import { House } from '@lucide/vue';

const {
    isLoading,
    error,
    requestCadastro,
} = useCadastrarImovel();

</script>


<template>
    <MainTitle 
        title="Anunciar Imóvel"
        subTitle="Cadastre seu imóvel e encontre pessoas interessadas em um novo lar com segurança e agilidade institucional."
        :modeIndicator="{
            color: 'primarySoft',
            label: 'Modo de Criação Guiada DOMUS'
        }" />

        <BaseForm >
            <HeaderForm 
            title="1. Informações do imóvel"
            description="Dados principais para identificação do anúncio nas buscas DOMUS" 
            :icon="House"/>
            <p>teste</p>
        </BaseForm>
</template>