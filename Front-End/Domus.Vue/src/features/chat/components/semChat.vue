
<template>
  <div class="flex h-full w-full flex-col items-center justify-center px-6 text-center">
    <!-- Ícone -->
    <div
      class="mb-5 flex h-20 w-20 items-center justify-center rounded-full
             bg-primary-soft text-primary"
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="1.5"
        class="h-10 w-10"
      >
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M8 10h8M8 14h5"
        />
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M19.5 15.5a8.5 8.5 0 0 0-7.5-12A8.5 8.5 0 0 0 4.5 15.5L3 21l5.5-1.5a8.5 8.5 0 0 0 11-4Z"
        />
      </svg>
    </div>

    <!-- Texto -->
    <h2 class="text-lg font-semibold text-text">
      Nenhuma conversa selecionada
    </h2>

    <p class="mt-2 max-w-sm text-sm leading-relaxed text-text-muted">
      Selecione uma conversa ao lado para começar a conversar sobre um imóvel.
    </p>
  </div>
</template>

