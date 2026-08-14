<script setup lang="ts">
defineProps<{
    title: string,
    description: string
}>();

</script>




<template>
    <div class="mb-2 text-center">
        <h1 class="
                    text-2xl
                    font-semibold
                    tracking-tight
                    text-white
                ">
            {{ title }}
        </h1>

        <p class="
                    mt-2
                    text-sm
                    text-white/60
                ">
            {{ description }}
        </p>
    </div>
</template>