// axios
import axios from 'axios'

const baseURL = 'http://host.docker.internal:80'

export default axios.create({
  baseURL
})
