<script setup lang="ts">
import { computed } from 'vue';
import type { TableAlign } from '../types/tableColumn';

interface Props {
    align?: TableAlign;
    width?: string;
    minWidth?: string;
    className?: string;
}

const props = withDefaults(defineProps<Props>(), {
    align: 'left',
    className: ''
});

const alignmentClass = computed(() => ({
    left: 'text-left',
    center: 'text-center',
    right: 'text-right'
}[props.align]));

const cellStyle = computed(() => ({
    width: props.width,
    minWidth: props.minWidth
}));
</script>

<template>
    <td
        :class="['px-4 py-3.5 align-middle text-sm text-primary-dark', alignmentClass, className]"
        :style="cellStyle"
    >
        <slot />
    </td>
</template>
