<script setup lang="ts">
import { Clock, Headset, ShieldLock, Users } from '@lucide/vue';
import type { BeneficiosCard } from '../../types/common';

const props = defineProps<{
    beneficio: BeneficiosCard
}>();

const icon = props.beneficio.icon === 'shield' ? ShieldLock : props.beneficio.icon === 'hour' ? Clock : props.beneficio.icon === 'user' ? Users : Headset

</script>

<template>
    <div class="flex flex-col gap-2">
        <icon :size="28" class="text-primary" />
        <div>
            <h3 class="text-text font-bold text-sm">{{ beneficio.title }}</h3>
            <p class="text-primary-light text-sm">{{ beneficio.text }}</p>
        </div>
    </div>
</template>