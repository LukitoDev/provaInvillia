<template>
<div>
    <v-alert outlined :color="message.type" v-if="messageStart">
        <div>{{message.text}}</div>
    </v-alert>

    <v-data-table :headers="headers" :items="games" :options.sync="options" :server-items-length="totalGames" :loading="loading" class="elevation-1" disable-sort :footer-props="footerProps" loading-text="Carregando jogos" no-data-text="Nenhum jogo encontrado">
        <template v-slot:top>
            <v-toolbar flat>
                <v-toolbar-title>Jogos</v-toolbar-title>
                <v-divider class="mx-4" inset vertical></v-divider>
                <v-spacer></v-spacer>
                <v-dialog v-model="dialog" max-width="900px">
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
                            Adicionar
                        </v-btn>
                    </template>
                    <v-card>
                        <v-card-title>
                            <span class="headline">{{ formTitle }}</span>
                        </v-card-title>

                        <v-card-text>
                            <v-container>
                                <v-row>
                                    <v-col cols="12" sm="6" md="4">
                                        <v-text-field v-model="editedItem.name" label="Nome" ref="name" :rules="[() => !!editedItem.name || 'Nome é obrigatório']"></v-text-field>
                                    </v-col>
                                    <v-col cols="12" sm="6" md="4">
                                      <v-autocomplete no-data-text='Nenhuma plataforma encontrada!' ref="platform"
                                                      v-model="editedItem.platform" :items="platforms" label="Plataforma" item-value="id" item-text="text"
                                                      :rules="[numberRulePlatform]">
                                      </v-autocomplete>
                                    </v-col>
                                    <v-col cols="12" sm="6" md="4">
                                       <v-autocomplete no-data-text='Nenhuma gênero encontrado!' ref="genre"
                                                      v-model="editedItem.genre" :items="genres" label="Gênero" item-value="id" item-text="text"
                                                      :rules="[numberRuleGenre]">
                                      </v-autocomplete>
                                    </v-col>
                                </v-row>
                            </v-container>
                        </v-card-text>

                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" text @click="close">
                                Cancelar
                            </v-btn>
                            <v-btn color="blue darken-1" text @click="save">
                                Salvar
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
                <v-dialog v-model="dialogDelete" max-width="420px">
                    <v-card>
                        <v-card-title class="headline">Deseja mesmo deletar esse jogo?</v-card-title>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" text @click="closeDelete">Cancelar</v-btn>
                            <v-btn color="blue darken-1" text @click="deleteItemConfirm">Deletar</v-btn>
                            <v-spacer></v-spacer>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
                <v-dialog v-model="dialogLend " max-width="500px">
                    <v-card>
                        <v-card-title>
                            <span class="headline">Emprestar jogo</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container>
                                <v-row>
                                    <v-col cols="12" sm="6" md="6">
                                      <v-autocomplete ref="idGame" v-model="lendGame.idGame" :items="gameSelected" label="Jogo" item-value="id" item-text="text" disabled >

                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="12" sm="6" md="6">
                                        <v-autocomplete no-data-text='Nenhum amigo encontrado!' ref="friend" v-model="lendGame.friend" :items="friends" label="Amigo" item-value="id" item-text="text" :rules="[() => !!lendGame.friend || 'Amigo é obrigatório']">

                                        </v-autocomplete>
                                    </v-col>
                                </v-row>
                            </v-container>
                        </v-card-text>

                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" text @click="closeLend">
                                Cancelar
                            </v-btn>
                            <v-btn color="blue darken-1" text @click="saveLend">
                                Salvar
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
                <v-dialog v-model="dialogReturnGame" max-width="555px">
                    <v-card>
                        <v-card-title class="headline">Deseja realmente realizar a devolução do jogo?</v-card-title>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" text @click="closeReturnGame">Não</v-btn>
                            <v-btn color="blue darken-1" text @click="returnGameConfirm">Sim</v-btn>
                            <v-spacer></v-spacer>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-toolbar>
        </template>
         <template v-slot:item.platform="{ item }">
           {{item.platform | platform}}
        </template>
        <template v-slot:item.genre="{ item }">
           {{item.genre | genre}}
        </template>
        <template v-slot:item.actions="{ item }">
            <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                    <v-icon small class="mr-2" @click="editItem(item)" v-bind="attrs" v-on="on">
                        mdi-pencil
                    </v-icon>
                </template>
                <span>Editar</span>
            </v-tooltip>

            <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                    <v-icon small @click="deleteItem(item)" v-bind="attrs" v-on="on">
                        mdi-delete
                    </v-icon>
                </template>
                <span>Excluir</span>
            </v-tooltip>

            <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                    <v-icon small @click="lendItem(item)" v-bind="attrs" v-on="on">
                        mdi-arrow-right
                    </v-icon>
                </template>
                <span>Emprestar</span>
            </v-tooltip>

            <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                    <v-icon small @click="returnGame(item)" v-bind="attrs" v-on="on">
                        mdi-arrow-left
                    </v-icon>
                </template>
                <span>Devolver</span>
            </v-tooltip>
        </template>
    </v-data-table>
