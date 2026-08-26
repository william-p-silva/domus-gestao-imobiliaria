<script setup lang="ts">
import { useHeader } from '@/shared/hooks/header/useHeader';
import { ArrowDown, Heart, User2Icon } from '@lucide/vue';


const header = useHeader();
</script>




<template>
    <div class="gap-6 flex">
            <div class="gap-2 justify-center items-center hidden sm:flex z-55">
                <Heart />
                <div class="h-10 w-10 rounded-full bg-white/20 flex justify-center items-center">
                    <User2Icon />
                </div>
            </div>

            <div class="flex justify-center items-center z-55 bg p-2 transition-all duration-500"
                @mouseleave="header.close" @click="header.handleNav"
                :class="header.isOpen.value ? 'rotate-180' : 'rotate-0'" @mouseenter="header.open">
                <ArrowDown />
            </div>
        </div>
</template>