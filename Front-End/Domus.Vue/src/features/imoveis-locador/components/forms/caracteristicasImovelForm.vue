
<script setup lang="ts">
import BaseForm from '@/shared/components/forms/templates/baseForm.vue';
import HeaderForm from '@/shared/components/forms/templates/headerForm.vue';
import { RulerDimensionLine } from '@lucide/vue';
import type { CadastrarImovelForm } from '../../hook/useCadastrarImovel';
import BaseNumberInput from '@/shared/components/inputs/base/BaseNumberInput.vue';
import BaseNumberField from '@/shared/components/inputs/base/BaseNumberField.vue';

const props = defineProps<CadastrarImovelForm>();
</script>

<template>
    <BaseForm>
        <HeaderForm
            description="Dimensões e comodidades que agregam valor perceptível ao imóvel"
            title="2. Características e dimensões"
            :icon="RulerDimensionLine"
        />

        <div class="flex flex-col gap-4">
            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <BaseNumberInput
                    v-model="requestCadastro.value.comodos"
                    label="Cômodos / Quartos"
                    required
                    :min="1"
                    :max="200"
                    description="Excluindo banheiros"
                    :error="errors.value.comodos"
                />

                <BaseNumberInput
                    v-model="requestCadastro.value.banheiros"
                    label="Banheiros"
                    required
                    :min="1"
                    :max="200"
                    description="Incluindo suítes e lavabo"
                    :error="errors.value.banheiros"
                />
            </div>

            <BaseNumberField
                v-model="requestCadastro.value.metrosQuadrados"
                label="Área útil"
                required
                :decimals="2"
                :min="0"
                description="Metragem privativa averbada"
                :error="errors.value.metrosQuadrados"
            >
                <template #right>
                    <span class="rounded-md bg-background px-2 py-0.5 text-xs font-semibold text-primary">
                        m²
                    </span>
                </template>
            </BaseNumberField>
        </div>
    </BaseForm>
</template>
