<script setup lang="ts">
import AuthInput from '@/shared/components/inputs/authInput.vue';
import { useLogin } from '../../hooks/useLogin';
import Header from '../header.vue';
import Error from '../error.vue';
import Button from '../button.vue';
import AuthToggle from '../authToggle.vue';
import { authStore } from '../../store/useAuthStore.ts';
import ResetPassword from './resetPassword.vue';

const login = useLogin();
const auth = authStore();
async function handleSubmit() {
    await login.handleLogin();
}
</script>

<template>
    <Transition name="reset" mode="out-in">
        <ResetPassword v-if="auth.isReset" key="reset" />

        <form v-else key="login" @submit.prevent="handleSubmit" class="
            flex
            w-full
            h-full
            flex-col
            gap-5
            justify-center
        ">
            <!-- Header -->
            <Header title="Entre na sua conta" description="Bem-vindo de volta à DOMUS." />

            <!-- Email -->
            <AuthInput label="Email" type="email" placeholder="Digite seu email"
                v-model="login.requestLogin.value.email" />

            <!-- Senha -->
            <AuthInput label="Senha" type="password" placeholder="Digite sua senha"
                v-model="login.requestLogin.value.senha" />

            <!-- Esqueci minha senha -->
            <div class="-mt-2 flex justify-end">
                <button type="button" class="
                    cursor-pointer
                    text-xs
                    font-medium
                    text-primary
                    transition-colors
                    hover:text-primary-light
                " @click="auth.isReset = true">
                    Esqueci minha senha
                </button>
            </div>

            <!-- Error -->
            <Error v-if="login.errorLogin.value" :error="login.errorLogin.value" />

            <!-- Submit -->
            <Button value="Entrar" :isLoading="login.isLoading.value" />

            <!-- Cadastro -->
            <AuthToggle value="Cadastre-se" description="Ainda não possui uma conta?" target="cadastro" />
        </form>
    </Transition>
</template>

<style scoped>
@import '../../styles/authStyle.css'
</style>