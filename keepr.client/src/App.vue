<template>
  <header>
    <Navbar />
    
  </header>
  <main class="bg-warning">

    <router-view />
  </main>
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
  components: { Navbar, ActiveKeepCard, ModalComponent, ExampleForm, AccountForm, VaultForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
