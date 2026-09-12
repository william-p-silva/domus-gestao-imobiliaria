<script setup lang="ts">
import FooterDomus from '../components/footer/footerDomus.vue';
import HeaderUser from '../components/header/headerUser.vue';
</script>

<template>
    <main
        class="w-full h-screen max-w-[2000px] m-auto
        2xl:border-x 2xl:border-primary-light/30
        selection:bg-primary selection:text-primary-soft
        flex flex-col overflow-hidden "
    >
        <HeaderUser />
        <section class="flex-1 min-h-0 min-w-0">
            <RouterView />
        </section>
    </main>
</template>