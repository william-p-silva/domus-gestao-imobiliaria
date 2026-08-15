<script setup lang="ts">
import AuthInput from '@/shared/components/inputs/authInput.vue';
import Header from '../header.vue';
import Error from '../error.vue';
import Button from '../button.vue';
import AuthToggle from '../authToggle.vue';
import { useLogin } from '../../hooks/useLogin.ts';
import { authStore } from '../../store/useAuthStore.ts';


const login = useLogin();

const auth = authStore();

async function handleSubmit() {
    await login.handleResetPassword();
}

</script>




<template>
    <form @submit.prevent="handleSubmit" class="
            flex
            w-full
            flex-col
            gap-5
            h-full
            justify-center
        ">
        <!-- Header -->
        <Header title="Esqueceu sua senha?" description="Recupere sua conta aqui." />

        <!-- Email -->
        <AuthInput label="Email" type="email" placeholder="Digite seu email" v-model="login.resetPassword.value" />

        <!-- Error -->
        <Error v-if="false" :error="'teste'" />

        <!-- Submit -->
        <Button value="Recuperar" :isLoading="login.isLoading.value" />

        <!-- Login -->
        <AuthToggle description="Lembrou da sua senha?" value="Entrar" target="login" />

    </form>
</template>