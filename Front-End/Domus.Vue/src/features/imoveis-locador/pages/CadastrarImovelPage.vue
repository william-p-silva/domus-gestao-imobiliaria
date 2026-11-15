<script setup lang="ts">
import MainTitle from '@/shared/components/title/mainTitle.vue';
import { useCadastrarImovel } from '../hook/useCadastrarImovel';
import { DEFAULT_TITLE } from '../config/cadastrarImovelConfig';
import InfosImovelForm from '../components/forms/infosImovelForm.vue';
import CaracteristicasImovelForm from '../components/forms/caracteristicasImovelForm.vue';
import ComercilImovelForm from '../components/forms/comercilImovelForm.vue';
import EnderecoImovelForm from '../components/forms/enderecoImovelForm.vue';
import BaseButton from '@/shared/components/inputs/buttons/baseButton.vue';
import { Bookmark, Check } from '@lucide/vue';

const cadastro = useCadastrarImovel();

</script>


<template>
    <MainTitle :title="DEFAULT_TITLE.title" :subTitle="DEFAULT_TITLE.subTitle"
        :modeIndicator="DEFAULT_TITLE.modeIndicator" />
    <article class="grid grid-cols-2  px-4 pb-4">
        <div class="flex flex-col gap-4">
            <InfosImovelForm v-bind="cadastro" />
            <CaracteristicasImovelForm v-bind="cadastro" />
            <ComercilImovelForm v-bind="cadastro" />
            <EnderecoImovelForm v-bind="cadastro" />

            <div class="flex justify-between border-t-2 border-text-muted/20 pt-4">
                <BaseButton variant="outlined">
                    <span class="flex gap-4 justify-center items-center">
                        <Bookmark :size="20" /> Cadastrar
                    </span>
                </BaseButton>
                <div class="flex gap-4">
                    <BaseButton variant="outlined">Cancelar</BaseButton>
                    <BaseButton variant="primary" type="button" :action="cadastro.enviar">
                        <span class="flex gap-4 justify-center items-center">
                            <Check /> Cadastrar
                        </span>
                    </BaseButton>
                </div>
            </div>
        </div>
    </article>
</template>