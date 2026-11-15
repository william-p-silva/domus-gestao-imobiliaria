
<script setup lang="ts">
import BaseForm from '@/shared/components/forms/templates/baseForm.vue';
import HeaderForm from '@/shared/components/forms/templates/headerForm.vue';
import BaseTextArea from '@/shared/components/inputs/base/baseTextArea.vue';
import InputText from '@/shared/components/inputs/base/inputText.vue';
import BaseSelect from '@/shared/components/inputs/base/select/BaseSelect.vue';
import { Building2 } from '@lucide/vue';
import type { CadastrarImovelForm } from '../../hook/useCadastrarImovel';
import {
    DEFAULT_STATUSOPTIONS_IMOVEIS,
    DEFAULT_TIPOS_IMOVEIS
} from '../../config/cadastrarImovelConfig';

const props = defineProps<CadastrarImovelForm>();
</script>

<template>
    <BaseForm>
        <HeaderForm
            title="1. Informações do imóvel"
            description="Dados principais para identificação do anúncio nas buscas DOMUS"
            :icon="Building2"
        />

        <div class="flex flex-col gap-4">
            <InputText
                v-model="requestCadastro.value.titulo"
                label="Título do imóvel"
                required
                :max-caracteres="200"
                placeholder="Ex: Apartamento de alto padrão"
                description="Destaque os principais atrativos: localização nobre, vista e padrão arquitetônico."
                :error="errors.value.titulo"
            />

            <BaseTextArea
                v-model="requestCadastro.value.descricao"
                label="Descrição do imóvel"
                required
                :min-height="100"
                :max-caracteres="1000"
                placeholder="Excelente residência com iluminação natural abundante..."
                description="Conte detalhes sobre ambientes, acabamento e diferenciais."
                :error="errors.value.descricao"
            />

            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <BaseSelect
                    v-model="requestCadastro.value.tipoDoImovel"
                    label="Tipo do imóvel"
                    required
                    description="Selecione o tipo do seu imóvel"
                    :options="DEFAULT_TIPOS_IMOVEIS"
                    placeholder="Selecione o tipo"
                    :error="errors.value.tipoDoImovel"
                />

                <BaseSelect
                    v-model="requestCadastro.value.status"
                    label="Status de disponibilidade"
                    required
                    description="Selecione a disponibilidade do imóvel"
                    :options="DEFAULT_STATUSOPTIONS_IMOVEIS"
                    :error="errors.value.status"
                />
            </div>
        </div>
    </BaseForm>
</template>
