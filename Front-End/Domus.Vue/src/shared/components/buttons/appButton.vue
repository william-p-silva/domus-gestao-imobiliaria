<script setup>


defineProps({
  variant: {
    type: String,
    default: 'primary', // 'primary' | 'outline'
    validator: (v) => ['primary', 'outline'].includes(v),
  },
})
</script>

<template>
  <button
    class="w-full flex items-center justify-center gap-2 rounded-xl px-6 py-3 font-medium text-[15px] transition-all duration-200 ease-out active:scale-[0.97] cursor-pointer focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-offset-2"
    :class="variant === 'primary'
      ? 'bg-primary-dark text-white shadow-sm hover:bg-primary hover:shadow-md focus-visible:ring-primary-light'
      : 'bg-transparent text-primary-dark border border-primary/20 hover:border-primary/30 hover:bg-primary-soft focus-visible:ring-primary-light'"
  >
    <span v-if="$slots.icon" class="shrink-0">
      <slot name="icon" />
    </span>
    <slot />
  </button>
</template>