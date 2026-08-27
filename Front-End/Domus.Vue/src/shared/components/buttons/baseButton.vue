<script setup lang="ts">
import LoadingCircle from '@/shared/components/loadingCircle.vue';

defineProps<{
    value: string,
    isLoading: boolean
}>();

</script>

<template>
    <button type="submit" :disabled="isLoading" class="
                mt-1
                w-full
                rounded-xl

                bg-primary-dark
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
                hover:bg-primary
                hover:shadow-primary/30

                active:translate-y-0

                
                cursor-pointer
            ">
        <LoadingCircle v-if="isLoading" size="sm" />
        <span v-else>
            {{ value }}
        </span>
    </button>
</template>