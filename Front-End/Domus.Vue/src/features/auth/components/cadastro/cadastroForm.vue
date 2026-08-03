<script setup lang="ts">
import { useCadastro } from '../../hooks/useCadastro';

const cadastro = useCadastro();

async function handleSubmit() {
    await cadastro.handleSubmit();
}
</script>


<template>
    <form @submit.prevent="handleSubmit">

        <div>
            <h1>Cadastre-se</h1>
        </div>

        <div>
            <label for="nome">Nome:</label>
            <input type="text" name="nome" id="" 
            placeholder="Digite seu nome" 
            v-model="cadastro.requestCadastro.value.nome" />
        </div>

        <div>
            <label for="email">Email:</label>
            <input type="text" name="email" id="" 
            placeholder="Digite seu email" 
            v-model="cadastro.requestCadastro.value.email" />
        </div>

        <div>
            <label for="senha">Senha:</label>
            <input type="password" name="senha" id="" 
            placeholder="Digite seu senha" 
            v-model="cadastro.requestCadastro.value.senha" />
        </div>

        <div>
            <input type="submit" value="Cadastrar" />
        </div>

        <div>
            <p>{{ cadastro.errorCadastro.value }}</p>
        </div>
    </form>
</template>