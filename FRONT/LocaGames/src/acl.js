import Vue from 'vue'
import { AclInstaller, AclCreate, AclRule } from 'vue-acl'
import router from './router'

Vue.use(AclInstaller)

let initialRole = 'public'

const token = localStorage.getItem('accessToken')
if (token) initialRole = 'admin'

export default new AclCreate({
  initial: initialRole,
  notfound: '/login',
  router,
  acceptLocalRules: true,
  globalRules: {
    admin: new AclRule('admin').generate(),
    public: new AclRule('public').or('admin').generate()
  }
})
