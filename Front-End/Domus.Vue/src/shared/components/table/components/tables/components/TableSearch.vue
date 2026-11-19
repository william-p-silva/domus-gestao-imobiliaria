<script setup lang="ts">
interface Props {
    modelValue: string;
    placeholder?: string;
    disabled?: boolean;
}

withDefaults(defineProps<Props>(), {
    placeholder: 'Pesquisar...',
    disabled: false
});

const emit = defineEmits<{
    'update:modelValue': [value: string];
}>();
</script>

<template>
    <div class="flex w-full items-center gap-2 rounded-xl bg-blue-50/80 px-3 py-2.5 transition-colors focus-within:ring-2 focus-within:ring-primary/20">
        <span class="select-none text-sm text-primary-dark/60" aria-hidden="true">⌕</span>
        <input
            :value="modelValue"
            type="search"
            :placeholder="placeholder"
            :disabled="disabled"
            class="min-w-0 flex-1 bg-transparent text-sm text-primary-dark outline-none placeholder:text-slate-400 disabled:cursor-not-allowed"
            @input="emit('update:modelValue', ($event.target as HTMLInputElement).value)"
        />
        <button
            v-if="modelValue"
            type="button"
            class="rounded px-1.5 text-xs text-slate-500 hover:bg-blue-100 hover:text-primary-dark"
            aria-label="Limpar pesquisa"
            @click="emit('update:modelValue', '')"
        >
            ×
        </button>
    </div>
</template>
