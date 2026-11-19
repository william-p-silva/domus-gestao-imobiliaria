<script setup lang="ts">
interface Props {
    empty?: boolean;
    loading?: boolean;
    emptyMessage?: string;
    loadingMessage?: string;
    colspan?: number;
    minWidth?: string;
}

withDefaults(defineProps<Props>(), {
    empty: false,
    loading: false,
    emptyMessage: 'Nenhum registro encontrado.',
    loadingMessage: 'Carregando registros...',
    colspan: 1,
    minWidth: '760px'
});
</script>

<template>
    <div class="w-full overflow-x-auto">
        <table class="w-full border-collapse text-left" :style="{ minWidth }">
            <slot name="header" />

            <tbody>
                <tr v-if="loading || empty">
                    <td
                        :colspan="Math.max(1, colspan)"
                        class="px-6 py-12 text-center text-sm text-slate-500"
                        role="status"
                    >
                        {{ loading ? loadingMessage : emptyMessage }}
                    </td>
                </tr>
                <slot v-else />
            </tbody>
        </table>
    </div>
</template>
