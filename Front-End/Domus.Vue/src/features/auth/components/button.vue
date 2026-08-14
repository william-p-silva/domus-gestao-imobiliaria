<script setup lang="ts">
defineProps<{
    value: string
}>();

</script>

<template>
    <button type="submit" class="
                mt-1
                w-full
                rounded-xl

                bg-primary
                px-4
                py-3

                text-sm
                font-semibold
                text-white

                shadow-lg
                shadow-primary-dark/20

                transition-all
                duration-200

                hover:-translate-y-0.5
                hover:bg-primary-light
                hover:shadow-primary/30

                active:translate-y-0

                focus:outline-none
                focus:ring-2
                focus:ring-primary-light/50
                focus:ring-offset-2
                focus:ring-offset-primary-dark/50

                cursor-pointer
            ">
        {{value}}
    </button>
</template>