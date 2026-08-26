<script setup lang="ts">
import { useHeader } from '@/shared/hooks/header/useHeader';
import logoMarca from "@/assets/logo/LogoMarcaResumo.png"
import { LucideHatGlasses } from '@lucide/vue';

const header = useHeader();
</script>


<template>
    <div class="flex gap-8">
        <RouterLink to="/imoveis">
            <img :src="logoMarca" alt="" class="h-12 hidden lg:flex">
        </RouterLink>
        <div class="bg-white px-1 sm:px-4 py-1 rounded-lg text-primary-dark flex gap-3 items-center">
            <LucideHatGlasses />
            <input type="text" name="" id="" placeholder="Buscar por localização" class="outline-none">
        </div>
    </div>
</template>