
<script setup lang="ts">
import BaseForm from '@/shared/components/forms/templates/baseForm.vue';
import type { CadastrarImovelForm } from '../../hook/useCadastrarImovel';
import HeaderForm from '@/shared/components/forms/templates/headerForm.vue';
import { Banknote } from '@lucide/vue';
import BaseNumberField from '@/shared/components/inputs/base/BaseNumberField.vue';

const props = defineProps<CadastrarImovelForm>();
</script>

<template>
    <BaseForm>
        <HeaderForm
            title="3. Informações comerciais"
            description="Condições financeiras para o contrato de locação"
            :icon="Banknote"
        />

        <div class="w-full min-w-0">
            <BaseNumberField
                v-model="requestCadastro.value.valorAluguel"
                label="Valor do aluguel"
                required
                size="lg"
                :decimals="2"
                :min="0"
                description="Valor base do aluguel mensal (não inclui taxas condominiais nem parcela de IPTU aplicável)."
                :error="errors.value.valorAluguel"
            >
                <template #left>
                    <span class="text-lg font-semibold">R$</span>
                </template>

                <template #right>
                    <span class="text-sm">/mês</span>
                </template>
            </BaseNumberField>
        </div>
    </BaseForm>
</template>
