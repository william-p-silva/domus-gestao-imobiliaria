<script setup lang="ts">
import { useLogin } from '../../hooks/useLogin';


const login = useLogin();

async function handleSubmit() {
    await login.handleLogin();
}
</script>



<template>
    <form @submit.prevent="handleSubmit">

        <div>
            <h1>Entre</h1>
        </div>
        <div>
            <label for="email">Email:</label>
            <input type="text" name="email" id="" placeholder="Digite seu email"
                v-model="login.requestLogin.value.email" />
        </div>

        <div>
            <label for="senha">Senha:</label>
            <input type="password" name="senha" id="" placeholder="Digite seu senha"
                v-model="login.requestLogin.value.senha" />
        </div>

        <div>
            <input type="submit" value="Logar" />
        </div>

        <div>
            <p>{{ login.errorLogin.value }}</p>
        </div>
    </form>
</template>