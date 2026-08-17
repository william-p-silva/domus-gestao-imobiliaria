<script setup lang="ts">
import { ChevronDown , Locate } from '@lucide/vue';
import type { InputBuscaImovel } from '../types/common';


const props = defineProps<{
    infos: InputBuscaImovel,
    modelValue?: string
}>();

const emit = defineEmits<{
    'update:modelValue': [value: string];
}>();

</script>


<template>
    <div class="flex flex-col flex-1 min-w-[180px]">
        <p class="font-bold text-primary">{{ infos.text }}</p>
        <div class="flex p-4 border border-primary/20 rounded-2xl text-primary">
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