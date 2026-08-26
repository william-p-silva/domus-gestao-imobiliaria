<script setup lang="ts">
import { useHeader } from '@/shared/hooks/header/useHeader';

const header = useHeader();
</script>



<template>
    <div class="hidden md:flex">
        <ul class="flex gap-3">
            <RouterLink :to="link.route" v-for="link in header.links" :key="link.route"
                class="transition-colors duration-200 hover:text-primary-light">
                {{ link.label }}
            </RouterLink>
        </ul>
    </div>
</template>