import axios from '../../axios'

export default {
  login (payload) {
    return axios.post('/user', {
      email: payload.email,
      pwd: payload.pwd
    })
  }
}
