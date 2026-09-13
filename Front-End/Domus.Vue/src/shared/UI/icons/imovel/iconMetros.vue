<script setup lang="ts">
import { SquareDashed } from '@lucide/vue';


const props = withDefaults(defineProps<{
    size: number
}>(),
    {size: 16}
)

</script>





<template>
    <SquareDashed :size="size"/>
</template>