<script setup lang="ts">
import { ChevronDown , Locate } from '@lucide/vue';
import type { InputBuscaImovel } from '../types/common';


const props = defineProps<{
    infos: InputBuscaImovel,
    modelValue?: string,
    clasName?: string
}>();

const emit = defineEmits<{
    'update:modelValue': [value: string];
}>();

</script>


<template>
    <div class="flex flex-col flex-1 sm:min-w-45">
        <p class="font-bold text-primary">{{ infos.text }}</p>
        <div class="flex border border-primary/20 rounded-2xl text-primary" :class="clasName">
            <input type="text" class="w-full min-w-0 outline-none" :placeholder="infos.placeholder" :value="modelValue"
                @input="emit(
                    'update:modelValue',
                    ($event.target as HTMLInputElement).value
                )">
                <Locate v-if="infos.icon === 'location'" />
                <ChevronDown v-else/>
        </div>
    </div>
</template>