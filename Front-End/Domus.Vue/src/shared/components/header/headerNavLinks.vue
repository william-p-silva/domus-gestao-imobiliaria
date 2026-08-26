<script setup lang="ts">
import { useHeader } from '@/shared/hooks/header/useHeader';

const header = useHeader();

</script>


<template>
    <div class="absolute top-0 right-0 z-50 pt-15 pb-10 pr-5 pl-10 text-primary-soft flex" @mouseenter="header.open"
        @mouseleave="header.close">
        <ul class="flex flex-col gap-3 p-4 bg-primary-dark rounded-l-lg rounded-b-lg">
            <RouterLink :to="link.route" v-for="link in header.allLinks" :key="link.route"
                class="transition-colors duration-200 hover:text-primary-light">
                {{ link.label }}
            </RouterLink>
        </ul>
    </div>
</template>