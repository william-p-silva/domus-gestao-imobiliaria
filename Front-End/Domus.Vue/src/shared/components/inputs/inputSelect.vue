<script setup lang="ts">
import { ChevronDown } from '@lucide/vue';
import { ref, computed, onMounted, onUnmounted } from 'vue';

interface Option {
    label: string;
    value: string;
}

const props = defineProps<{
    modelValue?: string;
    options: Option[];
    placeholder?: string;
}>();

const emit = defineEmits<{
    (e: 'update:modelValue', value: string): void;
}>();

const isOpen = ref(false);
const rootRef = ref<HTMLElement | null>(null);

const selectedLabel = computed(() => {
    const found = props.options.find(opt => opt.value === props.modelValue);
    return found ? found.label : props.placeholder ?? '';
});

function selectOption(value: string) {
    emit('update:modelValue', value);
    isOpen.value = false;
}

function handleClickOutside(event: MouseEvent) {
    if (rootRef.value && !rootRef.value.contains(event.target as Node)) {
        isOpen.value = false;
    }
}

onMounted(() => document.addEventListener('click', handleClickOutside));
onUnmounted(() => document.removeEventListener('click', handleClickOutside));
</script>



<template>
    <div ref="rootRef" class="relative w-full">
        <button
            type="button"
            @click="isOpen = !isOpen"
            class="w-full flex justify-between items-center p-2 outline-none border border-primary/30 rounded-lg focus:ring focus:ring-primary-light/50 text-primary cursor-pointer bg-transparent"
        >
            <span>{{ selectedLabel }}</span>
            <ChevronDown :size="18" :class="isOpen ? 'rotate-180' : ''" class="transition-transform" />
        </button>

        <ul
            v-if="isOpen"
            class="absolute z-10 mt-1 w-full border border-primary/30 rounded-lg bg-white overflow-hidden shadow-sm"
        >
            <li
                v-for="opt in options"
                :key="opt.value"
                @click="selectOption(opt.value)"
                class="p-2 text-primary hover:text-primary-dark hover:bg-primary-soft/60 cursor-pointer"
            >
                {{ opt.label }}
            </li>
        </ul>
    </div>
</template>