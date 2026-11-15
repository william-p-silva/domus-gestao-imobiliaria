<script setup lang="ts">
defineProps<{
  id: string
  label: string
  selected: boolean
  active: boolean
  disabled?: boolean
}>()

defineEmits<{
  select: []
  hover: []
}>()
</script>

<template>
  <li
    :id="id"
    role="option"
    :aria-selected="selected"
    :aria-disabled="disabled"
    class="flex items-center justify-between gap-2 rounded-lg px-3 py-2 text-[15px] text-text transition-colors"
    :class="[
      active && !disabled ? 'bg-background' : '',
      selected ? 'font-semibold text-primary' : '',
      disabled ? 'cursor-not-allowed opacity-40' : 'cursor-pointer',
    ]"
    @mousedown.prevent
    @click="!disabled && $emit('select')"
    @mousemove="!disabled && $emit('hover')"
  >
    <slot>{{ label }}</slot>

    <svg
      v-if="selected"
      class="size-4 shrink-0"
      viewBox="0 0 20 20"
      fill="none"
      stroke="currentColor"
      stroke-width="2"
      stroke-linecap="round"
      stroke-linejoin="round"
      aria-hidden="true"
    >
      <path d="M4 10.5l4 4 8-9" />
    </svg>
  </li>
</template>