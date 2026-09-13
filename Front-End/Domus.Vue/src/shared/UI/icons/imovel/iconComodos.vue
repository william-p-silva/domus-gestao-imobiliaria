<script setup lang="ts">
import { BedDouble } from '@lucide/vue';


const props = withDefaults(defineProps<{
    size: number
}>(),
    {size: 16}
)


</script>





<template>
    <BedDouble :size="size"/>
</template>