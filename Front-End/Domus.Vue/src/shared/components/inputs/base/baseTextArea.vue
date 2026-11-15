<script setup lang="ts">
import { computed, nextTick, onMounted, ref, useId, watch } from 'vue'

interface Props {
  label: string
  description?: string
  error?: string
  placeholder?: string
  maxCaracteres?: number
  required?: boolean
  minHeight?: number // em px
  id?: string
}

const props = withDefaults(defineProps<Props>(), {
  required: false,
  minHeight: 120,
})

const model = defineModel<string>({ default: '' })

const generatedId = useId()
const textareaId = computed(() => props.id ?? generatedId)

const textareaRef = ref<HTMLTextAreaElement | null>(null)

// Só cresce: se o conteúdo passar da altura atual, aumenta até caber.
// Isso respeita o redimensionamento manual (não "briga" com o arraste do mouse).
function autoGrow() {
  const el = textareaRef.value
  if (!el) return
  if (el.scrollHeight > el.clientHeight) {
    el.style.height = `${el.scrollHeight}px`
  }
}

onMounted(autoGrow)

// Também cobre mudanças vindas de fora (ex.: preencher o form via código)
watch(model, () => nextTick(autoGrow))

const controlClasses = computed(() =>
  props.error
    ? 'border-red-500 focus-within:ring-red-500/20'
    : 'border-primary-soft/50 focus-within:border-primary focus-within:ring-primary-soft/30'
)
</script>

<template>
  <div class="flex w-full flex-col gap-1.5">
    <div class="flex items-center justify-between">
      <label :for="textareaId" class="text-sm font-semibold text-text">
        {{ label }}
        <span v-if="required" class="text-red-600">*</span>
      </label>
      <span v-if="maxCaracteres" class="text-xs text-text-muted">
        {{ model.length }}/{{ maxCaracteres }}
      </span>
    </div>

    <div
      class="group flex items-start gap-2.5 rounded-[10px] border bg-white px-4 py-3 transition focus-within:ring-[3px]"
      :class="controlClasses"
      @click="textareaRef?.focus()"
    >
      <span
        v-if="$slots.icon"
        class="mt-0.5 flex shrink-0 items-center transition-colors"
        :class="error
          ? 'text-red-500'
          : 'text-text-muted/50 group-focus-within:text-primary-light'"
      >
        <slot name="icon" />
      </span>

      <textarea
        :id="textareaId"
        ref="textareaRef"
        v-model="model"
        :placeholder="placeholder"
        :maxlength="maxCaracteres"
        :required="required"
        :aria-invalid="!!error"
        :aria-describedby="`${textareaId}-help`"
        :style="{ minHeight: `${minHeight}px` }"
        class="block min-w-0 flex-1 resize-y border-none bg-transparent text-[15px] leading-relaxed text-text outline-none placeholder:text-text-muted/60"
        @input="autoGrow"
      />
    </div>

    <p
      :id="`${textareaId}-help`"
      class="m-0 min-h-4 text-xs"
      :class="error ? 'text-red-600' : 'text-text-muted'"
    >
      <template v-if="error">{{ error }}</template>
      <template v-else-if="description">{{ description }}</template>
    </p>
  </div>
</template>