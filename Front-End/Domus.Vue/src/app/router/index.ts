import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/core/configuration/authentication.ts'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Landing",
      component: () => import("@/shared/layouts/appLayout.vue"),
      children: [
        {
          path: "",
          name: "LandingPage",
          component: () => import("@/app/view/LandingPageView.vue")
        },
      ]
    },
    {
      path: "/auth",
      name: "Auth",
      component: () => import("@/shared/layouts/authLayout.vue"),
      children: [
        {
          path: "cadastro",
          name: "Cadastro",
          component: () => import("@/app/view/auth/AuthView.vue")
        },
        {
          path: "login",
          name: "Login",
          component: () => import("@/app/view/auth/AuthView.vue")
        },
        {
          path: "",
          name: "AuthHome",
          component: () => import("@/app/view/auth/AuthView.vue")
        },
      ]
    },
    {
      path: "/locador",
      name: "Locador",
      component: () => import("@/shared/layouts/appLayout.vue"),
      meta: { requiresAuth: true, roles: ["Locador"] },
      redirect: "/locador/dashboard",
      children: [
        {
          path: "dashboard",
          name: "LocadorDashboard",
          component: () => import("@/app/view/locador/HomeLocadorView.vue")
        }
      ],
    },
    {
      path: "/locatario",
      name: "Locatario",
      component: () => import("@/shared/layouts/appLayout.vue"),
      meta: { requiresAuth: true, roles: ["Locatario"] },
      redirect: "/locatario/home",
      children: [
        {
          path: "home",
          name: "LocatarioHome",
          component: () => import("@/app/view/locatario/HomeLocatarioView.vue")
        }
      ]
    },
    {
      // Rota coringa para capturar URLs inexistentes (404)
      path: "/:pathMatch(.*)*",
      redirect: "/"
    }
  ],
})

router.beforeEach(async (to) => {
  const authStore = useAuthStore();

  if (authStore.isCheckingAuth) {
      await authStore.checkAuth();
  }

  const requiresAuth = to.matched.some(
      record => record.meta.requiresAuth
  );

  const requiresGuest = to.matched.some(
      record => record.meta.requiresGuest
  );

  // Usuário não autenticado tentando acessar rota protegida
  if (requiresAuth && !authStore.isLogged) {
      return {
          name: "Login",
          query: {
              redirect: to.fullPath
          }
      };
  }

  // Usuário autenticado tentando acessar rota de convidado
  if (requiresGuest && authStore.isLogged) {
      return {
          name: "Login"
      };
  }

  const allowedRoles = to.meta.roles as string[] | undefined;

  if (allowedRoles && authStore.isLogged) {
      const hasPermission = authStore.userLogged.perfil.some(
          role => allowedRoles.includes(role)
      );

      if (!hasPermission) {
          return {
              name: "Login"
          };
      }
  }

  return true;
});

export default router
