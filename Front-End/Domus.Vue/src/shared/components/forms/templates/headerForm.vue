<script setup lang="ts">
import type { Component } from 'vue';



interface Props {
    title: string,
    description?: string,
    icon: Component,
    sizeIcon?: number,
    strokeIcon?: number
}

const props = withDefaults(defineProps<Props>(),{
    sizeIcon: 21,
    strokeIcon: 2
});

</script>



<template>
    <div class="flex items-center gap-3 border-b-2 border-primary-soft/30 py-4">
        <div
            class="flex h-10 w-10 shrink-0 items-center justify-center
                   rounded-xl bg-indigo-100 text-indigo-900"
        >
            <component :is="icon" :size="sizeIcon" :stroke-width="strokeIcon" />
        </div>

        <div class="min-w-0">
            <h3 class="text-base font-semibold text-slate-900">
                {{ title }}
            </h3>

            <p
                v-if="description"
                class="text-sm text-slate-500"
            >
                {{ description }}
            </p>
        </div>
    </div>
</template>