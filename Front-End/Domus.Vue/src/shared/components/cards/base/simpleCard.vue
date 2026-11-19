
<script setup lang="ts">
import { computed, type Component } from 'vue';

import BaseCard from '../templates/baseCard.vue';
import { SIMPLE_CARD_ICON_STYLES, type SimpleCardIconVariant } from '../constants/simpleCardIconVariant.ts';



interface Props {
    title: string;
    label: string;
    description: string;
    icon: Component;
    iconVariant?: SimpleCardIconVariant;
}

const props = withDefaults(defineProps<Props>(), {
    iconVariant: 'primary'
});

const iconStyle = computed(() =>
    SIMPLE_CARD_ICON_STYLES[props.iconVariant]
);
</script>

<template>
    <BaseCard>
        <div class="flex items-center justify-between gap-4">
            <!-- Informações -->
            <div class="min-w-0 flex-1">
                <p class="text-xs font-medium text-slate-500">
                    {{ label }}
                </p>

                <h3 class="text-2xl font-semibold text-primary-dark">
                    {{ title }}
                </h3>

                <p class="text-sm text-slate-500">
                    {{ description }}
                </p>
            </div>

            <!-- Ícone -->
            <div
                class="flex size-12 shrink-0 items-center justify-center rounded-xl"
                :class="iconStyle.background"
            >
                <component
                    :is="props.icon"
                    :size="22"
                    :class="iconStyle.color"
                />
            </div>
        </div>
    </BaseCard>
</template>
