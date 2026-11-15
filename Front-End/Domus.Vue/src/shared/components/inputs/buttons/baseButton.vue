<script setup lang="ts">
import { computed } from 'vue'

export type ButtonVariant = 'primary' | 'secondary' | 'inverted' | 'outlined'

interface Props {
    variant?: ButtonVariant
    description?: string
    type?: 'button' | 'submit' | 'reset'
    disabled?: boolean
    loading?: boolean
    fullWidth?: boolean
    action?: () => void
}

const props = withDefaults(defineProps<Props>(), {
    variant: 'primary',
    type: 'button',
    disabled: false,
    loading: false,
    fullWidth: false,
})

const variantClasses: Record<ButtonVariant, string> = {
    primary:
        'bg-primary-dark text-white enabled:hover:bg-primary',
    secondary:
        'bg-background text-text-muted enabled:hover:bg-primary-soft/25 enabled:hover:text-primary',
    inverted:
        'bg-text text-white enabled:hover:bg-text/85',
    outlined:
        'border-text-muted/40 bg-white text-text enabled:hover:bg-background',
}

const classes = computed(() => variantClasses[props.variant])
</script>

<template>
    <button :type="type" :disabled="disabled || loading" :aria-busy="loading" v-on:click="action"
        class="inline-flex min-h-11 items-center justify-center gap-2.5 rounded-[10px] border border-transparent px-5 py-2 transition-colors focus-visible:ring-[3px] focus-visible:ring-primary-soft/50 focus-visible:outline-none disabled:cursor-not-allowed disabled:opacity-50 cursor-pointer"
        :class="[classes, fullWidth ? 'w-full' : '']">
        <!-- Esquerda (o spinner substitui o slot enquanto carrega) -->
        <svg v-if="loading" class="size-4 shrink-0 animate-spin" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4" />
            <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z" />
        </svg>
        <span v-else-if="$slots.left" class="flex shrink-0 items-center">
            <slot name="left" />
        </span>

        <!-- Texto + descrição -->
        <span class="flex min-w-0 flex-col items-center text-center">
            <span class="text-sm font-semibold">
                <slot />
            </span>
            <span v-if="description" class="text-xs font-normal opacity-70">
                {{ description }}
            </span>
        </span>

        <!-- Direita -->
        <span v-if="$slots.right" class="flex shrink-0 items-center">
            <slot name="right" />
        </span>
    </button>
</template>