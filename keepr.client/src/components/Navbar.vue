<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-warning navShadow">
    <router-link class="navbar-brand d-flex me-1" :to="{ name: 'Home' }">
      <button class="btn p-0 dotHover ms-2">
        <i class="mdi mdi-home-circle fs-1 markoOne text-dark px-2  rounded"></i>
      </button>
    </router-link>
    <button
      class="navbar-toggler border-0 dotHover bg-dark my-3"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div
      class="collapse navbar-collapse justify-content-between"
      id="navbarText"
    >
      <div class="d-flex align-items-center justify-content-around">
        <div>
        
          <div v-if="user?.isAuthenticated" class="dropdown d-flex dotHover p-1 rounded">
         
            <button
              class="btn d-flex px-1"
              type="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
             <h4 class="markoOne mb-0 d-flex align-items-center">Create       <i class="mdi mdi-triangle-small-down fs-3"></i></h4>
       
          </button>
            <ul class="dropdown-menu  p-0 bg-info">
              <li
                    class="text-center  rounded p-1 markoOne dropdown-item "
                  data-bs-toggle="modal"
                  data-bs-target="#createForm"
                  @click="toggleCreateForm()">
              Keep
              </li>
              <li
                 data-bs-toggle="modal"
                  data-bs-target="#vaultForm"
                  @click="toggleCreateVault()"
              class="text-center selectHover rounded p-1 markoOne dropdown-item"> Vault</li>
            
            </ul>
          </div>
        </div>

        <!-- <ToggleTheme /> -->
        <i
          v-if="route.name == 'Home'"
          class="mdi mdi-magnify fs-1 dotHover text-dark mx-1 px-1 rounded"
          id="button-addon2"
          data-bs-toggle="collapse"
          data-bs-target="#collapseSearchBar"
          title="Search"
        ></i>
        <Login class="d-md-none " />
      </div>
      <img
        src="../assets/img/Keepr logo.png"
        alt="keepr Logo"
        width="70"
        height="60"
        class="d-none d-md-block"
        title="keepr Logo"
      />
      <!-- <ul class="navbar-nav d-flex">
        <li class="d-flex align-items-center">
          <button
            class="btn dotHover"
            data-bs-toggle="modal"
            data-bs-target="#createForm"
            @click="toggleCreateForm()"
          >
            <h4 class="markoOne mb-0">Create</h4>
          </button>
        </li>
        <li class="mx-1">
          <button class="btn dotHover" @click="toggleTheme()">
            <img
              src="https://cdn-icons-png.flaticon.com/512/5005/5005681.png"
              alt="theme Button"
              width="40"
              height="40"
            />
   
          </button>
        </li>
        <li class="">
          <button
            class="btn dotHover"
            type="submit"
            id="button-addon2"
            data-bs-toggle="collapse"
            data-bs-target="#collapseSearchBar"
            title="Search"
          >
            <img
              src="https://cdn-icons-png.flaticon.com/512/751/751463.png"
              alt=""
              width="40"
              height="40"
            />
          </button>
        </li>
  
      </ul> -->

      <!-- LOGIN COMPONENT HERE -->
      <Login class="d-none d-md-block" />
    </div>
  </nav>
</template>

<script>
import { computed } from "@vue/reactivity";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import Login from "./Login.vue";
import ToggleTheme from "./ToggleTheme.vue";

export default {
  setup() {
    const route = useRoute();
    return {
      user:computed(() => AppState.user),
      // theme: computed(() => AppState.account?.theme),
      route,
      toggleCreateForm() {
        // AppState.activeKeep = {}
        AppState.keepEditForm = false;
      },
      toggleCreateVault(){
        AppState.vaultEditForm = false
      }
    };
  },
  components: { Login },
};
</script>

<style scoped>
.navShadow{
  box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
}
.test {
}
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}
button {
  border: none;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
button:active {
  box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px,
    rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
}
@media screen and (min-width: 768px) {
  nav {
    height: 70px;
  }
}
</style>
