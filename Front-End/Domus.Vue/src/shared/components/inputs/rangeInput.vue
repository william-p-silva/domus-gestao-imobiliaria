<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
    modelValue: [number, number];
    min: number;
    max: number;
    step?: number;
}>();

const emit = defineEmits<{
    (e: 'update:modelValue', value: [number, number]): void;
}>();

function formatPrice(value: number) {
    if (value >= props.max) return `R$ ${props.max.toLocaleString('pt-BR')}+`;
    return `R$ ${value.toLocaleString('pt-BR')}`;
}

function updateMin(event: Event) {
    const value = Number((event.target as HTMLInputElement).value);
    const newMin = Math.min(value, props.modelValue[1] - (props.step ?? 1));
    emit('update:modelValue', [newMin, props.modelValue[1]]);
}

function updateMax(event: Event) {
    const value = Number((event.target as HTMLInputElement).value);
    const newMax = Math.max(value, props.modelValue[0] + (props.step ?? 1));
    emit('update:modelValue', [props.modelValue[0], newMax]);
}

const trackStyle = computed(() => {
    const range = props.max - props.min;
    const left = ((props.modelValue[0] - props.min) / range) * 100;
    const right = ((props.modelValue[1] - props.min) / range) * 100;
    return { left: `${left}%`, width: `${right - left}%` };
});
</script>

<template>
    <div class="flex flex-col gap-3">
        <div class="relative h-1.5 rounded-full bg-primary/20">
            <div class="absolute h-1.5 rounded-full bg-primary-dark" :style="trackStyle"></div>

            <input
                type="range"
                :min="min"
                :max="max"
                :step="step ?? 1"
                :value="modelValue[0]"
                @input="updateMin"
                class="range-thumb"
            >
            <input
                type="range"
                :min="min"
                :max="max"
                :step="step ?? 1"
                :value="modelValue[1]"
                @input="updateMax"
                class="range-thumb"
            >
        </div>

        <div class="flex justify-between text-sm text-primary">
            <span>{{ formatPrice(modelValue[0]) }}</span>
            <span>{{ formatPrice(modelValue[1]) }}</span>
        </div>
    </div>
</template>

<style scoped>
.range-thumb {
    position: absolute;
    inset: 0;
    width: 100%;
    height: 1.5rem;
    top: 50%;
    transform: translateY(-50%);
    appearance: none;
    background: transparent;
    pointer-events: none;
    margin: 0;
}

.range-thumb::-webkit-slider-thumb {
    appearance: none;
    pointer-events: auto;
    width: 18px;
    height: 18px;
    border-radius: 50%;
    background: white;
    border: 2px solid var(--color-primary-dark);
    cursor: pointer;
}

.range-thumb::-moz-range-thumb {
    pointer-events: auto;
    width: 18px;
    height: 18px;
    border-radius: 50%;
    background: white;
    border: 2px solid var(--color-primary-dark);
    cursor: pointer;
}
</style>