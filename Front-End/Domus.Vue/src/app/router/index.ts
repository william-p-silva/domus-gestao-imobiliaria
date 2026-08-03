import AppLayout from '@/shared/layouts/appLayout.vue'
import { createRouter, createWebHistory } from 'vue-router'
import Home from '../view/home.vue'
import CadastroView from '../view/auth/CadastroView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: AppLayout,
      children: [
        {
          path: "/",
          name: "Landing Page",
          component: Home
        },
        {
          path: "/auth/cadastro",
          name: "Cadastro",
          component: CadastroView
        }
      ]
    },
  ],
})

export default router
