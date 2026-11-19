<template>
    <div class="flex w-full flex-col gap-3 rounded-2xl border border-slate-100 bg-white p-3 sm:flex-row sm:items-center sm:justify-between sm:gap-4 sm:p-4">
        <div v-if="$slots.search" class="min-w-0 flex-1">
            <slot name="search" />
        </div>

        <div v-if="$slots.filters" class="flex flex-wrap items-center gap-2">
            <slot name="filters" />
        </div>

        <div v-if="$slots.actions" class="flex shrink-0 items-center gap-2 sm:ml-auto">
            <slot name="actions" />
        </div>

        <slot />
    </div>
</template>
