<script setup lang="ts">
import { computed, ref, useId, watch } from 'vue'

interface Props {
    label: string
    description?: string
    error?: string
    required?: boolean
    disabled?: boolean
    step?: number
    min?: number
    max?: number
    id?: string
}

const props = withDefaults(defineProps<Props>(), {
    required: false,
    disabled: false,
    step: 1,
    min: -Infinity,
    max: Infinity,
})

const model = defineModel<number>({ required: true })

const generatedId = useId()
const inputId = computed(() => props.id ?? generatedId)

// step sempre positivo; o sinal vem do botão (− ou +)
const stepAbs = computed(() => Math.abs(props.step) || 1)

// ---------- precisão (evita 0.1 + 0.2 = 0.30000000000000004) ----------
function decimalsOf(n: number) {
    const s = String(n)
    if (s.includes('e-')) return Number(s.split('e-')[1])
    return s.split('.')[1]?.length ?? 0
}

const precision = computed(() =>
    Math.max(decimalsOf(stepAbs.value), decimalsOf(model.value))
)

function normalize(n: number) {
    const rounded = Number(n.toFixed(precision.value))
    return Math.min(props.max, Math.max(props.min, rounded))
}

// ---------- texto exibido (aceita vírgula e ponto) ----------
const format = (n: number) => String(n).replace('.', ',')

const draft = ref(format(model.value))

watch(model, (v) => {
    draft.value = format(v)
})

function parse(text: string) {
    const clean = text.trim().replace(',', '.')
    if (clean === '' || clean === '-' || clean === '.') return NaN
    return Number(clean)
}

// só deixa digitar caracteres válidos
function onInput(e: Event) {
    const el = e.target as HTMLInputElement
    const cleaned = el.value
        .replace(/[^\d.,-]/g, '')
        .replace(/(?!^)-/g, '') // "-" só no início
    el.value = cleaned
    draft.value = cleaned
}

// confirma o que foi digitado (blur / Enter)
function commitDraft() {
    const n = parse(draft.value)
    if (!Number.isNaN(n)) model.value = normalize(n)
    draft.value = format(model.value) // se inválido, volta ao valor anterior
}

// ---------- botões e teclado ----------
const canDecrement = computed(() => !props.disabled && model.value > props.min)
const canIncrement = computed(() => !props.disabled && model.value < props.max)

function change(direction: 1 | -1) {
    if (props.disabled) return
    commitDraft()
    model.value = normalize(model.value + direction * stepAbs.value)
    draft.value = format(model.value)
}

function onKeydown(e: KeyboardEvent) {
    if (e.key === 'ArrowUp') {
        e.preventDefault()
        change(1)
    } else if (e.key === 'ArrowDown') {
        e.preventDefault()
        change(-1)
    } else if (e.key === 'Enter') {
        e.preventDefault()
        commitDraft()
    } else if (e.key === 'Home' && Number.isFinite(props.min)) {
        e.preventDefault()
        model.value = props.min
    } else if (e.key === 'End' && Number.isFinite(props.max)) {
        e.preventDefault()
        model.value = props.max
    }
}

const controlClasses = computed(() =>
    props.error
        ? 'border-red-500 focus-within:ring-red-500/20'
        : 'border-primary-soft/50 focus-within:border-primary focus-within:ring-primary-soft/30'
)

const buttonClasses =
    'flex h-full w-11 shrink-0 items-center justify-center text-text-muted transition-colors enabled:hover:bg-background enabled:hover:text-primary disabled:cursor-not-allowed disabled:opacity-30'
</script>

<template>
    <div class="flex w-full flex-col gap-1.5">
        <label :for="inputId" class="text-sm font-semibold text-text">
            {{ label }}
            <span v-if="required" class="text-red-600">*</span>
        </label>

        <div class="flex h-11 items-center overflow-hidden rounded-[10px] border bg-white transition focus-within:ring-[3px]"
            :class="[controlClasses, disabled ? 'opacity-60' : '']">
            <button type="button" tabindex="-1" aria-label="Diminuir" :disabled="!canDecrement" :class="buttonClasses"
                @click="change(-1)">
                <svg class="size-4" viewBox="0 0 20 20" fill="none" stroke="currentColor" stroke-width="2"
                    stroke-linecap="round" aria-hidden="true">
                    <path d="M4 10h12" />
                </svg>
            </button>

            <input :id="inputId" :value="draft" type="text" inputmode="decimal" role="spinbutton" autocomplete="off"
                :disabled="disabled" :aria-valuenow="model" :aria-valuemin="Number.isFinite(min) ? min : undefined"
                :aria-valuemax="Number.isFinite(max) ? max : undefined" :aria-invalid="!!error"
                :aria-required="required" :aria-describedby="`${inputId}-help`"
                class="min-w-0 flex-1 border-none bg-transparent text-center text-[15px] text-text outline-none"
                @input="onInput" @blur="commitDraft" @keydown="onKeydown" />

            <button type="button" tabindex="-1" aria-label="Aumentar" :disabled="!canIncrement" :class="buttonClasses"
                @click="change(1)">
                <svg class="size-4" viewBox="0 0 20 20" fill="none" stroke="currentColor" stroke-width="2"
                    stroke-linecap="round" aria-hidden="true">
                    <path d="M4 10h12M10 4v12" />
                </svg>
            </button>
        </div>

        <p :id="`${inputId}-help`" class="m-0 min-h-4 text-xs" :class="error ? 'text-red-600' : 'text-text-muted'">
            <template v-if="error">{{ error }}</template>
            <template v-else-if="description">{{ description }}</template>
        </p>
    </div>
</template>