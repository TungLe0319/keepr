<template>
  <div class="container">
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
              <h1 class="lgText">{{ vault?.name }}</h1>
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-md-12 d-flex flex-column justify-content-center align-items-center"
      >
        <div class="col-md-12 justify-content-end d-flex mt-3 mb-1 px-5">
          <div class="d-flex align-items-center">
            <router-link :to="{name: 'Account'}"> 
            
              <img
              src="https://cdn-icons-png.flaticon.com/512/4211/4211391.png"
              alt="profile Icon"
              class=""
              width="30"
              height="30"
            />
         
            </router-link>
          </div>
          <div class="btn-group dropstart">
            <i
              class="mdi mdi-dots-horizontal ms-3 fs-1 action"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li class="dotHover rounded">
                <a
                  class="btn border-0"
                  data-bs-toggle="modal"
                  data-bs-target="#vaultForm"
                  @click="toggleEditForm()"
                  >Edit Vault</a
                >
              </li>
              <li class="dotHover rounded">
                <a class="btn border-0" @click="deleteVault()">Delete Vault</a>
              </li>
            </ul>
          </div>
        </div>

        <div class="d-flex">
          <span class="monoton">
            <h1>{{ keeps?.length }}</h1>
          </span>
          <span>
            <h1 class="monoton">keeps</h1>
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
</template>

<script>
import { authSettled, onAuthLoaded } from "@bcwdev/auth0provider-client";
import { propsToAttrMap } from "@vue/shared";
import { computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import PopperTip from "../components/PopperTip.vue";
import VaultCard from "../components/VaultCard.vue";
import { router } from "../router.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    onMounted(() => {
      getVaultById();
    });
    onAuthLoaded(() => {
      getKeepsByVaultId();
    });
    async function getKeepsByVaultId() {
      try {
        if (AppState.activeVault) {
          await vaultsService.getKeepsByVaultId(AppState.activeVault.id);
        }
      } catch (error) {
        Pop.error(error, "[getKeepsByVaultId]");
      }
    }
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);
      } catch (error) {
        Pop.error(error, "[getVaultById]");
      }
    }
    const route = useRoute();
    return {
      route,
      keeps: computed(() => AppState.vaultedKeeps),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),

      async deleteVault() {
        try {
          if (
            await Pop.confirm(
              `Are you sure you want to delete ${AppState.activeVault.name} ?`,
              "theres no undoing this"
            )
          ) {
            let vaultId = AppState.activeVault.id;
            await vaultsService.deleteVault(vaultId);
            router.push({ name: "Account" });
          }
        } catch (error) {
          Pop.error(error, "[deleteVault]");
        }
      },

      toggleEditForm() {
        AppState.vaultEditForm = true;
      },
    };
  },
  components: { VaultCard, PopperTip },
};
</script>

<style scoped>
.lgText {
  font-size: 80px !important;

  text-shadow: 0 1px 0 #ccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbb, 0 4px 0 #b9b9b9,
    0 5px 0 #aaa, 0 6px 1px rgba(0, 0, 0, 0.1), 0 0 5px rgba(0, 0, 0, 0.1),
    0 1px 3px rgba(0, 0, 0, 0.3), 0 3px 5px rgba(0, 0, 0, 0.2),
    0 5px 10px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.2),
    0 20px 20px rgba(0, 0, 0, 0.15);
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
