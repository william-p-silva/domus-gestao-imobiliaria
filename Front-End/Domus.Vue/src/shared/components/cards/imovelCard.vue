<script setup lang="ts">
import type { ImovelResponse } from '@/shared/types/imovel/imovelResponse';
import { BedDouble, Heart, MapPin, SquareDashed, Toilet } from '@lucide/vue';
import imovelImage from '@/assets/imgs/LandingPage/contratoImovel.jpeg'
import { FormatterNumber } from '@/shared/utils/formatter/formatterNumber';


const props = defineProps<{
    imovel: ImovelResponse
}>();

const formatter = FormatterNumber();

const sizeIcon = 14;
</script>



<template>
    <div class="bg-background shadow-sm shadow-primary-light/20 flex flex-col h-90 min-w-60 max-w-60 rounded-2xl border border-primary/20 bg-cover">
        <div class="h-[55%] bg-cover py-2 px-2 rounded-t-2xl" :style="{ backgroundImage: `url(${imovelImage})` }">
            <div class="flex w-full justify-between">
                <p class="bg-white rounded-md p-1 text-primary text-sm">{{ imovel.tipoDoImovel }}</p>
                <div class="text-white ">
                    <Heart />
                </div>
            </div>
        </div>
        <div class="py-2 px-4 text-primary-light h-[45%] flex flex-col justify-between">

            <div>
                <p class="text-primary-dark font-bold">
                    {{ imovel.titulo }}
                </p>
            </div>

            <div>
                <p class="flex justify-start items-center gap-1 text-sm">
                    <MapPin :size="sizeIcon" /> {{ imovel.endereco.cidade }}, {{ imovel.endereco.uf.toUpperCase() }}
                </p>
            </div>

            <div class="flex justify-between">
                <p class="flex justify-start items-center gap-1 text-sm">
                    <BedDouble :size="sizeIcon" /> 
                    {{ imovel.comodos }}
                </p>

                <p class="flex justify-start items-center gap-1 text-sm">
                    <Toilet :size="sizeIcon" />
                    {{ imovel.banheiros }}
                </p>

                <p class="flex justify-start items-center gap-1 text-sm">
                    <SquareDashed :size="sizeIcon" />
                    {{ formatter.metrosQuadrados(imovel.metrosQuadrados) }} 
                </p>
            </div>

            <div>
                <p class="font-bold text-primary-dark text-lg"> {{ formatter.moeda(imovel.valorAluguel) }} /mês</p>
            </div>
        </div>
    </div>
</template>