<script setup lang="ts">
import { ShieldCheck, UserShield, Heart  } from '@lucide/vue';

import type { SimpleCardConfig } from '../../types/common';

const props = defineProps<{
    infos: SimpleCardConfig
}>();

</script>





<template>
    <div class="flex justify-center items-center gap-2 w-full">
        <div class="bg-primary/10 rounded-2xl border border-primary/30 w-14 h-14 flex justify-center items-center text-text">
            <ShieldCheck v-if="infos.icon === 'ShieldCheck'" />
            <UserShield v-if="infos.icon === 'UserShield'" />
            <Heart v-if="infos.icon === 'Heart'"/>
        </div>
        <div class="flex flex-col wrap-break-word text-text w-33">
            <h3 class="font-bold text-primary">{{ infos.title }}</h3>
            <p >{{infos.text}}</p>
        </div>
    </div>
</template>