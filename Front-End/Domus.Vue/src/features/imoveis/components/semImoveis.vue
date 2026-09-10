
<template>
  <div
    class="flex w-full h-full  flex-col items-center justify-center
           px-6 text-center"
  >
    <!-- Ícone -->
    <div
      class="mb-6 flex h-20 w-20 items-center justify-center
             rounded-full bg-primary-soft
             text-primary"
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="1.5"
        class="h-10 w-10 "
      >
        <!-- Casa -->
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="m3 10 9-7 9 7"
        />

        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M5 9.5V21h14V9.5"
        />

        <!-- Porta -->
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M9 21v-6h6v6"
        />

        <!-- Busca -->
        <circle cx="17.5" cy="17.5" r="3" />
        <path
          stroke-linecap="round"
          d="m20 20 1.5 1.5"
        />
      </svg>
    </div>

    <!-- Título -->
    <h2
      class="text-xl font-bold tracking-tight
             text-text"
    >
      Nenhum imóvel encontrado
    </h2>

    <!-- Descrição -->
    <p
      class="mt-2 max-w-md text-sm leading-relaxed
             text-text-muted sm:text-base"
    >
      Não encontramos imóveis que correspondam aos critérios
      selecionados. Tente ajustar ou remover alguns filtros.
    </p>

    <!-- Ação opcional -->
    <button v-if="button"
      type="button"
      class="mt-7 inline-flex items-center gap-2 rounded-xl
             border border-primary
             px-5 py-2.5 text-sm font-semibold
             text-primary
             transition-all duration-200
             hover:bg-primary
             hover:text-white
             focus:outline-none focus:ring-2
             focus:ring-primary-light
             focus:ring-offset-2"
      @click="$emit('clear-filters')"
    >
      Limpar filtros
    </button>
  </div>
</template>

<script setup lang="ts">
defineProps({
    button: {
        type: Boolean,
        defaut: true
    }
})


defineEmits<{
  'clear-filters': []
}>()
</script>
