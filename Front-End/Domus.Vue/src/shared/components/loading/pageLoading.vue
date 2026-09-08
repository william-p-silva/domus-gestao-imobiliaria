
<template>
  <Transition name="loading">
    <div
      v-if="visible"
      class="fixed inset-0 z-9999 flex items-center justify-center
             bg-slate-950/95 backdrop-blur-sm "
      role="status"
      aria-live="polite"
      aria-label="Carregando página"
    >
      <div class="flex flex-col items-center gap-6">

        <!-- Casinha -->
        <svg
          viewBox="0 0 240 210"
          class="h-48 w-48 text-sky-400 sm:h-56 sm:w-56"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <!-- Chaminé -->
          <path
            d="M158 66V42H174V77"
            class="house-line house-line-1"
          />

          <!-- Telhado -->
          <path
            d="M35 92L120 25L205 92"
            class="house-line house-line-2"
          />

          <!-- Parede esquerda -->
          <path
            d="M50 82V170"
            class="house-line house-line-3"
          />

          <!-- Parede direita -->
          <path
            d="M190 82V170"
            class="house-line house-line-4"
          />

          <!-- Base -->
          <path
            d="M42 170H198"
            class="house-line house-line-5"
          />

          <!-- Porta -->
          <path
            d="M102 170V115H138V170"
            class="house-line house-line-6"
          />

          <!-- Maçaneta -->
          <circle
            cx="130"
            cy="143"
            r="2.5"
            class="house-dot"
          />

          <!-- Janela -->
          <path
            d="M153 108H178V133H153V108Z"
            class="house-line house-line-7"
          />

          <!-- Cruz da janela -->
          <path
            d="M165.5 108V133M153 120.5H178"
            class="house-line house-line-8"
          />

          <!-- Arbusto esquerdo -->
          <path
            d="M50 170C38 170 32 162 37 153C40 147 46 145 52 148C54 139 62 135 69 139C75 142 77 149 75 155C82 154 88 159 88 165C88 168 86 170 82 170"
            class="house-line house-line-9"
          />

          <!-- Arbusto direito -->
          <path
            d="M190 170C202 170 208 162 203 153C200 147 194 145 188 148C186 139 178 135 171 139C165 142 163 149 165 155C158 154 152 159 152 165C152 168 154 170 158 170"
            class="house-line house-line-10"
          />
        </svg>

        <!-- Texto -->
        <div class="flex items-center gap-1 text-sm font-medium tracking-wide text-slate-300">
          <span>Carregando</span>

          <span class="loading-dot">.</span>
          <span class="loading-dot [animation-delay:150ms]">.</span>
          <span class="loading-dot [animation-delay:300ms]">.</span>
        </div>

      </div>
    </div>
  </Transition>
</template>


<script setup lang="ts">
import { onBeforeUnmount, watch } from 'vue'

const props = defineProps<{
  visible: boolean
}>()

function setScrollLock(locked: boolean) {
  if (typeof document === 'undefined') return

  const html = document.documentElement
  const body = document.body

  if (locked) {
    html.classList.add('loading-active')
    body.classList.add('loading-active')
  } else {
    html.classList.remove('loading-active')
    body.classList.remove('loading-active')
  }
}

watch(
  () => props.visible,
  (visible) => {
    setScrollLock(visible)
  },
  { immediate: true }
)

onBeforeUnmount(() => {
  setScrollLock(false)
})
</script>



<style scoped>
/* =========================
   Desenho da casa
   ========================= */

.house-line {
  stroke: currentColor;
  stroke-width: 3;
  stroke-linecap: round;
  stroke-linejoin: round;

  /*
   * A animação simula o traço
   * sendo desenhado.
   */
  stroke-dasharray: 500;
  stroke-dashoffset: 500;

  animation: draw-house 2.8s ease-in-out infinite;
}

.house-dot {
  fill: currentColor;
  opacity: 0;
  animation: appear-dot 2.8s ease-in-out infinite;
}

/* Pequeno atraso entre cada parte */
.house-line-1 {
  animation-delay: 0s;
}

.house-line-2 {
  animation-delay: 0.15s;
}

.house-line-3 {
  animation-delay: 0.3s;
}

.house-line-4 {
  animation-delay: 0.4s;
}

.house-line-5 {
  animation-delay: 0.55s;
}

.house-line-6 {
  animation-delay: 0.7s;
}

.house-line-7 {
  animation-delay: 0.9s;
}

.house-line-8 {
  animation-delay: 1s;
}

.house-line-9 {
  animation-delay: 1.15s;
}

.house-line-10 {
  animation-delay: 1.3s;
}

@keyframes draw-house {
  0% {
    stroke-dashoffset: 500;
    opacity: 0.3;
  }

  15% {
    opacity: 1;
  }

  55% {
    stroke-dashoffset: 0;
    opacity: 1;
  }

  80% {
    stroke-dashoffset: 0;
    opacity: 1;
  }

  100% {
    stroke-dashoffset: 500;
    opacity: 0.3;
  }
}

@keyframes appear-dot {
  0%,
  45% {
    opacity: 0;
  }

  55%,
  80% {
    opacity: 1;
  }

  100% {
    opacity: 0;
  }
}

/* =========================
   Pontinhos do texto
   ========================= */

.loading-dot {
  animation: loading-dot 1.2s ease-in-out infinite;
}

@keyframes loading-dot {
  0%,
  60%,
  100% {
    opacity: 0.2;
  }

  30% {
    opacity: 1;
  }
}

/* =========================
   Entrada / saída
   ========================= */

.loading-enter-active,
.loading-leave-active {
  transition:
    opacity 0.25s ease,
    backdrop-filter 0.25s ease;
}

.loading-enter-from,
.loading-leave-to {
  opacity: 0;
}


:global(html.loading-active),
:global(body.loading-active) {
  overflow: hidden !important;
}

/*
 * Evita que o layout "pule" quando a scrollbar desaparece.
 */
:global(html) {
  scrollbar-gutter: stable;
}
</style>

