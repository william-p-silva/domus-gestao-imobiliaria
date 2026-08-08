<script setup lang="ts">
import CadastroForm from '../cadastro/cadastroForm.vue';
import LoginForm from '../login/loginForm.vue';

</script>


<template>
    <section class="bg-[url('@/assets/backgrounds/background.jpg')] bg-cover bg-center bg-no-repeat min-h-screen min-w-screen flex justify-center items-center">
        <article class="bg-amber-500/30 p-6 rounded-2xl">
            <CadastroForm v-if="true" />
            <LoginForm v-else />
        </article>
    </section>
</template>