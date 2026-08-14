<script setup lang="ts">
import { authStore } from '../../store/useAuthStore.ts';
import CadastroForm from '../cadastro/cadastroForm.vue';
import LoginForm from '../login/loginForm.vue';
import authBackground from '@/assets/backgrounds/auth/authBackground.jpeg';

const auth = authStore();
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
                    
                    min-h-[560px]

                    ease-in-out
                " :class="{
                    '[transform:rotateY(180deg)]':
                        auth.form === 'cadastro'
                }">

                <!-- LOGIN -->
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
                    <LoginForm />
                </article>


                <!-- CADASTRO -->
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
                    <CadastroForm />
                </article>

            </div>
        </div>
    </section>
</template>
