<script setup lang="ts" >
defineProps<{
    error: string
}>();
</script>



<template>
    <div class="
                rounded-lg
                border
                border-red-300/60
                bg-red-500/60
                px-3
                py-2
                text-xs
                font-medium
                text-red-100
            ">
        {{ error }}
    </div>
</template>