<template>
  <div class="container">
    <div class="row">
      <div class="col-md-12 text-center mt-2 position-relative">
        <div class="card border-0">
          <img
            :src="account.coverImg"
            alt=""
            class="img-fluid coverImg bShadow rounded"
          />
          <div
            class="card-img-overlay d-flex justify-content-center align-items-end"
          >
            <div class="text-center position-absolute top-50">
              <img
                class="bShadow p-1 profilePic"
                :src="account.picture"
                alt=""
              />
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-md-12 d-flex flex-column justify-content-center align-items-center"
      >
        <div class="col-md-12 justify-content-end d-flex mt-3  px-5">
          <div class="btn-group dropstart">
            <i
              class="mdi mdi-dots-horizontal ms-3 fs-1 selectable"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li class="dotHover rounded">
                <a
                  class="btn"
                  data-bs-toggle="modal"
                  data-bs-target="#vaultForm"
                  @click="toggleCreateForm()"
                  >Add Vault</a
                >
              </li>
              <li class="dotHover rounded">
                <a
                  class="btn"
                  data-bs-toggle="modal"
                  data-bs-target="#accountForm"
                  >Edit Account</a
                >
              </li>
            </ul>
          </div>
        </div>

        <h3 class="markoOne">{{ account.email }}</h3>
        <div>
          <span>{{ vaults.length }} Vaults</span>
          <span>{{ keeps.length }} Keeps</span>
        </div>
      </div>
    </div>

    <div class="row">
      <div class="col-md-12">
        <h3>Vaults</h3>
      </div>
<div class="row  scrollX">

  <div class="col-md-4" v-for="v in vaults" :key="v.id">
    <VaultCard :vault="v" />
  </div>
</div>
    </div>
    <div class="col-md-12 mt-4">
      <h3>Keeps</h3>
    </div>
    <div class="bricks">
      <div class="my-3" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>

  <div>
  </div>
</template>

<script>
import { onAuthLoaded } from "@bcwdev/auth0provider-client";

import { computed, onMounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import VaultCard from "../components/VaultCard.vue";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    async function getAccountKeeps() {
      try {
        await accountService.getAccountKeeps();
      } catch (error) {
        Pop.error(error, "[getAccountKeeps]");
      }
    }
    async function getAccountVaults() {
      try {
        await accountService.getAccountVaults();
      } catch (error) {
        Pop.error(error, "[getAccountVaults]");
      }
    }
    onMounted(() => {
      
      
      getAccountKeeps();
      getAccountVaults();
    });
    onAuthLoaded(() => {});
    watchEffect(()=>{
      if (useRoute == "Account") {
        AppState.keeps = []
      }
    })
 
    return {
    
      keeps: computed(() => AppState.accountKeeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.accountVaults),
      toggleCreateForm() {
        AppState.vaultEditForm = false;
        console.log(AppState.vaultEditForm);
      },
    };
  },
  components: { VaultCard },
};
</script>

<style scoped>
.coverImg {
  height: 300px;

  object-fit: cover;
}
.pImg {
  box-shadow: none;
}
.profilePic {
  border-radius: 50%;
  width: 200px;
  height: 200px;
}
.bricks {
  columns: 4;
  grid-template-columns: auto minmax(0, 1fr);
}

@media only screen and (max-width: 768px) {
  .bricks {
    columns: 2;
  }
}
</style>
