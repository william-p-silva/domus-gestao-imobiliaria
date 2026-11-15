<script setup lang="ts">
import MainTitle from '@/shared/components/title/mainTitle.vue';
import { useCadastrarImovel } from '../hook/useCadastrarImovel';
import BaseForm from '@/shared/components/forms/templates/baseForm.vue';
import HeaderForm from '@/shared/components/forms/templates/headerForm.vue';
import { Building2 } from '@lucide/vue';
import InputText from '@/shared/components/inputs/base/inputText.vue';
import BaseTextArea from '@/shared/components/inputs/base/baseTextArea.vue';
import BaseSelect from '@/shared/components/inputs/base/select/BaseSelect.vue';
import { DEFAULT_STATUSOPTIONS_IMOVEIS, DEFAULT_TIPOS_IMOVEIS, DEFAULT_TITLE } from '../config/cadastrarImovelConfig';

const { 
    errors, 
    requestCadastro,
    enviar
} = useCadastrarImovel();

</script>


<template>
    <MainTitle :title="DEFAULT_TITLE.title"
        :subTitle="DEFAULT_TITLE.subTitle"
        :modeIndicator="DEFAULT_TITLE.modeIndicator" />
    <article class="px-4">
        <BaseForm>
            <HeaderForm title="1. Informações do imóvel"
                description="Dados principais para identificação do anúncio nas buscas DOMUS" :icon="Building2" />

            <InputText v-model="requestCadastro.titulo" label="Título do imóvel" required :max-caracteres="200"
                placeholder="Ex: Apartamento de alto padrão"
                description="Destaque os principais atrativos: localização nobre, vista e padrão arquitetônico."
                :error="errors.titulo" />

            <BaseTextArea v-model="requestCadastro.descricao" label="Descrição do imóvel" required :min-height="100"
                :max-caracteres="1000" placeholder="Excelente residência com iluminação natural abundante..."
                description="Conte detalhes sobre ambientes, acabamento e diferenciais." :error="errors.descricao" />
            <div class="flex justify-between gap-4">

                <BaseSelect v-model="requestCadastro.tipoDoImovel" label="Tipo do imóvel" required
                    description="Selecione o tipo do seu imóvel" :options="DEFAULT_TIPOS_IMOVEIS"
                    placeholder="Selecione o tipo" :error="errors.tipoDoImovel" />

                <BaseSelect v-model="requestCadastro.status" label="Status de disponibilidade" required
                    description="Selecione a disponibilidade do imóvel" :options="DEFAULT_STATUSOPTIONS_IMOVEIS"
                    :error="errors.status" />
            </div>

            <button v-on:click="enviar">
                text-base
            </button>
        </BaseForm>

    </article>
</template>