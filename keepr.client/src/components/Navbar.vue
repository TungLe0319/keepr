<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-warning px-3 bShadow">
    <router-link class="navbar-brand d-flex me-1" :to="{ name: 'Home' }"  >
      <button class="btn dotHover">
        <h4 class="markoOne text-dark mb-0">Home</h4>
      </button>
    </router-link>
    <button
      class="navbar-toggler bg-dark"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse " id="navbarText">
      <ul class="navbar-nav">
        <li class="d-flex align-items-center" >
          <button
            class="btn dotHover "
            data-bs-toggle="modal"
            data-bs-target="#createForm"
            @click="toggleCreateForm()"
          >
            <h4 class="markoOne mb-0 ">Create</h4>
          </button>
        </li>
        <li class="ms-1">
         <button class="btn dotHover  " @click="toggleTheme()">
          <img src="https://cdn-icons-png.flaticon.com/512/5005/5005681.png" alt="theme Button" width="40" height="40">
         </button>
        </li>
      </ul>
      <img src="../assets/img/Keepr logo.png" alt="" width="60" height="60" class="mx-auto" />
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { AppState } from "../AppState.js";
import Login from "./Login.vue";
export default {
  setup() {
    return {
      toggleCreateForm() {
        AppState.keepEditForm = false;
      },
      toggleTheme(){
        console.log('hi');
        document.body.classList.toggle = 'dark'
      }
    };
  },
  components: { Login },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}
button{
  border: none;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
button:active{
box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px, rgba(60, 64, 67, 0.15) 0px 1px 3px 1px;
}
@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
