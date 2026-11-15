<script setup lang="ts">
import { computed, useId } from 'vue'

interface Props {
  label: string
  description?: string
  error?: string
  placeholder?: string
  maxCaracteres?: number
  required?: boolean
  type?: string
  id?: string
}

const props = withDefaults(defineProps<Props>(), {
  type: 'text',
  required: false,
})

const model = defineModel<string>({ default: '' })

const generatedId = useId()
const inputId = computed(() => props.id ?? generatedId)

const controlClasses = computed(() =>
  props.error
    ? 'border-danger focus-within:ring-danger/20'
    : 'border-primary-soft/50 focus-within:border-primary focus-within:ring-primary-soft/30'
)
</script>

<template>
  <div class="flex w-full flex-col gap-1.5">
    <div class="flex items-center justify-between">
      <label :for="inputId" class="text-sm font-semibold text-text">
        {{ label }}
        <span v-if="required" class="text-danger">*</span>
      </label>
      <span v-if="maxCaracteres" class="text-xs text-text-muted">
        {{ model.length }}/{{ maxCaracteres }}
      </span>
    </div>

    <div
      class="group flex h-11 items-center gap-2.5 rounded-[10px] border bg-white px-4 transition focus-within:ring-[3px]"
      :class="controlClasses"
    >
      <span v-if="$slots.icon" class="flex shrink-0 items-center text-text-muted/50 transition-colors group-focus-within:text-primary-light">
        <slot name="icon" />
      </span>

      <input
        :id="inputId"
        v-model="model"
        :type="type"
        :placeholder="placeholder"
        :maxlength="maxCaracteres"
        :aria-invalid="!!error"
        :aria-describedby="`${inputId}-help`"
        class="min-w-0 flex-1 border-none bg-transparent text-[15px] text-text outline-none placeholder:text-text-muted/60"
      />
    </div>

    <p
      :id="`${inputId}-help`"
      class="m-0 min-h-4 text-xs"
      :class="error ? 'text-danger' : 'text-text-muted'"
    >
      <template v-if="error">{{ error }}</template>
      <template v-else-if="description">{{ description }}</template>
    </p>
  </div>
</template>