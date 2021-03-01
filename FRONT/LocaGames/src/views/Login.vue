<template>
<v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="4">
            <v-card class="elevation-12">
                <v-toolbar color="primary" dark flat>
                    <v-toolbar-title>Login</v-toolbar-title>
                    <v-spacer></v-spacer>
                </v-toolbar>
                <v-card-text>
                    <v-form>
                        <v-text-field label="Login" v-model="email" name="login" prepend-icon="mdi-account" type="text"></v-text-field>

                        <v-text-field id="password" v-model="pwd" label="Password" name="password" prepend-icon="mdi-lock" type="password"></v-text-field>
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="primary" @click="login">Login</v-btn>
                </v-card-actions>
            </v-card>
        </v-col>
    </v-row>
</v-container>
</template>

<script>
export default {

  data () {
    return {
      email: '',
      pwd: ''
    }
  },

  methods: {

    login () {
      const payload = {
        email: this.email,
        pwd: this.pwd
      }
      this.$store.dispatch('login/login', payload)
        .then((response) => {
          if (response.data.status) {
            this.$acl.change('admin')
            this.$router.push('/home')
          }
        })
    }
  }

}
</script>
