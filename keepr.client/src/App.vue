<template>
  <header class=" ">
    <Navbar class="d-none d-md-flex navBarShadow " />
    
  </header>
  <main class="bg-warning ">

    <router-view />
  </main>
 

       <!-- <Navbar class="p-0 m-0 fixed-bottom d-flex d-md-none justify-content-end" /> -->

<FooterNav class="p-0 m-0 fixed-bottom d-flex d-md-none justify-content-end"/>
  <ModalComponent id="activeKeep">
<ActiveKeepCard :keep="keep" v-if="keep" />
  </ModalComponent>

  <ModalComponent id="createForm">
    <ExampleForm/>
  </ModalComponent>

  <ModalComponent id="accountForm">
    <AccountForm/>
  </ModalComponent>
  <ModalComponent id="vaultForm">
    <VaultForm/>
  </ModalComponent>
</template>

<script>
import { Modal } from "bootstrap"
import { computed } from 'vue'
import { AppState } from './AppState'
import AccountForm from "./components/AccountForm.vue"
import ActiveKeepCard from "./components/ActiveKeepCard.vue"
import ExampleForm from "./components/CreateForm.vue"
import FooterNav from "./components/FooterNav.vue"
import ModalComponent from "./components/ModalComponent.vue"
import Navbar from './components/Navbar.vue'
import VaultForm from "./components/VaultForm.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState),
      keep: computed(() => AppState.activeKeep),
      modalOpen:computed(() => {
        Modal.getOrCreateInstance("#activeKeep")
       
      }),
    }
  },
  components: { Navbar, ActiveKeepCard, ModalComponent, ExampleForm, AccountForm, VaultForm, FooterNav }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 70px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
