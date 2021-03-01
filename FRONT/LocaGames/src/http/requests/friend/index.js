import axios from '../../axios'

export default {
  getPagination (payLoad) {
    return axios.get(`/friend/${payLoad.page}/${payLoad.size}`)
  },
  obter (id) {
    return axios.get(`/friend/${id}`)
  },
  getFriendsSelectList (id) {
    return axios.get('/friend/getFriendsSelectList')
  },
  add (payload) {
    return axios.post('/friend', payload)
  },
  update (payload) {
    return axios.put('/friend', payload)
  },
  delete (id) {
    return axios.delete(`/friend/${id}`)
  }
}
