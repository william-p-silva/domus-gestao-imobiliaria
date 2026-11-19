<script setup lang="ts" generic="T extends object">
import { computed } from 'vue';
import type { TableAction } from '../types/tableAction';

interface Props {
    item: T;
    actions?: TableAction<T>[];
}

const props = withDefaults(defineProps<Props>(), {
    actions: () => []
});

const visibleActions = computed(() =>
    props.actions.filter((action) => action.visible?.(props.item) ?? true)
);

function isDisabled(action: TableAction<T>): boolean {
    return action.disabled?.(props.item) ?? false;
}

const variantClasses = {
    default: 'text-slate-500 hover:bg-slate-100 hover:text-primary-dark',
    primary: 'text-primary hover:bg-primary-soft',
    danger: 'text-red-500 hover:bg-red-50'
} as const;
</script>

<template>
    <div v-if="visibleActions.length" class="flex items-center justify-end gap-1.5">
        <button
            v-for="action in visibleActions"
            :key="action.key"
            type="button"
            :aria-label="action.label"
            :title="action.label"
            :disabled="isDisabled(action)"
            :class="[
                'inline-flex size-8 shrink-0 items-center justify-center rounded-lg transition-colors',
                variantClasses[action.variant ?? 'default'],
                isDisabled(action) ? 'cursor-not-allowed opacity-40' : 'cursor-pointer'
            ]"
            @click="action.onClick(item)"
        >
            <component v-if="action.icon" :is="action.icon" :size="16" />
            <span v-else class="text-xs font-medium">{{ action.label }}</span>
        </button>
    </div>
</template>
