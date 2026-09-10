
<template>
  <main
    class="flex min-h-screen flex-col items-center justify-center
           bg-background px-6 text-center"
  >
    <!-- Número -->
    <span
      class="text-[clamp(7rem,20vw,12rem)] font-extrabold leading-none
             tracking-tight text-primary"
    >
      404
    </span>

    <!-- Ícone -->
    <div
      class="-mt-4 mb-6 flex h-16 w-16 items-center justify-center
             rounded-full bg-primary-soft
             text-primary"
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="1.7"
        class="h-8 w-8"
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
      </svg>
    </div>

    <!-- Mensagem -->
    <h1
      class="text-2xl font-bold tracking-tight
             text-text sm:text-3xl"
    >
      Ops! Página não encontrada
    </h1>

    <p
      class="mt-3 max-w-md text-sm leading-relaxed
             text-text-muted sm:text-base"
    >
      Parece que este endereço não existe ou a página foi movida.
      Mas não se preocupe, podemos te levar de volta para casa.
    </p>

    <!-- Ação -->
    <RouterLink
      to="/"
      class="mt-8 inline-flex items-center gap-2 rounded-xl
             bg-primary px-6 py-3
             text-sm font-semibold text-white
             shadow-sm transition-all duration-200
             hover:-translate-y-0.5 hover:shadow-md
             focus:outline-none focus:ring-2
             focus:ring-primary-light
             focus:ring-offset-2"
    >
      <!-- Home icon -->
      <svg
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="2"
        class="h-4 w-4"
      >
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="m3 10 9-7 9 7v10a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V10Z"
        />
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M9 21v-6h6v6"
        />
      </svg>

      Voltar para o início
    </RouterLink>
  </main>
</template>

