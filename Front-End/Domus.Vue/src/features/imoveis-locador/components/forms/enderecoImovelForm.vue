
<script setup lang="ts">
import BaseForm from '@/shared/components/forms/templates/baseForm.vue';
import type { CadastrarImovelForm } from '../../hook/useCadastrarImovel';
import HeaderForm from '@/shared/components/forms/templates/headerForm.vue';
import { MapPin } from '@lucide/vue';
import InputText from '@/shared/components/inputs/base/inputText.vue';
import BaseButton from '@/shared/components/inputs/buttons/baseButton.vue';

const props = defineProps<CadastrarImovelForm>();
</script>

<template>
    <BaseForm>
        <HeaderForm
            title="4. Endereço do imóvel (Localização)"
            description="Localização precisa para exibição cartográfica e futuros locatários"
            :icon="MapPin"
        />

        <div class="flex flex-col gap-4">
            <!-- CEP -->
            <div class="flex flex-col sm:flex-row justify-center gap-3 items-center">
                <InputText
                    description="Não é necessário colocar barra (-)"
                    label="CEP"
                    required
                    placeholder="00000-000"
                    v-model="requestCadastro.value.endereco.cep"
                    :error="errors.value.endereco?.cep"
                />

                <BaseButton variant="secondary" class="mt-1 w-full sm:max-w-30">
                    Buscar
                </BaseButton>
            </div>

            <!-- Logradouro e número -->
            <div class="grid grid-cols-1 sm:grid-cols-[minmax(0,3fr)_minmax(0,1fr)] gap-4">
                <InputText
                    required
                    label="Logradouro / Rua"
                    placeholder="Rua dos Bobos"
                    v-model="requestCadastro.value.endereco.rua"
                    :error="errors.value.endereco?.rua"
                />

                <InputText
                    required
                    label="Número"
                    placeholder="0"
                    v-model="requestCadastro.value.endereco.numero"
                    :error="errors.value.endereco?.numero"
                />
            </div>

            <!-- Complemento e bairro -->
            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <InputText
                    label="Complemento"
                    placeholder="Apto 0 - Bloco 0"
                    v-model="requestCadastro.value.endereco.complemento"
                    :error="errors.value.endereco?.complemento"
                />

                <InputText
                    required
                    label="Bairro"
                    placeholder="Bairro dos Bobos"
                    v-model="requestCadastro.value.endereco.bairro"
                    :error="errors.value.endereco?.bairro"
                />
            </div>

            <!-- Cidade e estado -->
            <div class="grid grid-cols-1 sm:grid-cols-[minmax(0,3fr)_minmax(0,1fr)] gap-4">
                <InputText
                    required
                    label="Cidade"
                    placeholder="Cidade dos Bobos"
                    v-model="requestCadastro.value.endereco.cidade"
                    :error="errors.value.endereco?.cidade"
                />

                <InputText
                    required
                    label="Estado (UF)"
                    placeholder="SP"
                    v-model="requestCadastro.value.endereco.uf"
                    :error="errors.value.endereco?.uf"
                />
            </div>
        </div>
    </BaseForm>
</template>
