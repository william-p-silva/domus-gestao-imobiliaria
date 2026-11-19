<script setup lang="ts">
interface Props {
    showing: number;
    total: number;
    page: number;
    totalPages: number;
    itemLabel?: string;
}

withDefaults(defineProps<Props>(), {
    itemLabel: 'registros'
});

const emit = defineEmits<{
    previous: [];
    next: [];
}>();
</script>

<template>
    <footer class="flex flex-col gap-3 border-t border-blue-100 px-4 py-3.5 sm:flex-row sm:items-center sm:justify-between sm:px-5">
        <p class="text-sm text-slate-500">
            Mostrando <span class="font-medium text-primary-dark">{{ showing }}</span>
            de <span class="font-medium text-primary-dark">{{ total }}</span>
            {{ itemLabel }}
        </p>

        <nav class="flex items-center justify-end gap-1.5" aria-label="Paginação da tabela">
            <button
                type="button"
                class="rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-primary-dark transition-colors hover:bg-blue-100 disabled:cursor-not-allowed disabled:opacity-40"
                :disabled="page <= 1"
                @click="emit('previous')"
            >
                Anterior
            </button>

            <span class="min-w-7 rounded-md bg-primary px-2 py-1.5 text-center text-xs font-semibold text-white" aria-current="page">
                {{ page }}
            </span>

            <button
                type="button"
                class="rounded-md bg-blue-50 px-3 py-1.5 text-xs font-medium text-primary-dark transition-colors hover:bg-blue-100 disabled:cursor-not-allowed disabled:opacity-40"
                :disabled="page >= totalPages"
                @click="emit('next')"
            >
                Próximo
            </button>
        </nav>
    </footer>
</template>
