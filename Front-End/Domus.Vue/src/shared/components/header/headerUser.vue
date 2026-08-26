<script setup lang="ts">
import { useAuthStore } from "@/core/configuration/authentication";
import { useHeader } from "@/shared/hooks/header/useHeader";
import { ArrowDown, Heart, LucideHatGlasses, User2Icon } from "@lucide/vue";
import HeaderNavLinks from "./headerNavLinks.vue";
import HeaderButtonNavLink from "./headerButtonNavLink.vue";
import HeaderLinks from "./headerLinks.vue";
import HeaderBusca from "./headerBusca.vue";

const header = useHeader();
const auth = useAuthStore();
</script>

<template>
    <header class="w-full bg-primary-dark text-white flex justify-between items-center p-4 gap-6">
        <HeaderBusca />

        <HeaderLinks />

        <HeaderButtonNavLink />
    </header>

    <Transition name="links-modal">
        <HeaderNavLinks v-if="header.isOpen.value" />
    </Transition>
</template>

<style scoped>
@import url('../../style/header/header.css');
</style>