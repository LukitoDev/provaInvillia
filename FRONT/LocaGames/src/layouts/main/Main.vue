<template>
<div>
    <v-navigation-drawer v-model="drawer" app>
        <v-list dense>
            <v-list-item link @click.stop="goTo('home')">
                <v-list-item-action>
                    <v-icon>mdi-home</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>Home</v-list-item-title>
                </v-list-item-content>
            </v-list-item>

            <v-list-item link @click.stop="goTo('game')">
                <v-list-item-action>
                    <v-icon>mdi-gamepad</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>Jogos</v-list-item-title>
                </v-list-item-content>
            </v-list-item>

            <v-list-item link @click.stop="goTo('friend')">
                <v-list-item-action>
                    <v-icon>mdi-account</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>Amigo</v-list-item-title>
                </v-list-item-content>
            </v-list-item>

            <v-list-item link @click.stop="leave">
                <v-list-item-action>
                    <v-icon>mdi-logout</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>Sair</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
    </v-navigation-drawer>

    <v-app-bar app color="indigo" dark>
        <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
        <v-toolbar-title>LocaGames</v-toolbar-title>
    </v-app-bar>

    <v-main>
        <v-container class="" fluid>
            <v-row align="center" justify="center">
                <v-col class="text-center">
                    <router-view></router-view>
                </v-col>
            </v-row>
        </v-container>
    </v-main>
    <v-footer color="indigo" app>
        <span class="white--text">&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
</div>
</template>

<script>
export default {
  name: 'Main',
  data: () => ({
    drawer: null
  }),

  methods: {

    goTo (route) {
      const newRoute = '/' + route
      if (newRoute !== this.$router.history.current.path) this.$router.push('/' + route)
    },

    leave (route) {
      localStorage.clear()
      this.$router.push('/login')
    }
  }
}
</script>
