<script setup lang="ts">
import { authStore } from '../../store/useAuthStore.ts';
import CadastroForm from '../cadastro/cadastroForm.vue';
import LoginForm from '../login/loginForm.vue';
import authBackground from '@/assets/backgrounds/auth/authBackground.jpeg';
import logo from '@/assets/logo/logo.png'

const auth = authStore();

function handleExit(){
    auth.router.push("/");
    auth.clear();
}
</script>

<template>

    <section :style="{ backgroundImage: `url(${authBackground})` }" class="
            relative
            min-h-screen
            w-full
            bg-cover
            bg-center
            bg-no-repeat
            flex
            items-center
            justify-center
            overflow-hidden
            px-4
        ">
        <!-- Overlay -->
        <div class="
                absolute
                inset-0
                bg-primary-dark/20
            " />

        <!-- Sair -->
        <button type="button" @click="handleExit" class="
                absolute
                top-5
                left-5
                z-20

                flex
                items-center
                justify-center

                h-11
                w-11

                rounded-xl

                bg-primary-dark/20
                backdrop-blur-md

                text-white/80

                shadow-lg
                shadow-primary-dark/20

                transition-all
                duration-200

                hover:bg-primary-dark/40
                hover:text-white
                hover:border-white/30

                active:scale-95
                cursor-pointer
            " aria-label="Sair">
            <!-- Arrow left -->
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.8"
                stroke="currentColor" class="h-5 w-5">
                <path stroke-linecap="round" stroke-linejoin="round" d="M10.5 19.5 3 12m0 0 7.5-7.5M3 12h18" />
            </svg>
        </button>

        <!-- Logo -->
        <div class="
                absolute
                top-5
                right-5
                z-20

                rounded-xl

                bg-white/20
                backdrop-blur-lg

                px-2
                py-1
                cursor-pointer

                shadow-lg
                shadow-primary-dark/20
            "
            @click="handleExit"
            >
            <img :src="logo" alt="DOMUS" class="h-10 w-auto object-contain" />
        </div>

        <!-- Perspectiva -->
        <div class="
                relative
                z-10
                w-full
                max-w-md
                [perspective:1200px]
            ">
            <!-- Card -->
            <div class="
                    relative
                    w-full
                    [transform-style:preserve-3d]
                    transition-transform
                    duration-700
                    

                    ease-in-out
                " :class="{
                    '[transform:rotateY(180deg)]':
                        auth.form === 'login'
                }">

                <!-- Cadastro -->
                <article class="
                        relative
                        w-full

                        rounded-2xl
                        border
                        border-background/20

                        bg-background/20
                        backdrop-blur-sm

                        p-7

                        shadow-2xl
                        shadow-primary-dark/40

                        ring-1
                        ring-white/10

                        [backface-visibility:hidden]

                    ">
                    <CadastroForm />

                </article>


                <!-- LOGIN -->
                <article class="
                        absolute
                        inset-0
                        w-full

                        rounded-2xl
                        border
                        border-background/20

                        bg-background/20
                        backdrop-blur-sm

                        p-7

                        shadow-2xl
                        shadow-primary-dark/40

                        ring-1
                        ring-white/10

                        [backface-visibility:hidden]
                        [transform:rotateY(180deg)]
                    ">
                    <LoginForm />

                </article>




            </div>
        </div>
    </section>
</template>
