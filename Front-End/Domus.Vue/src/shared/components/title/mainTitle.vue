
<script setup lang="ts">
import { THEME_COLORS, type ThemeColor } from "@/shared/components/title/constants/themeColors";

export interface ModeIndicator {
    label: string;
    color: ThemeColor;
}

interface Props {
    title?: string;
    subTitle?: string;
    modeIndicator?: ModeIndicator;
}

const props = withDefaults(defineProps<Props>(), {
    title: "",
    subTitle: "",
});
</script>

<template>
    <header class="flex w-full items-center justify-between px-4 py-6">
        <div class="flex flex-col">
            <h1 class="text-2xl font-bold text-primary-dark">
                {{ title }}
            </h1>

            <p v-if="subTitle" class="hidden sm:block">
                {{ subTitle }}
            </p>
        </div>

        <div
            v-if="modeIndicator"
            class="flex items-center gap-2 rounded-full px-3 py-1"
            :class="THEME_COLORS[modeIndicator.color].background"
        >
            <!-- Indicador animado -->
            <div class="relative flex size-2 items-center justify-center">
                <span
                    class="absolute size-full animate-ping rounded-full opacity-75"
                    :class="THEME_COLORS[modeIndicator.color].dot"
                />

                <span
                    class="relative size-2 rounded-full"
                    :class="THEME_COLORS[modeIndicator.color].dot"
                />
            </div>

            <span
                class="text-xs font-medium"
                :class="THEME_COLORS[modeIndicator.color].text"
            >
                {{ modeIndicator.label }}
            </span>
        </div>
    </header>
</template>
