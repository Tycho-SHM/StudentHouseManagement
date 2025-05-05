import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/dashboard',
      component: () => import('../views/dashboard/Layout.vue'),
      children: [
        {
          path: 'settings',
          name: 'settings',
          children: [
            {
              path: 'account',
              name: 'accountSettings',
              component: () => import('../views/dashboard/settings/AccountView.vue')
            }
          ]
        }
      ]
    },
    {
      path: '/account',
      component: () => import('../views/account/Layout.vue'),
      children: [
        {
          path: 'sign-in',
          name: 'signIn',
          component: () => import('../views/account/SignInView.vue')
        },
        {
          path: 'sign-up',
          name: 'signUp',
          component: () => import('../views/account/SignUpView.vue')
        }
      ]
    },
    {
      path: '/onboarding',
      component: () => import('../views/onboarding/Layout.vue'),
      children: [
        {
          path: 'profile-setup',
          name: 'profileSetup',
          component: () => import('../views/onboarding/ProfileSetup.vue')
        }
      ]
    }
  ],
})

export default router
