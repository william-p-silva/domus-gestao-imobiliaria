<script setup lang="ts">
import logo from '@/assets/logo/logo.png'
import { ArrowRight } from '@lucide/vue';

</script>


<template>
    <section class="w-full px-8 -mt-4 pb-12">
            <div class="w-full p-6 bg-primary rounded-2xl text-white flex justify-between flex-col sm:flex-row">
                <div>
                    <h3 class="text-xl sm:text-2xl font-bold">Seu próximo lar</h3>
                    <h3 class="text-xl sm:text-2xl font-bold mb-2">está mais perto do que você imagina</h3>
                    <p>Milhares de imóveis incríveis te esperam na DOMUS</p>
                </div>
                <div class="flex justify-center items-center gap-16">
                    <RouterLink to="/auth/cadastro" class="p-4  bg-accent rounded-2xl text-text font-bold flex gap-3">
                        Começar agora
                        <ArrowRight class="" />
                    </RouterLink>
                    <img :src="logo" alt="" class="h-20 hidden lg:flex">
                </div>
            </div>

        </section>
</template>