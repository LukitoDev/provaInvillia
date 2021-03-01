import Vue from 'vue'
import App from './App.vue'
import router from './router'
import acl from './acl'
import store from './store'
import vuetify from './plugins/vuetify'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import '@mdi/font/css/materialdesignicons.css'
import './plugins/filters'

// axios
import axios from './axios.js'
Vue.prototype.$http = axios

Vue.config.productionTip = false

new Vue({
  router,
  acl,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
