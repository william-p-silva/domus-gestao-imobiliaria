<script setup lang="ts" generic="T extends object">
import { computed } from 'vue';
import type { TableColumn } from '../types/tableColumn';
import type { TableAction } from '../types/tableAction';
import TableCell from './TableCell.vue';
import TableActions from '../components/TableActions.vue';

interface Props {
    item: T;
    columns: TableColumn<T>[];
    actions?: TableAction<T>[];
    selected?: boolean;
    rowClass?: string;
}

const props = withDefaults(defineProps<Props>(), {
    actions: () => [],
    selected: false,
    rowClass: ''
});

const visibleColumns = computed(() => props.columns.filter((column) => !column.hidden));

function getValue(column: TableColumn<T>): unknown {
    return (props.item as Record<string, unknown>)[column.key];
}

function getDisplayValue(column: TableColumn<T>): string | number {
    const value = getValue(column);

    if (column.format) return column.format(value, props.item);
    if (value === null || value === undefined || value === '') return '—';
    if (typeof value === 'string' || typeof value === 'number') return value;
    if (typeof value === 'boolean') return value ? 'Sim' : 'Não';
    return '—';
}

function slotName(column: TableColumn<T>): string {
    return `cell-${column.slot ?? column.key}`;
}
</script>

<template>
    <tr
        :class="[
            'border-b border-blue-100/80 transition-colors last:border-b-0 hover:bg-slate-50/70',
            selected ? 'bg-primary-soft/40' : '',
            rowClass
        ]"
        :aria-selected="selected"
    >
        <TableCell
            v-for="column in visibleColumns"
            :key="column.key"
            :align="column.align"
            :width="column.width"
            :min-width="column.minWidth"
            :class-name="column.className"
        >
            <slot
                :name="slotName(column)"
                :item="item"
                :value="getValue(column)"
                :column="column"
            >
                {{ getDisplayValue(column) }}
            </slot>
        </TableCell>

        <TableCell v-if="actions.length > 0" align="right" class-name="whitespace-nowrap">
            <TableActions :item="item" :actions="actions" />
        </TableCell>
    </tr>
</template>
