<script setup lang="ts">
import { computed, ref, useId, watch } from 'vue'

interface Props {
    label: string
    description?: string
    error?: string
    required?: boolean
    disabled?: boolean
    min?: number
    max?: number
    decimals?: number // casas decimais (0 = só inteiros)
    size?: 'md' | 'lg'
    id?: string
}

const props = withDefaults(defineProps<Props>(), {
    required: false,
    disabled: false,
    min: -Infinity,
    max: Infinity,
    decimals: 0,
    size: 'md',
})

const model = defineModel<number>({ required: true })

const generatedId = useId()
const inputId = computed(() => props.id ?? generatedId)

const inputEl = ref<HTMLInputElement | null>(null)
const isFocused = ref(false)

// ---------- formatação (pt-BR: 4.200,00) ----------
const formatter = computed(
    () =>
        new Intl.NumberFormat('pt-BR', {
            minimumFractionDigits: props.decimals,
            maximumFractionDigits: props.decimals,
        })
)

const formatDisplay = (n: number) => formatter.value.format(n)
const formatRaw = (n: number) => String(n).replace('.', ',') // sem milhar, para editar

const draft = ref(formatDisplay(model.value))

// mudanças vindas de fora (ex.: reset do form) atualizam o texto
watch(model, (v) => {
    if (!isFocused.value) draft.value = formatDisplay(v)
})

// ---------- parse / sanitização ----------
const allowNegative = computed(() => props.min < 0)

function sanitize(text: string) {
    // colou "4.200,00"? remove os pontos de milhar; senão, ponto vira vírgula
    let t = text.includes(',') ? text.replace(/\./g, '') : text.replace(/\./g, ',')

    t = t.replace(/[^\d,-]/g, '')

    // "-" só no início e só se o mínimo permitir negativos
    t = t.replace(/(?!^)-/g, '')
    if (!allowNegative.value) t = t.replace(/-/g, '')

    // uma única vírgula, respeitando o limite de casas decimais
    const [int = '', ...rest] = t.split(',')
    if (props.decimals === 0 || rest.length === 0) return props.decimals === 0 ? int : t
    return `${int},${rest.join('').slice(0, props.decimals)}`
}

function parse(text: string) {
    const clean = text.trim().replace(',', '.')
    if (clean === '' || clean === '-' || clean === '.') return 0
    const n = Number(clean)
    return Number.isNaN(n) ? 0 : n
}

const round = (n: number) => Number(n.toFixed(props.decimals))
const clamp = (n: number) => Math.min(props.max, Math.max(props.min, n))

// ---------- eventos ----------
function onInput(e: Event) {
    const el = e.target as HTMLInputElement
    const cleaned = sanitize(el.value)
    el.value = cleaned
    draft.value = cleaned
    model.value = round(parse(cleaned)) // atualiza ao vivo; o limite é aplicado no blur
}

function onFocus() {
    isFocused.value = true
    draft.value = formatRaw(model.value)
    // seleciona tudo para já poder digitar por cima
    requestAnimationFrame(() => inputEl.value?.select())
}

function commit() {
    model.value = clamp(round(parse(draft.value)))
    draft.value = isFocused.value ? formatRaw(model.value) : formatDisplay(model.value)
}

function onBlur() {
    isFocused.value = false
    commit()
}

function onKeydown(e: KeyboardEvent) {
    if (e.key === 'Enter') {
        e.preventDefault()
        commit()
    }
}

// ---------- estilos ----------
const controlClasses = computed(() =>
    props.error
        ? 'border-red-500 focus-within:ring-red-500/20'
        : 'border-primary-soft/50 focus-within:border-primary focus-within:ring-primary-soft/30'
)

const sizeClasses = computed(() => (props.size === 'lg' ? 'h-14' : 'h-11'))
const inputSizeClasses = computed(() =>
    props.size === 'lg' ? 'text-xl font-bold' : 'text-[15px]'
)
</script>

<template>
    <div class="flex w-full flex-col gap-1.5">
        <label :for="inputId" class="text-sm font-semibold text-text">
            {{ label }}
            <span v-if="required" class="text-red-600">*</span>
        </label>

        <div class="flex cursor-text items-center gap-2.5 rounded-[10px] border bg-white px-4 transition focus-within:ring-[3px]"
            :class="[controlClasses, sizeClasses, disabled ? 'cursor-not-allowed opacity-60' : '']"
            @click="inputEl?.focus()">
            <span v-if="$slots.left" class="flex shrink-0 items-center text-text-muted">
                <slot name="left" />
            </span>

            <input :id="inputId" ref="inputEl" :value="draft" type="text"
                :inputmode="decimals > 0 ? 'decimal' : 'numeric'" autocomplete="off" :disabled="disabled"
                :aria-invalid="!!error" :aria-required="required" :aria-describedby="`${inputId}-help`"
                class="min-w-0 flex-1 border-none bg-transparent text-text outline-none" :class="inputSizeClasses"
                @input="onInput" @focus="onFocus" @blur="onBlur" @keydown="onKeydown" />

            <span v-if="$slots.right" class="flex shrink-0 items-center text-text-muted">
                <slot name="right" />
            </span>
        </div>

        <p :id="`${inputId}-help`" class="m-0 min-h-4 text-xs" :class="error ? 'text-red-600' : 'text-text-muted'">
            <template v-if="error">{{ error }}</template>
            <template v-else-if="description">{{ description }}</template>
        </p>
    </div>
</template>