import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/core/configuration/authentication.ts'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: () => import("@/shared/layouts/appLayout.vue"),
      children: [
        {
          path: "",
          name: "Landing Page",
          component: () => import("@/app/view/home.vue")
        },
      ]
    },
    {
      path: "/auth",
      name: "Auth",
      component: () => import("@/shared/layouts/appLayout.vue"),
      meta: { requiresGuest: true },
      redirect: "/auth/login",
      children: [
        {
          path: "cadastro",
          name: "Cadastro",
          component: () => import("@/app/view/auth/CadastroView.vue")
        },
        {
          path: "login",
          name: "Login",
          component: () => import("@/app/view/auth/LoginView.vue")
        },
        {
          path: "",
          name: "Auths",
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
          name: "Dashboard",
          component: () => import("@/app/view/locador/HomeLocadorView.vue")
        }
      ],
    },
    {
      // Rota coringa para capturar URLs inexistentes (404)
      path: "/:pathMatch(.*)*",
      redirect: "/"
    }
  ],
})

router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore();

  if (authStore.isCheckingAuth) {
    await authStore.checkAuth();
  }

  const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);
  const requiresGuest = to.matched.some((record) => record.meta.requiresGuest);

  if (requiresAuth && !authStore.isLogged) {
    return next({ name: 'Login', query: { redirect: to.fullPath } });
  }

  if (requiresGuest && authStore.isLogged) {
    return next({ name: 'Home' });
  }

  const allowRoles = to.meta.roles as string[] | undefined;
  if (allowRoles && authStore.isLogged) {
    const hasPermission = authStore.userLogged.perfil.some((role) => allowRoles.includes(role));

    if (!hasPermission) {
      return next({ name: "Home" });
    }
  }

  next();
});

export default router
