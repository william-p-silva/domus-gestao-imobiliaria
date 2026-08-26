<script setup lang="ts">
import FooterDomus from '../components/footer/footerDomus.vue';
import HeaderUser from '../components/header/headerUser.vue';

</script>



<template>
    <HeaderUser class="selection:bg-primary selection:text-primary-soft" />
    <main class="w-full min-h-screen max-w-[2000px] m-auto 
    2xl:border-x 2xl:border-primary-light/30
    selection:bg-primary selection:text-primary-soft
    ">
        <RouterView />
    </main>
    <FooterDomus class="selection:bg-primary selection:text-primary-soft" />
</template>