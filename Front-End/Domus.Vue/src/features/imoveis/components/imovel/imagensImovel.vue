


<template>
    <article class="w-full lg:w-3/5 lg:shrink-0">
        <div class="flex flex-col gap-3 w-full">
            <img :src="img" alt="" class="rounded-2xl w-full aspect-4/3 sm:aspect-video object-cover">
            <div class="grid grid-cols-5 gap-8 sm:gap-8">
                <div v-for="num in imgsUrl" :key="num" class=" sm:h-22 bg-primary rounded-lg" />
            </div>
        </div>
    </article>
</template>

<script setup lang="ts" >
import img from "@/assets/imgs/LandingPage/encontrandoImovel.jpeg"

const props = defineProps<({
    imgsUrl: number,
})>()

</script>