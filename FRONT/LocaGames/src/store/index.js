import Vue from 'vue'
import Vuex from 'vuex'

import moduleGame from './game/moduleGame.js'
import moduleFriend from './friend/moduleFriend.js'
import moduleLogin from './login/moduleLogin.js'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    game: moduleGame,
    login: moduleLogin,
    friend: moduleFriend
  }
})
