<script setup lang="ts">
import AuthInput from '@/shared/components/inputs/authInput.vue';
import { useCadastro } from '../../hooks/useCadastro';
import Error from '../error.vue';
import Header from '../header.vue';
import Button from '../button.vue';
import AuthToggle from '../authToggle.vue';

const cadastro = useCadastro();


async function handleSubmit() {
    await cadastro.handleSubmit();
}
</script>

<template>
    <form @submit.prevent="handleSubmit" class="
            flex
            w-full
            flex-col
            gap-5
        ">
        <!-- Header -->
        <Header title="Cadastre-se" description="Crie sua conta e encontre seu novo lar." />


        <!-- Nome -->
        <AuthInput label="Nome" type="text" placeholder="Digite seu nome"
            v-model="cadastro.requestCadastro.value.nome" />

        <!-- Email -->
        <AuthInput label="Email" type="email" placeholder="Digite seu email"
            v-model="cadastro.requestCadastro.value.email" />

        <!-- Senha -->
        <AuthInput label="Senha" type="password" placeholder="Digite sua senha"
            v-model="cadastro.requestCadastro.value.senha" />

        <!-- Error -->
        <Error v-if="cadastro.errorCadastro.value" :error="cadastro.errorCadastro.value" />

        <!-- Submit -->
        <Button value="Cadastrar" :isLoading="cadastro.isLoading.value" />

        <!-- Login -->
        <AuthToggle description="Já possui uma conta?" value="Entrar" target="login" />

    </form>
</template>