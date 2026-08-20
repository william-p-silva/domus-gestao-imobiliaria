<script setup lang="ts">
import logoMarca from '@/assets/logo/LogoMarcaResumo.png'
import SimpleInput from '../inputs/simpleInput.vue';
import { ArrowRight } from '@lucide/vue';
import { useFooter } from '@/shared/hooks/footer/useFooter.ts';

const footer = useFooter();
</script>

<template>
    <footer class="
    border-t shadow-2xl shadow-primary-light border-primary-light/20
    w-full bg-primary-soft px-4 sm:px-6 md:px-8 py-6 text-primary text-sm
    flex flex-col">
        <section class="grid gap-8 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 max-w-7xl m-auto w-full">
            <div
                class="w-full flex flex-col items-center text-center md:items-start md:text-start justify-center gap-4">
                <RouterLink to="/">
                    <img :src="logoMarca" alt="Logo da DOMUS" class="w-28 sm:w-33">
                </RouterLink>
                <p>A maneira mais fácil, segura e inteligente de encontrar imóveis para alugar</p>
                
            </div>

            <div v-for="info in footer.links" :key="info.title"
                class="w-full flex flex-col items-center text-center md:items-start md:text-start gap-1">
                <h4 class="text-primary-dark font-bold">{{ info.title }}</h4>
                <RouterLink v-for="link in info.links" :key="link.route" :to="link.route">
                    {{ link.label }}
                </RouterLink>
            </div>

            <div
                class="flex flex-col gap-2 w-full items-center text-center md:items-start md:text-start sm:col-span-2 lg:col-span-1 xl:col-span-1">
                <h4 class="text-primary-dark font-bold">Receba Novidades</h4>
                <p>Cadastre-se e receba as melhores opções de imóveis</p>
                <div class="flex flex-col sm:flex-row gap-4 w-full">
                    <div class="w-full flex flex-col justify-center">
                        <SimpleInput type="email" placeholder="Seu melhor E-mail" label="" />
                    </div>
                    <RouterLink to="/auth/cadastro"
                        class="bg-primary px-6 py-3 sm:py-0 text-white flex justify-center items-center rounded-xl border border-primary shrink-0">
                        <ArrowRight />
                    </RouterLink>
                </div>
            </div>
        </section>
        <section class="flex justify-center mt-6 text-center px-2">
            <p>2026 DOMUS. Todos os direitos reservados.</p>
        </section>
    </footer>
</template>