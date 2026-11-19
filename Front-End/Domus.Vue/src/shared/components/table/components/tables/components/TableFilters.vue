<script setup lang="ts">
import type { TableFilterOption, TableFilterValue } from '../types/tableFilter';

interface Props {
    options: TableFilterOption[];
    modelValue?: TableFilterValue | null;
}

defineProps<Props>();

const emit = defineEmits<{
    'update:modelValue': [value: TableFilterValue | undefined];
}>();
</script>

<template>
    <div class="inline-flex max-w-full flex-wrap items-center gap-1 rounded-xl bg-blue-50/80 p-1" role="group" aria-label="Filtros da tabela">
        <button
            v-for="option in options"
            :key="option.value"
            type="button"
            :aria-pressed="modelValue === option.value"
            :class="[
                'inline-flex items-center gap-1.5 rounded-lg px-3 py-2 text-xs font-medium transition-colors',
                modelValue === option.value
                    ? 'bg-white text-primary-dark shadow-sm'
                    : 'text-slate-500 hover:bg-white/70 hover:text-primary-dark'
            ]"
            @click="emit('update:modelValue', modelValue === option.value ? undefined : option.value)"
        >
            {{ option.label }}
            <span v-if="option.count !== undefined" class="text-[10px] opacity-70">
                ({{ option.count }})
            </span>
        </button>
    </div>
</template>
