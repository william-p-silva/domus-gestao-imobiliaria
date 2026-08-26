<script setup lang="ts">
import logoMarca from "@/assets/logo/LogoMarcaResumo.png"
import { useAuthStore } from "@/core/configuration/authentication";
import { useHeader } from "@/shared/hooks/header/useHeader";
import { ArrowDown, Heart, LucideHatGlasses, User2Icon } from "@lucide/vue";

const header = useHeader();
const auth = useAuthStore();
</script>

<template>
    <header class="w-full bg-primary-dark text-white flex justify-between items-center p-4 gap-6">
        <div class="flex gap-8">
            <img :src="logoMarca" alt="" class="h-12 hidden lg:flex">
            <div class="bg-white px-1 sm:px-4 py-1 rounded-lg text-primary-dark flex gap-3 items-center">
                <LucideHatGlasses />
                <input type="text" name="" id="" placeholder="Buscar por localização" class="outline-none">
            </div>
        </div>
        <div class="hidden md:flex">
            <ul class="flex gap-3">
                <RouterLink
                :to="link.route"
                v-for="link in header.links"
                :key="link.route"
                class="transition-colors duration-200 hover:text-primary-light"
            >
                {{ link.label }}
            </RouterLink>
            </ul>
        </div>
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
    </header>

    <Transition name="links-modal">
    <div v-if="header.isOpen.value"
        class="absolute top-0 right-0 z-50 pt-15 pb-10 pr-5 pl-10 text-primary-soft flex"
        @mouseenter="header.open" @mouseleave="header.close">
        <ul class="flex flex-col gap-3 p-4 bg-primary-dark rounded-l-lg rounded-b-lg">
            <RouterLink
                :to="link.route"
                v-for="link in header.allLinks"
                :key="link.route"
                class="transition-colors duration-200 hover:text-primary-light"
            >
                {{ link.label }}
            </RouterLink>
        </ul>
    </div>
</Transition>
</template>

<style scoped>
.links-modal-enter-active,
.links-modal-leave-active {
    transition: transform 0.25s ease, opacity 0.25s ease;
}

.links-modal-enter-from {
    transform: translateY(-20px);
    opacity: 0;
}

.links-modal-enter-to {
    transform: translateY(0);
    opacity: 1;
}

.links-modal-leave-from {
    transform: translateY(0);
    opacity: 1;
}

.links-modal-leave-to {
    transform: translateY(-20px);
    opacity: 0;
}
</style>