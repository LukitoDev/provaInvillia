/* eslint-disable no-empty-pattern */

import friend from '../../http/requests/friend/index.js'

export default {
  getPagination ({ }, payLoad) {
    return new Promise((resolve, reject) => {
      friend.getPagination(payLoad)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  get ({ }, id) {
    return new Promise((resolve, reject) => {
      friend.obter(id)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  getFriendsSelectList  ({ }) {
    return new Promise((resolve, reject) => {
      friend.getFriendsSelectList()
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  add ({ }, payload) {
    return new Promise((resolve, reject) => {
      friend.add(payload)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  update ({ }, payload) {
    return new Promise((resolve, reject) => {
      friend.update(payload)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  delete ({ }, id) {
    return new Promise((resolve, reject) => {
      friend.delete(id)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  }
}
