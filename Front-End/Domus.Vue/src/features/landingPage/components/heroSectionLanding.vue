<script setup lang="ts">
import { ShieldCheck, User2 } from '@lucide/vue';
import SimpleLanding from './cards/simpleLanding.vue';
import { useLanding } from '../hooks/useLanding.ts';

const landing = useLanding();
</script>

<template>
<div class="flex w-full mt-12">
    <div class="px-6 w-[45%] flex-col flex">
            <div>
                <div class="flex text-primary">
                    <ShieldCheck />
                    <p class="text-sm ">
                        O caminho para o seu novo lar começa aqui
                    </p>
                </div>
                <div class="text-6xl font-bold ">
                    <p class="text-text">Encontre o lar</p>
                    <p class="text-primary">perfeito para você.</p>
                </div>
                <p class="mt-6 max-w-lg text-lg text-text">
                    A DOMUS conecta você aos melhores imóveis. <br>
                    para alugar com praticidade, segurança e confiança.
                </p>
            </div>
            <Div class="flex mt-8">
                <SimpleLanding :infos="info" v-for="info in landing.simpleCard" />
            </Div>
        </div>

    </div>
</template>