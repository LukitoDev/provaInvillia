<template>
<div>
    <v-alert outlined :color="message.type" v-if="messageStart">
        <div>{{message.text}}</div>
    </v-alert>
    <v-data-table :headers="headers" :items="friends" :options.sync="options" :server-items-length="totalFriends" :loading="loading" class="elevation-1" disable-sort :footer-props="footerProps"
                   loading-text="Carregando amigos" no-data-text="Nenhum amigo encontrado">
        <template v-slot:top>
            <v-toolbar flat>
                <v-toolbar-title>Amigos</v-toolbar-title>
                <v-divider class="mx-4" inset vertical></v-divider>
                <v-spacer></v-spacer>
                <v-dialog v-model="dialog" max-width="500px">
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
                                    <v-col cols="12" sm="6" md="6">
                                        <v-text-field v-model="editedItem.name" label="Nome" ref="name" :rules="[() => !!editedItem.name || 'Nome é obrigatório']"></v-text-field>
                                    </v-col>
                                    <v-col cols="12" sm="6" md="6">
                                        <v-text-field v-model="editedItem.lastName" label="Sobrenome" ref="lastName" :rules="[() => !!editedItem.lastName || 'Sobrenome é obrigatório']"></v-text-field>
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
            </v-toolbar>
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

        </template>
    </v-data-table>
</div>
</template>

<script>
export default {
  data: () => ({
    page: 1,
    itemsPerPage: 10,
    dialog: false,
    dialogDelete: false,
    totalFriends: 0,
    loading: false,
    headers: [{
      text: 'Nome',
      value: 'name'
    },
    {
      text: 'Sobrenome',
      value: 'lastName'
    },
    {
      text: 'Actions',
      value: 'actions'
    }
    ],
    friends: [],
    options: {},
    footerProps: {
      'items-per-page-options': [5, 10, 15, 20],
      'items-per-page-text': 'Amigos por página'
    },
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: '',
      lastName: ''
    },
    defaultItem: {
      id: 0,
      name: '',
      lastName: ''
    },
    formHasErrors: false,
    messageStart: false,
    message: {
      text: '',
      type: ''
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'Adicionar amigo' : 'Editar amigo'
    },
    friendForm () {
      return {
        name: this.editedItem.name,
        lastName: this.editedItem.lastName
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
    options: {
      handler () {
        this.page = this.options.page
        this.itemsPerPage = this.options.itemsPerPage
        this.getFriends()
      },
      deep: true
    }
  },

  methods: {

    getFriends () {
      this.loading = true

      const payLoad = {
        page: this.page,
        size: this.itemsPerPage
      }

      return new Promise((resolve, reject) => {
        this.$store.dispatch('friend/getPagination', payLoad)
          .then((response) => {
            this.friends = response.data.items
            this.totalFriends = response.data.totalItems
            this.loading = false
            resolve()
          })
      })
    },

    deleteItem (item) {
      this.editedIndex = this.friends.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },

    deleteItemConfirm () {
      this.$store.dispatch('friend/delete', this.editedItem.id)
        .then((response) => {
          if (response.data.success) {
            this.friends.splice(this.editedIndex, 1)
            this.closeDelete()
            this.notification('Amigo deletado com sucesso!', 'success')
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

    editItem (item) {
      this.editedIndex = this.friends.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    save () {
      this.formHasErrors = false
      Object.keys(this.friendForm).forEach(f => {
        if (!this.friendForm[f]) this.formHasErrors = true
        this.$refs[f].validate(true)
      })
      if (!this.formHasErrors) {
        let request = ''

        if (this.editedItem.id === 0) { request = 'add' } else { request = 'update' }

        const payLoad = {
          id: this.editedItem.id || 0,
          name: this.editedItem.name,
          lastName: this.editedItem.lastName
        }

        this.$store.dispatch('friend/' + request, payLoad)
          .then((response) => {
            if (response.data.success) {
              this.notification('Amigo salvo com sucesso!', 'success')
              this.editedItem.id = response.data.resource.id
              this.editedItem.name = response.data.resource.name
              this.editedItem.lastName = response.data.resource.lastName
              this.getFriends()
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
      this.message.type = type

      setTimeout(() => {
        this.messageStart = false
      }, 5000)
    }
  }
}
</script>
