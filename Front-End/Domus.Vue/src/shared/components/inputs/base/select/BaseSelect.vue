<script setup lang="ts" generic="T extends string | number">
import { computed, nextTick, onBeforeUnmount, onMounted, ref, useId, watch } from 'vue'
import BaseSelectOption from './baseSelectOption.vue';

interface Option {
    label: string
    value: T
    disabled?: boolean
}

interface Props {
    label: string
    options: readonly Option[]
    description?: string
    error?: string
    placeholder?: string
    required?: boolean
    disabled?: boolean
    id?: string
}

const props = withDefaults(defineProps<Props>(), {
    placeholder: 'Selecione...',
    required: false,
    disabled: false,
})

const model = defineModel<T>({ required: true })

const generatedId = useId()
const baseId = computed(() => props.id ?? generatedId)
const listId = computed(() => `${baseId.value}-list`)
const optionId = (i: number) => `${baseId.value}-opt-${i}`

const control = ref<HTMLElement | null>(null)
const isOpen = ref(false)
const openUp = ref(false)
const activeIndex = ref(-1)

const selectedIndex = computed(() =>
    props.options.findIndex((o) => o.value === model.value)
)
const selectedOption = computed(() => props.options[selectedIndex.value] ?? null)

// ---------- abrir / fechar ----------
function firstEnabled() {
    return props.options.findIndex((o) => !o.disabled)
}

function lastEnabled() {
    for (let i = props.options.length - 1; i >= 0; i--) {
        if (!props.options[i]?.disabled) return i
    }
    return -1
}

function open() {
    if (props.disabled || isOpen.value) return

    // abre para cima se não houver espaço embaixo
    const rect = control.value?.getBoundingClientRect()
    if (rect) {
        const spaceBelow = window.innerHeight - rect.bottom
        openUp.value = spaceBelow < 260 && rect.top > spaceBelow
    }

    activeIndex.value = selectedIndex.value >= 0 ? selectedIndex.value : firstEnabled()
    isOpen.value = true
}

function close() {
    isOpen.value = false
}

function toggle() {
    isOpen.value ? close() : open()
}

function select(option: Option) {
    if (option.disabled) return
    model.value = option.value
    close()
}

// ---------- teclado ----------
function move(step: 1 | -1) {
    const n = props.options.length
    if (!n) return
    let i = activeIndex.value
    for (let c = 0; c < n; c++) {
        i = (i + step + n) % n
        if (!props.options[i]?.disabled) {
            activeIndex.value = i
            return
        }
    }
}

function onKeydown(e: KeyboardEvent) {
    switch (e.key) {
        case 'ArrowDown':
            e.preventDefault()
            isOpen.value ? move(1) : open()
            break
        case 'ArrowUp':
            e.preventDefault()
            isOpen.value ? move(-1) : open()
            break
        case 'Home':
            if (!isOpen.value) return
            e.preventDefault()
            activeIndex.value = firstEnabled()
            break
        case 'End':
            if (!isOpen.value) return
            e.preventDefault()
            activeIndex.value = lastEnabled()
            break
        case 'Enter':
        case ' ': {
            e.preventDefault()
            if (!isOpen.value) {
                open()
            } else {
                const option = props.options[activeIndex.value]
                if (option) select(option)
            }
            break
        }
        case 'Escape':
            if (!isOpen.value) return
            e.preventDefault()
            close()
            break
        case 'Tab':
            close()
            break
    }
}

// mantém o item ativo visível ao navegar com as setas
watch(activeIndex, (i) => {
    if (!isOpen.value || i < 0) return
    nextTick(() => {
        document.getElementById(optionId(i))?.scrollIntoView({ block: 'nearest' })
    })
})

// ---------- clique fora ----------
function onPointerDown(e: PointerEvent) {
    if (isOpen.value && !control.value?.contains(e.target as Node)) close()
}

onMounted(() => document.addEventListener('pointerdown', onPointerDown))
onBeforeUnmount(() => document.removeEventListener('pointerdown', onPointerDown))

// ---------- estilos ----------
const triggerClasses = computed(() =>
    props.error
        ? 'border-red-500 focus:ring-red-500/20'
        : 'border-primary-soft/50 focus:border-primary focus:ring-primary-soft/30'
)


</script>

<template>
    <div class="flex w-full flex-col gap-1.5">
        <label :for="baseId" class="text-sm font-semibold text-text">
            {{ label }}
            <span v-if="required" class="text-red-600">*</span>
        </label>

        <div ref="control" class="relative">
            <button :id="baseId" type="button" role="combobox" :disabled="disabled" :aria-expanded="isOpen"
                aria-haspopup="listbox" :aria-controls="listId"
                :aria-activedescendant="isOpen && activeIndex >= 0 ? optionId(activeIndex) : undefined"
                :aria-invalid="!!error" :aria-required="required" :aria-describedby="`${baseId}-help`"
                class="group flex h-11 w-full items-center gap-2.5 rounded-[10px] border bg-white px-4 text-left transition focus:ring-[3px] focus:outline-none disabled:cursor-not-allowed disabled:opacity-60"
                :class="triggerClasses" @click="toggle" @keydown="onKeydown" @keyup.space.prevent>
                <span v-if="$slots.icon" class="flex shrink-0 items-center transition-colors" :class="error
                    ? 'text-red-500'
                    : 'text-text-muted/50 group-focus:text-primary-light'">
                    <slot name="icon" />
                </span>

                <span class="min-w-0 flex-1 truncate text-[15px]"
                    :class="selectedOption ? 'text-text' : 'text-text-muted/60'">
                    {{ selectedOption ? selectedOption.label : placeholder }}
                </span>

                <svg class="size-4 shrink-0 text-text-muted transition-transform duration-150"
                    :class="{ 'rotate-180': isOpen }" viewBox="0 0 20 20" fill="none" stroke="currentColor"
                    stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">
                    <path d="M5 7.5l5 5 5-5" />
                </svg>
            </button>

            <Transition enter-active-class="transition duration-100 ease-out" enter-from-class="opacity-0"
                leave-active-class="transition duration-75 ease-in" leave-to-class="opacity-0">
                <ul v-if="isOpen" :id="listId" role="listbox"
                    class="absolute left-0 z-20 max-h-60 w-full overflow-auto rounded-[10px] border border-primary-soft/50 bg-white p-1 shadow-lg"
                    :class="openUp ? 'bottom-full mb-1.5' : 'top-full mt-1.5'">
                    <BaseSelectOption v-for="(option, i) in options" :id="optionId(i)" :key="option.value"
                        :label="option.label" :selected="option.value === model" :active="i === activeIndex"
                        :disabled="option.disabled" @select="select(option)" @hover="activeIndex = i">
                        <slot name="option" :option="option" :selected="option.value === model">
                            {{ option.label }}
                        </slot>
                    </BaseSelectOption>

                    <li v-if="!options.length" class="px-3 py-2 text-sm text-text-muted">
                        Nenhuma opção disponível
                    </li>
                </ul>
            </Transition>
        </div>

        <p :id="`${baseId}-help`" class="m-0 min-h-4 text-xs" :class="error ? 'text-red-600' : 'text-text-muted'">
            <template v-if="error">{{ error }}</template>
            <template v-else-if="description">{{ description }}</template>
        </p>
    </div>
</template>