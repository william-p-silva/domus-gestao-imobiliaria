<script setup lang="ts">
import InputBusca from '@/features/landingPage/components/inputBusca.vue';
import { useChat } from '../../hooks/useChat';
import { ListFilter } from '@lucide/vue';




const { chatsUser,inputFiltro } = useChat();
</script>


<template>
    <header class="flex flex-col gap-2 px-4 ">
        <div class="flex gap-2">
            <h1 class="text-2xl font-bold text-primary-dark">Meus Chats</h1>
            <span class="flex justify-center items-center rounded-full bg-primary-light/30 w-8 h-8">
                {{ chatsUser.length }}
            </span>
        </div>

        <div class="flex gap-4">
            <InputBusca :infos="inputFiltro" clas-name="p-2 rounded-lg"/>
            <button class="flex justify-center items-center px-2 text-primary rounded-lg border border-primary/20 cursor-pointer">
                <ListFilter />
            </button>
        </div>

    </header>
</template>

<script setup lang="ts">

</script>