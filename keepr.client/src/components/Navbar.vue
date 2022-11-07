<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-warning px-3 bShadow">
    <router-link class="navbar-brand d-flex me-1" :to="{ name: 'Home' }">
      <button class="btn p-0 dotHover">
        <i
          class="mdi mdi-home-circle fs-1 markoOne text-dark  px-2 rounded"
        ></i>
      </button>
    </router-link>
    <button
      class="navbar-toggler border-0 dotHover py-2"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span >
        <img src="https://cdn-icons-png.flaticon.com/512/6015/6015685.png" alt="navbar icon" width="30" height="30" class="test">
      </span>
    </button>
    <div class="collapse navbar-collapse " id="navbarText">
      <div class="d-flex  ">
  <button
            class="btn dotHover text-dark"
            data-bs-toggle="modal"
            data-bs-target="#createForm"
            @click="toggleCreateForm()"
          >
            <h4 class="markoOne mb-0 ">Create</h4>
          </button>


<ToggleTheme/>

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
           <img
        src="../assets/img/Keepr logo.png"
        alt=""
        width="70"
        height="60"
        class=""
      />
      </div>
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
      <Login />
    </div>
  </nav>
</template>

<script>
import { AppState } from "../AppState.js";
import Login from "./Login.vue";
import ToggleTheme from "./ToggleTheme.vue";
export default {
  setup() {
    return {
      toggleCreateForm() {
        AppState.keepEditForm = false;
      },
     
    };
  },
  components: { Login },
};
</script>

<style scoped>
.test{

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
    height: 64px;
  }
}
</style>
