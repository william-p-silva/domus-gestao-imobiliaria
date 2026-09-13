<script setup lang="ts">
import { Toilet } from '@lucide/vue';


const props = withDefaults(defineProps<{
    size: number
}>(),
    {size: 16}
)


</script>





<template>
    <Toilet :size="size"/>
</template>