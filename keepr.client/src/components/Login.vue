<template>
  <span class="navbar-text me-2">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else class="d-flex">
      <div class="dropdown dropstart my-2 my-lg-0 ">
        <div type="button" class=" border-0 " data-bs-toggle="dropdown"
          aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo"  class="rounded-circle  pImg" />
          </div>
          <div v-else>
            <img src="" alt="account photo"  class="rounded-circle elevation-3 pImg skeleton-loader" />
          </div>
        </div>
      </div>



  <button class="btn position-relative" type="button" data-bs-toggle="collapse" data-bs-target="#collapseAuth" aria-expanded="false" aria-controls="collapseExample">
  <i class="mdi mdi-arrow-down-box text-dark"></i>
  </button>

<div class="collapse position-absolute top-100" id="collapseAuth" style="z-index: 9">
  <div class="card card-body text-dark p-0">
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
  height: 60px;
  width: 60px;
box-shadow: rgba(0, 0, 0, 0.4) 0px 2px 4px, rgba(0, 0, 0, 0.3) 0px 7px 13px -3px, rgba(0, 0, 0, 0.2) 0px -3px 0px inset;
}
</style>
