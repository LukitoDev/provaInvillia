import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import { AclRule } from 'vue-acl'

Vue.use(VueRouter)

const routes = [
  {
    path: '',
    component: () => import('@/layouts/main/Main.vue'),
    children: [
      {
        path: '',
        redirect: '/home',
        meta: {
          rule: new AclRule('admin').generate()
        }
      },
      {
        path: '/home',
        name: 'Home',
        component: Home,
        meta: {
          rule: new AclRule('admin').generate()
        }
      },
      {
        path: '/game',
        name: 'Game',
        component: () => import(/* webpackChunkName: "game" */ '@/views/Game.vue'),
        meta: {
          rule: new AclRule('admin').generate()
        }
      },
      {
        path: '/friend',
        name: 'Friend',
        component: () => import(/* webpackChunkName: "friend" */ '@/views/Friend.vue'),
        meta: {
          rule: new AclRule('admin').generate()
        }
      }]
  },
  {
    path: '',
    component: () => import('@/layouts/full-page/FullPage.vue'),
    children: [
      {
        path: '/login',
        name: 'page-login',
        component: () => import(/* webpackChunkName: "friend" */ '@/views/Login.vue'),
        meta: {
          rule: new AclRule('public').generate()
        }
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
