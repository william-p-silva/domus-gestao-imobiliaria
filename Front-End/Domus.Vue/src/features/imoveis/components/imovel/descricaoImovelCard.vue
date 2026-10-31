<script setup lang="ts">



const props = defineProps({
    title: {
        type: String,
        default: '',
        required: true,
    },
    description: {
        type: String,
        default: '',
        required: true
    }
})


</script>


<template>
    <section class="w-full flex flex-col justify-start p-4 border border-primary/30 rounded-2xl mt-4">
        <h2 class="text-primary text-2xl font-bold">{{ title }}</h2>

        <p class="text-black/80">{{ description }}</p>
    </section>
</template>