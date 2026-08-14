<script setup lang="ts">
import { authStore } from '../store/useAuthStore';


const props = defineProps<{
    description: string;
    value: string;
    target: 'login' | 'cadastro';
}>();

const auth = authStore();
console.log(auth);

function handleClick() {
    if (props.target === 'login') {
        auth.showLogin();
    } else {
        auth.showCadastro();
    }
}

</script>

<template>
    <div class="text-center">
        <p class="text-sm text-text-muted">
            {{ description }}

            <button type="button" class="
                        font-medium
                        text-primary
                        transition-colors
                        hover:text-primary-light
                        cursor-pointer
                    "
                    @click="handleClick"
                    >
                {{ value }}
            </button>
        </p>
    </div>
</template>