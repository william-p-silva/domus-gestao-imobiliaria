<script setup lang="ts" generic="T extends object">
import type { TableColumn, TableSortDirection } from '../types/tableColumn';

interface Props {
    columns: TableColumn<T>[];
    hasActions?: boolean;
    actionsLabel?: string;
    sortBy?: string;
    sortDirection?: TableSortDirection;
}

withDefaults(defineProps<Props>(), {
    hasActions: false,
    actionsLabel: 'Ações',
    sortDirection: 'asc'
});

const emit = defineEmits<{
    sort: [key: string];
}>();

function visibleColumns(columns: TableColumn<T>[]) {
    return columns.filter((column) => !column.hidden);
}
</script>

<template>
    <thead class="bg-blue-50/70">
        <tr class="border-b border-blue-100">
            <th
                v-for="column in visibleColumns(columns)"
                :key="column.key"
                scope="col"
                :style="{ width: column.width, minWidth: column.minWidth }"
                :class="[
                    'px-4 py-4 text-xs font-semibold uppercase tracking-wide text-primary-dark/80',
                    column.align === 'center' ? 'text-center' : '',
                    column.align === 'right' ? 'text-right' : '',
                    column.align === 'left' || !column.align ? 'text-left' : '',
                    column.headerClassName
                ]"
            >
                <button
                    v-if="column.sortable"
                    type="button"
                    class="inline-flex items-center gap-1.5 transition-colors hover:text-primary"
                    :aria-label="`Ordenar por ${column.label}`"
                    @click="emit('sort', column.key)"
                >
                    {{ column.label }}
                    <span class="text-[10px]" aria-hidden="true">
                        {{ sortBy === column.key ? (sortDirection === 'asc' ? '↑' : '↓') : '↕' }}
                    </span>
                </button>
                <span v-else>{{ column.label }}</span>
            </th>

            <th
                v-if="hasActions"
                scope="col"
                class="w-px whitespace-nowrap px-4 py-4 text-right text-xs font-semibold uppercase tracking-wide text-primary-dark/80"
            >
                {{ actionsLabel }}
            </th>
        </tr>
    </thead>
</template>
