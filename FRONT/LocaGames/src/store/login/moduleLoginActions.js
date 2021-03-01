/* eslint-disable no-empty-pattern */

import login from '../../http/requests/login/index.js'

export default {
  login ({ dispatch }, payload) {
    return new Promise((resolve, reject) => {
      login.login(payload)
        .then(response => {
          if (response.data.status) {
            dispatch('setUser', response)

            resolve(response)
          }

          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },

  setUser ({ commit }, response) {
    localStorage.setItem('accessToken', response.data.accessToken)

    commit('SET_BEARER', response.data.accessToken)
  }

}