</div>
</template>

<script>
export default {
  data: () => ({
    numberRulePlatform: v => {
      if (!isNaN(parseFloat(v)) && v >= 0 && v <= 999) return true
      return 'Plataforma é obrigatória'
    },
    numberRuleGenre: v => {
      if (!isNaN(parseFloat(v)) && v >= 0 && v <= 999) return true
      return 'Gênero é obrigatória'
    },
    page: 1,
    itemsPerPage: 10,
    dialog: false,
    dialogDelete: false,
    dialogLend: false,
    dialogReturnGame: false,
    totalGames: 0,
    loading: false,
    headers: [{
      text: 'Nome',
      value: 'name'
    },
    {
      text: 'Plataforma',
      value: 'platform'
    },
    {
      text: 'Gênero',
      value: 'genre'
    },
    {
      text: 'Jogo emprestado com ',
      value: 'gameLendsWith'
    },
    {
      text: 'Actions',
      value: 'actions'
    }
    ],
    games: [],
    options: {},
    footerProps: {
      'items-per-page-options': [5, 10, 15, 20],
      'items-per-page-text': 'Jogos por página'
    },
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      platform: '',
      genre: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      platform: '',
      genre: ''
    },
    lendGame: {
      idGame: 0,
      friend: ''
    },
    friends: [],
    returnGameForm: {
      idGame: 0,
      idFriend: 0
    },
    formHasErrors: false,
    messageStart: false,
    message: {
      text: '',
      type: ''
    },
    platforms: [
      {
        id: 0,
        text: 'PC'
      },
      {
        id: 1,
        text: 'PS4'
      },
      {
        id: 2,
        text: 'PS5'
      },
      {
        id: 3,
        text: 'Xbox One'
      },
      {
        id: 4,
        text: 'Xbox Series S'
      },
      {
        id: 5,
        text: 'Xbox Series X'
      }
    ],
    genres: [
      {
        id: 0,
        text: 'História'
      },
      {
        id: 1,
        text: 'FPS'
      },
      {
        id: 2,
        text: 'Luta'
      },
      {
        id: 3,
        text: 'Sobrevivência'
      },
      {
        id: 4,
        text: 'Ação'
      },
      {
        id: 5,
        text: 'RPG'
      }
    ],
    gameSelected: []
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'Adicionar Item' : 'Editar Item'
    },
    lendGameForm () {
      return {
        friend: this.lendGame.friend,
        idGame: this.lendGame.idGame
      }
    },
    gameForm () {
      return {
        name: this.editedItem.name,
        platform: this.editedItem.platform,
        genre: this.editedItem.genre
      }
    }
  },

  watch: {
    dialog (val) {
      val || this.close()
    },
    dialogDelete (val) {
      val || this.closeDelete()
    },
    dialogLend (val) {
      val || this.closeLend()
    },
    dialogReturnGame (val) {
      val || this.closeReturnGame()
    },
    options: {
      handler () {
        this.page = this.options.page
        this.itemsPerPage = this.options.itemsPerPage
        this.getGames()
      },
      deep: true
    }
  },

  mounted () {
    this.$store.dispatch('friend/getFriendsSelectList')
      .then((response) => {
        this.friends = response.data
      })
  },

  methods: {

    getGames () {
      this.loading = true

      const payLoad = {
        page: this.page,
        size: this.itemsPerPage
      }

      return new Promise((resolve) => {
        this.$store.dispatch('game/getPagination', payLoad)
          .then((response) => {
            this.games = response.data.items
            this.totalGames = response.data.totalItems
            this.loading = false
            resolve()
          })
      })
    },

    deleteItem (item) {
      this.editedIndex = this.games.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },

    deleteItemConfirm () {
      this.$store.dispatch('game/delete', this.editedItem.id)
        .then((response) => {
          if (response.data.success) {
            this.games.splice(this.editedIndex, 1)
            this.closeDelete()
            this.notification('Jogo deletado com sucesso!', 'success')
          } else {
            this.notification(response.data.message[0].mensagem, response.data.message[0].tipo)
          }
        })
    },

    closeDelete () {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    lendItem (item) {
      this.dialogLend = true
      this.gameSelected.push({ id: item.id, text: item.name })
      this.lendGame.idGame = item.id
    },

    saveLend () {
      this.formHasErrors = false

      Object.keys(this.lendGameForm).forEach(f => {
        if (!this.lendGameForm[f]) this.formHasErrors = true
        this.$refs[f].validate(true)
      })

      const payLoad = {
        idGame: this.lendGameForm.idGame,
        idFriend: this.lendGameForm.friend
      }

      this.$store.dispatch('game/borrowGame', payLoad)
        .then((response) => {
          if (response.data.success) {
            this.notification('Jogo emprestado com sucesso!', 'success')
            this.getGames()
          } else {
            this.notification(response.data.message[0].mensagem, response.data.message[0].tipo)
          }
        })
        .finally(() => {
          this.closeLend()
        })
    },

    closeLend () {
      this.lendGame.idGame = 0
      this.lendGame.friend = ''
      this.gameSelected = []
      this.dialogLend = false
    },

    returnGame (item) {
      this.returnGameForm.idGame = item.id
      this.returnGameForm.idFriend = item.idFriend
      this.dialogReturnGame = true
    },
    returnGameConfirm (item) {
      const payLoad = {
        idGame: this.returnGameForm.idGame,
        idFriend: this.returnGameForm.idFriend
      }

      this.$store.dispatch('game/returnGame', payLoad)
        .then((response) => {
          if (response.data.success) {
            this.notification('Jogo devolvido com sucesso!', 'success')
            this.getGames()
          } else {
            this.notification(response.data.message[0].mensagem, response.data.message[0].tipo)
          }
        })
        .finally(() => {
          this.closeLend()
        })

      this.closeReturnGame()
    },

    closeReturnGame () {
      this.dialogReturnGame = false
      this.$nextTick(() => {
        this.returnGameForm.idGame = 0
        this.returnGameForm.idFriend = 0
      })
    },

    editItem (item) {
      this.editedIndex = this.games.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    save () {
      this.formHasErrors = false
      Object.keys(this.gameForm).forEach(f => {
        if (!this.gameForm[f] && this.gameForm[f] !== 0) this.formHasErrors = true
        this.$refs[f].validate(true)
      })
      if (!this.formHasErrors) {
        let request = ''

        if (this.editedItem.id === 0) {
          request = 'add'
        } else {
          request = 'update'
        }

        const payLoad = {
          id: this.editedItem.id || 0,
          name: this.editedItem.name,
          platform: parseInt(this.editedItem.platform),
          genre: parseInt(this.editedItem.genre)
        }

        this.$store.dispatch('game/' + request, payLoad)
          .then((response) => {
            if (response.data.success) {
              this.notification('Jogo salvo com sucesso!', 'success')
              this.getGames()
            } else {
              this.notification(response.data.message[0].mensagem, response.data.message[0].tipo)
            }
          })
          .finally(() => {
            this.close()
          })
      }
    },

    close () {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    notification (text, type) {
      this.messageStart = true
      this.message.text = text
      this.message.type = this.$options.filters.typeMessage(type)

      setTimeout(() => {
        this.messageStart = false
      }, 5000)
    }
  }
}
</script>
