/* eslint-disable no-empty-pattern */

import game from '../../http/requests/game/index.js'

export default {
  getPagination ({ }, payLoad) {
    return new Promise((resolve, reject) => {
      game.getPagination(payLoad)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  get ({ }, id) {
    return new Promise((resolve, reject) => {
      game.obter(id)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  add ({ }, payload) {
    return new Promise((resolve, reject) => {
      game.add(payload)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  update ({ }, payload) {
    return new Promise((resolve, reject) => {
      game.update(payload)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  delete ({ }, id) {
    return new Promise((resolve, reject) => {
      game.delete(id)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  borrowGame ({ }, payload) {
    return new Promise((resolve, reject) => {
      game.borrowGame(payload)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  },
  returnGame ({ }, payload) {
    return new Promise((resolve, reject) => {
      game.returnGame(payload)
        .then(response => {
          resolve(response)
        })
        .catch(error => { reject(error) })
    })
  }
}
