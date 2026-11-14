<script setup lang="ts">
import { THEME_COLORS, type ThemeColor } from "@/shared/components/title/constants/themeColors";

interface ModeIndicator {
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

            <p v-if="subTitle" class="sm:block hidden">
                {{ subTitle }}
            </p>
        </div>

        <div
            v-if="modeIndicator"
            class="flex items-center gap-2 rounded-full px-3 py-1"
            :class="THEME_COLORS[modeIndicator.color].background"
        >
            <div
                class="size-2 rounded-full"
                :class="THEME_COLORS[modeIndicator.color].dot"
            />

            <span
                class="text-xs font-medium"
                :class="THEME_COLORS[modeIndicator.color].text"
            >
                {{ modeIndicator.label }}
            </span>
        </div>
    </header>
</template>