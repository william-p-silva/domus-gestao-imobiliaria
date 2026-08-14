<script setup lang="ts">
defineProps<{
    type?: string;
    placeholder?: string;
    label?: string;
    modelValue?: string;
}>();

const emit = defineEmits<{
    'update:modelValue': [value: string];
}>();
</script>

<template>
    <div class="flex flex-col gap-1.5">
        <label
            class="
                text-sm
                font-medium
                text-white/85
            "
        >
            {{ label }}
        </label>

        <input
            :type="type ?? 'text'"
            :placeholder="placeholder"
            :value="modelValue"
            @input="
                emit(
                    'update:modelValue',
                    ($event.target as HTMLInputElement).value
                )
            "
            class="
                w-full
                rounded-xl

                border
                border-white/20

                bg-white/10
                backdrop-blur-md

                px-4
                py-3

                text-sm
                text-text
                

                outline-none

                placeholder:text-text/45

                transition-all
                duration-200

                hover:border-white/30
                hover:bg-white/12

                focus:border-primary-light
                focus:bg-white/15
                focus:ring-2
                focus:ring-primary-light/20
            "
        />
    </div>
</template>