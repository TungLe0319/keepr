<template>
  <div class="container" v-if="vault">
    <div class="row">
      <div class="col-md-12 text-center mt-2">
        <div class="card border-0">
          <img
            v-if="vault?.img"
            :src="vault?.img"
            alt=""
            class="img-fluid coverImg bShadow rounded"
          />
          <img
            v-else
            src=""
            alt=""
            class="img-fluid coverImg bShadow rounded skeleton-loader"
          />
          <div
            class="card-img-overlay d-flex justify-content-center align-items-end"
          >
            <div class="text-center markoOne">
              <h1 class="lgText text-constantLight no-select" title="Vault name">{{ vault?.name }}</h1>
            </div>
             <span v-if="vault?.isPrivate">
          <i class="mdi mdi-shield-lock-outline fs-1 text-constantLight ms-4 lgText" title="Currently in one of your private vaults"></i>
  
        </span>
          </div>
        </div>
      </div>
      <div
        class="col-md-12 d-flex flex-column justify-content-center align-items-center"
      >
        <div class="col-md-12 justify-content-end d-flex mt-3 mb-1 px-3">
          <div class="d-flex align-items-center me-2"> 

<img :src="vault?.creator?.picture" alt="" width="40" height="40" class="rounded-circle dotHover" :title="vault?.creator?.name" >

          </div>
          <div class="d-flex align-items-center">
        
         
              <i title="back to previous page" class="mdi mdi-rewind fs-1 text-dark dotHover btn p-0 px-1 border-0" @click="$router?.go(-1)"></i>
        
          </div>
          <div class="btn-group dropstart">
            <i
              v-if="owner"
              class="mdi mdi-dots-horizontal ms-3 fs-1 action text-dark rounded px-1 dotHover"
              data-bs-toggle="dropdown"
              aria-expanded="false"
              title="Vault options"
            ></i>
       
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li
                class="dotHover rounded p-2 text-center"
                data-bs-toggle="modal"
                data-bs-target="#vaultForm"
                @click="toggleEditForm()"
              >
                Edit Vault
              </li>
              <li
                class="dotHover rounded p-2 text-center"
                @click="deleteVault()"
              >
                Delete Vault
              </li>
            </ul>
          </div>
        </div>

        <div class="d-flex">
          <span class="monoton text-dark">
            <h1>{{ keeps?.length }}</h1>
          </span>
          <span>
            <h1 class="monoton text-dark">keeps</h1>
          </span>
        </div>
      </div>
    </div>

    <div class="bricks">
      <div class="my-3" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
  <div v-if="!owner && private" class="text-center mt-5">
    <img
      src="https://cdn-icons-png.flaticon.com/512/2913/2913133.png"
      alt=""
      class="img-fluid rounded-circle dotHover btn border-0"
    />
    <h1 class="text-center mt-5 markoOne">THIS IS A PRIVATE VAULT</h1>
  </div>
</template>

<script>
import { authSettled, onAuthLoaded } from "@bcwdev/auth0provider-client";
import { propsToAttrMap } from "@vue/shared";

import { computed, onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import PopperTip from "../components/PopperTip.vue";
import VaultCard from "../components/VaultCard.vue";
import { router } from "../router.js";
import { accountService } from "../services/AccountService.js";
import { vaultKeepService } from "../services/VaultKeepService.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    onMounted(() => {
      
      getVaultById();
      getKeepsByVaultId();
   
    });
    onAuthLoaded(()=>{
      
      // getVaultById();
      // getKeepsByVaultId();
    })
    async function getKeepsByVaultId() {
      try {
        
        await vaultsService.getKeepsByVaultId(route.params.id);
      } catch (error) {
        // router.push({ name: "Home" });
        // Pop.error(error, "[getKeepsByVaultId]");
         Pop.toast(`<img src="https://media.tenor.com/j1U9chTe2_0AAAAi/nope-finger-wag.gif" height="80" width="80" > this is Private`);
         logger.error(error)
        router.go(-1)
      }
    }
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);
        console.log("[creatorId]",AppState.activeVault.creator.id);
        console.log("[userId]",AppState.user.id);
      } catch (error) {
        Pop.toast(`<img src="https://media.tenor.com/j1U9chTe2_0AAAAi/nope-finger-wag.gif" height="80" width="80" > this is Private`);
        logger.error(error);
        // router.push({ name: "Home" });
      }
    }
    const route = useRoute();
    const router = useRouter();
    return {
      route,
      router,
      Vaulted: computed(() => AppState.vKeepIds),
      keeps: computed(() => AppState.vaultedKeeps),
      account: computed(() => AppState.account),
      vault: computed(() => AppState?.activeVault),
      private: computed(() => AppState.activeVault?.isPrivate == true),
      owner: computed(() => AppState.activeVault?.creator?.id == AppState.user?.id),
     ownerAndPrivate: computed(
        () =>
          AppState.user?.id == AppState.activeVault?.creator?.id &&
          AppState.activeVault?.isPrivate == true
      ),
      async deleteVault() {
        try {
          if (
            await Pop.confirm(
              `Are you sure you want to delete  ${AppState.activeVault?.name}  ?`,
              "theres no undoing this"
            )
          ) {
            let vaultId = AppState.activeVault?.id;
            await vaultsService.deleteVault(vaultId);
            router.push({ name: "Account" });
          }
        } catch (error) {
          Pop.error(error, "[deleteVault]");
        }
      },

      async deleteVaultKeep() {
        try {
          let id = this.vault.id;
          await vaultKeepService.deleteVaultKeep(id);
        } catch (error) {
          Pop.error(error, "[]");
        }
      },

      toggleEditForm() {
        AppState.vaultEditForm = true;
      },
      push() {
        router.push({ name: "Home" });
      },
    };
  },
  components: { VaultCard, PopperTip },
};
</script>

<style scoped>
.lgText {
  font-size: 80px !important;
  text-shadow: 2px 2px 0px rgba(0, 0, 0, 0.459);

}
.privateIcon{
 font-size: 80px !important;
}

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
