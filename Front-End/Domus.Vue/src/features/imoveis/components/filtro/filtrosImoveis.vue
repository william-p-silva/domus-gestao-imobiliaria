<script setup lang="ts">
import InputSelect from '@/shared/components/inputs/inputSelect.vue';
import RangeInput from '@/shared/components/inputs/rangeInput.vue';
import { BedDouble, DollarSign, House, MapPin, PencilRuler, RefreshCcw } from '@lucide/vue';
import { ref } from 'vue';
import { useFiltro } from '../../hooks/filtro/useFiltro';
import BaseButton from '@/shared/components/buttons/baseButton.vue';



const filtro = useFiltro();


async function handleSubmit() {
    await filtro.submit();
}
</script>


<template>
    <aside class="flex w-80 h-full p-2">
        <section
            class="flex flex-col h-full w-full text-primary border border-primary/20 p-4 rounded-2xl shadow-sm shadow-primary/20 gap-6">

            <div class="flex justify-between items-center">
                <h3 class="text-primary-dark text-xl font-bold">Filtros</h3>
                <button class="text-sm flex flex-row items-center gap-1 cursor-pointer" @click="filtro.limparFiltros">
                    Limpar filtros
                    <RefreshCcw :size="16" />
                </button>
            </div>

            <form class="flex flex-col gap-4" @submit.prevent="handleSubmit">
                <div class="flex flex-col gap-2">
                    <p class="flex justify-start items-center gap-1">
                        <MapPin :size="22" />
                        Localização
                    </p>
                    <input type="text"
                        class="p-2 outline-none border border-primary/30 rounded-lg focus:ring focus:ring-primary-light/50"
                        v-model="filtro.endereco.value"
                        placeholder="Ex: Delmiro Gouveia, AL, Fernandes Lima">
                </div>

                <div class="flex flex-col gap-2">
                    <p class="flex justify-start items-center gap-1">
                        <House :size="22" />
                        Tipo do Imóvel
                    </p>
                    <InputSelect v-model="filtro.tipoImovel.value" :options="filtro.tiposImoveis" />
                </div>


                <div class="flex flex-col gap-2">
                    <p class="flex justify-start items-center gap-1">
                        <DollarSign :size="22" />
                        Faixa de Preço
                    </p>
                    <RangeInput v-model="filtro.faixaPreco.value" :min="0" :max="8000" :step="100" />
                </div>

                <div class="flex flex-col gap-2">
                    <p class="flex justify-start items-center gap-1">
                        <BedDouble :size="22" />
                        Quartos
                    </p>
                    <div class="grid grid-cols-4 gap-2">
                        <div v-for="num in filtro.numObj" @click="filtro.numQuartos.value = num"
                            class="rounded-lg border  px-4 py-2 cursor-pointer text-center transition-all duration-300"
                            :class="filtro.numQuartos.value === num ? ' border-accent' : 'border-primary/20'">
                            {{ num }} <span v-if="num === 4">+</span>
                        </div>
                    </div>

                </div>

                <div class="flex flex-col gap-2">
                    <p class="flex justify-start items-center gap-1">
                        <BedDouble :size="22" />
                        Quartos
                    </p>
                    <div class="grid grid-cols-4 gap-2">
                        <div v-for="num in filtro.numObj" @click="filtro.numBanheiros.value = num"
                            class="rounded-lg border  px-4 py-2 cursor-pointer text-center transition-all duration-300"
                            :class="filtro.numBanheiros.value === num ? ' border-accent' : 'border-primary/20'">
                            {{ num }} <span v-if="num === 4">+</span>
                        </div>
                    </div>

                </div>

                <div class="flex flex-col gap-2">
                    <p class="flex justify-start items-center gap-1">
                        <PencilRuler :size="22" />
                        Área mínima (M²)
                    </p>
                    <div class="grid grid-cols-2 gap-2">
                       <input type="number" class="p-2 outline-none border border-primary/30 rounded-lg focus:ring focus:ring-primary-light/50
                       [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none
                       " 
                       v-model="filtro.minArea.value"
                       placeholder="De">

                       <input type="number" class="p-2 outline-none border border-primary/30 rounded-lg focus:ring focus:ring-primary-light/50 
                       [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none
                       " 
                       v-model="filtro.maxArea.value"
                       placeholder="Até">

                    </div>

                </div>

                <div>
                    <BaseButton value="Buscar Imóveis" :isLoading="filtro.isLoading.value" />
                </div>

            </form>

        </section>
    </aside>
</template>