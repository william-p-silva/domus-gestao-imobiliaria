<script setup lang="ts">
import { MapPin } from '@lucide/vue';
import { usePageImovel } from '../hooks/usePageImovel';
import img from "@/assets/imgs/LandingPage/encontrandoImovel.jpeg"
import { FormatterNumber } from '@/shared/utils/formatter/formatterNumber';
import BaseButton from '@/shared/components/buttons/baseButton.vue';


const imovelPage = usePageImovel();
const formmater  = FormatterNumber();
const nums = 5
</script>



<template>
    
    <section class="w-full flex flex-row  h-full text-primary">
        <article class=" ">
                <div class="flex flex-col gap-3  w-185 h-fulll">
                    <img :src="img" alt="" class="rounded-2xl">
                    <div class="grid grid-cols-5 gap-6">
                        <div v-for="num in nums" class="h-22 bg-primary rounded-lg " />
                    </div>
                </div>
        </article>
        <article class="flex flex-col px-4">
            <div>
                <p>{{ imovelPage.imovel.value?.tipoDoImovel }}</p>
            </div>
            <div>
                <div>
                    <h3 class="text-primary-dark text-2xl font-bold">{{ imovelPage.imovel.value?.titulo }}</h3>
                </div>
                <div>
                    <p class="flex gap-1 items-center text-sm">
                        <MapPin  :size="15"/>
                        {{ imovelPage.imovel.value?.endereco.bairro }},
                        {{ imovelPage.imovel.value?.endereco.cidade }},
                        {{ imovelPage.imovel.value?.endereco.uf }}
                    </p>
                </div>            
            </div>
            <div>
                <h2 class="font-bold text-3xl text-primary-dark">
                    {{ formmater.moeda(imovelPage.imovel.value?.valorAluguel) }} <span class="text-lg">/mês</span>
                </h2>
                <p>{{ imovelPage.imovel.value?.descricao }}</p>
            </div>

            <div class="flex flex-col gap-2 px-1">
                <BaseButton value="Conversar com locador" :isLoading="imovelPage.isLoading.value" />
                <BaseButton value="Agendar Visita" :isLoading="imovelPage.isLoading.value" />
            </div>
        </article>
    </section>
</template>