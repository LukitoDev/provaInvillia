import Vue from 'vue'

Vue.filter('platform', function (value) {
  value = Number(value)
  let retorno = ''
  switch (value) {
    case 0:
      retorno = 'PC'
      break
    case 1:
      retorno = 'PS4'
      break
    case 2:
      retorno = 'PS5'
      break
    case 3:
      retorno = 'Xbox One'
      break
    case 4:
      retorno = 'Xbox Series S'
      break
    case 5:
      retorno = 'Xbox Series X'
      break
  }
  return retorno
})

Vue.filter('genre', function (value) {
  value = Number(value)
  let retorno = ''
  switch (value) {
    case 0:
      retorno = 'História'
      break
    case 1:
      retorno = 'FPS'
      break
    case 2:
      retorno = 'Luta'
      break
    case 3:
      retorno = 'Sobrevivência'
      break
    case 4:
      retorno = 'Ação'
      break
    case 5:
      retorno = 'RPG'
      break
  }
  return retorno
})

Vue.filter('typeMessage', function (value) {
  if (isNaN(Number(value))) return value

  value = Number(value)

  let retorno = value
  switch (value) {
    case 0:
      retorno = 'success'
      break
    case 1:
      retorno = 'error'
      break
    case 2:
      retorno = 'warning'
      break
  }
  return retorno
})
