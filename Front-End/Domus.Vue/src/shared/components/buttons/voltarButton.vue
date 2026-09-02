<script setup lang="ts">
import { ArrowLeft } from '@lucide/vue';

withDefaults(defineProps<{
  label?: string
}>(), {
  label: 'Voltar para a busca',
})

const emit = defineEmits<{
  click: []
}>()
</script>

<template>
  <button
    type="button"
    class="group relative inline-flex items-center gap-2 rounded-full bg-primary-dark px-6 py-3 text-sm font-semibold text-white transition-all duration-200 ease-out hover:bg-primary hover:pl-5 hover:pr-7 active:scale-[0.97] focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-primary-light focus-visible:ring-offset-2 cursor-pointer"
    @click="emit('click')"
  >
    <ArrowLeft
      :size="16"
      class="transition-transform duration-200 ease-out group-hover:-translate-x-1"
    />
    <span>{{ label }}</span>
    <span
      class="absolute inset-0 -z-10 rounded-full bg-accent/0 blur-md transition-all duration-300 group-hover:bg-accent/20"
      aria-hidden="true"
    />
  </button>
</template>