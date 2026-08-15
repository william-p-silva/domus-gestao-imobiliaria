<script setup lang="ts">
withDefaults(
    defineProps<{
        size?: 'sm' | 'md' | 'lg';
    }>(),
    {
        size: 'sm',
    }
);
</script>

<template>
    <span
        class="
            inline-block
            animate-spin
            rounded-full
            border-2
            border-white/30
            border-t-white
        "
        :class="{
            'h-4 w-4': size === 'sm',
            'h-5 w-5': size === 'md',
            'h-6 w-6': size === 'lg',
        }"
        aria-label="Carregando"
    />
</template>