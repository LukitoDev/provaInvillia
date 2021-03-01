import axios from '../../axios'

export default {
  getPagination (payLoad) {
    return axios.get(`/game/${payLoad.page}/${payLoad.size}`)
  },
  obter (id) {
    return axios.get(`/game/${id}`)
  },
  add (payload) {
    return axios.post('/game', payload)
  },
  update (payload) {
    return axios.put('/game', payload)
  },
  delete (id) {
    return axios.delete(`/game/${id}`)
  },
  borrowGame (payload) {
    return axios.post('/borrowedGame/borrowGame', payload)
  },
  returnGame (payload) {
    return axios.post('/borrowedGame/returnGame', payload)
  }
}
