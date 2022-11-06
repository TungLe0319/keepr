<template>
  <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown dropstart my-2 my-lg-0 ">
        <div type="button" class=" border-0 " data-bs-toggle="dropdown"
          aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo"  class="rounded-circle elevation-3 pImg" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-left p-0 " aria-labelledby="authDropdown">
          <ul class="list-group ">
            <router-link :to="{ name: 'Account' }">
              <li class="list-group-item ">
                Manage Account
              </li>
            </router-link>
            <li class="list-group-item  text-danger selectable " @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </li>
          </ul>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.pImg{
  height: 50px;
  width: 50px;
}
</style>
