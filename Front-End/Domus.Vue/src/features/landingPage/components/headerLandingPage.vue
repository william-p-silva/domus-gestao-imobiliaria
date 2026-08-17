<script setup lang="ts">
import logo from "@/assets/logo/LogoMarca.png"
import { useLanding } from "../hooks/useLanding";
import { useAuthStore } from "@/core/configuration/authentication";

const landing = useLanding()

const auth = useAuthStore()
</script>

<template>
    <header class="flex justify-between py-4 px-6 w-full">
        <RouterLink to="/" class="justify-center items-center hidden md:flex shrink-0">
            <img :src="logo" alt="" class="h-10">
        </RouterLink>
        <nav class="flex justify-center items-center min-w-0 overflow-x-auto">
            <ul class="flex md:gap-4 lg:gap-8 gap-8 text-primary font-bold whitespace-nowrap">
                <RouterLink :to="link.route" v-for="link in landing.links">
                    {{ link.label }}
                </RouterLink>
            </ul>
        </nav>
        <div class="gap-2 justify-center items-center font-bold flex shrink-0" v-if="!auth.isLogged">
            <RouterLink to="/auth/login" class="border border-primary p-2 px-4 rounded-lg text-primary">
                Entrar
            </RouterLink>
            <RouterLink to="/auth/cadastro" class="py-2 px-4 bg-primary text-white rounded-lg border border-primary ">
                Cadastrar
            </RouterLink>
        </div>
        <div v-else class="rounded-full bg-white w-12 h-12 flex justify-center items-center">
            {{ auth.userLogged.nome[0] ?? "U" }}
        </div>
    </header>

</template>