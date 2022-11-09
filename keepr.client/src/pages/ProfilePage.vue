<template>
  <div class="container animate__animated animate__fadeIn">
    <div class="row mt-3 ">
      <div class="col-md-12 text-center mt-2 position-relative">
        <div class="card border-0 animate__animated animate__fadeIn">
          <img
            v-if="profile?.coverImg"
            :src="profile?.coverImg"
            alt=" account cover Image"
            class="img-fluid coverImg bShadow rounded"
          />
          <img
            v-else
            src="https://quotefancy.com/media/wallpaper/3840x2160/1715705-Ashley-Smith-Quote-Life-is-full-of-beauty-Notice-it.jpg"
            class="img-fluid coverImg bShadow rounded skeleton-loader"
          />
          <div
            class="card-img-overlay d-flex  justify-content-center align-items-end"
          >
            <div
              v-if="profile?.picture"
              class="text-center position-absolute top-50"
            >
              <img
                class="bShadow p-1 profilePic"
                :src="profile?.picture"
                alt=""
              />
            </div>
            <div v-else class="text-center position-absolute top-50">
              <img class="bShadow p-1 profilePic skeleton-loader" />
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-md-12 d-flex flex-column mt-5 justify-content-center align-items-center"
      >
        <div class="col-md-10 justify-content-end  mt-5 d-flex  px-3">
          <div class="btn-group dropstart">
            <i
            v-if="profile?.id == account?.id"
              class="mdi mdi-dots-horizontal ms-3 fs-1 selectable text-dark"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
             <!-- <div class="d-flex align-items-center">
        
         
              <i title="back to previous page" class="mdi mdi-rewind fs-1 text-dark" @click="$router.go(-1)"></i>
        
          </div> -->
            <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
              <li
                class="dotHover rounded p-2 text-center"
                data-bs-toggle="modal"
                data-bs-target="#vaultForm"
                @click="toggleCreateForm()"
                title="Profile "
              >
                Add Vault
              </li>
              <li
                class="dotHover rounded p-2 text-center"
                data-bs-toggle="modal"
                data-bs-target="#accountForm"
              >
                Edit Account
              </li>
            </ul>
          </div>
        </div>

        <h3 class="markoOne text-dark">{{ profile?.name }}</h3>
        <div class="text-center d-flex align-items-center no-select text-dark">
          <span class="bShadow rounded p-1">{{ vaults.length }} Vaults</span>

          <h1 class="mx-2 rounded-5 no-select">|</h1>
          <span class="bShadow rounded p-1">{{ keeps.length }} Keeps</span>
        </div>
      </div>
    </div>
    <!-- ------------------VAULTS AND KEEPS------------------------------------------------ -->
     <div class="row">
      <div class="col-md-12 text-dark text-center">
        <button
          @click="scrollDown()"
          class="btn text-dark "
          :class="focusedBtn1 ==false? 'focusedBtn' : ''"
          data-bs-toggle="collapse"
          data-bs-target="#vaultCollapse"
        >
          <h3>Vaults</h3>
        </button>
        <button
          @click="scrollDown()"
  class="btn text-dark"
            :class="focusedBtn1 ==true? 'focusedBtn' : ''"
          data-bs-toggle="collapse"
          data-bs-target="#keepCollapse"
        >
          <h3>Keeps</h3>
        </button>
      </div>
      <div
        class="collapse animate__animated animate__fadeInLeft show"
        id="vaultCollapse"
      >
          <div class="col-md-12 border-bottom  mt-2 mb-3 border-2 bg-dark"></div>
        <div class="row">
          <div class="col-md-3" v-for="v in vaults" :key="v.id">
            <VaultCard :vault="v" />
          </div>
        </div>
      </div>
    </div>
    <div class="collapse animate__animated animate__fadeInLeft" id="keepCollapse">
      <div class="col-md-12 p-1 border-bottom border-dark mt-2 mb-3 border-3"></div>
      <div class="bricks ">
        <div class="my-3" v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <!-- ------------------------------------------------------------------------------------------------------------------------------------- -->
  <div></div>
</template>

<script>
import { onAuthLoaded } from "@bcwdev/auth0provider-client";

import { computed, onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import VaultCard from "../components/VaultCard.vue";
import { accountService } from "../services/AccountService.js";
import { profilesService } from "../services/ProfilesService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
      async function getProfileById() {
            try {
              await profilesService.getProfileById(route.params.id);
            } catch (error) {

              Pop.error(error, "[getProfileById]");
            }
          }
    async function getProfileKeeps() {
      try {
        await profilesService.getKeepsForProfiles(route.params.id);
      } catch (error) {
        Pop.error(error, "[getAccountKeeps]");
      }
    }
    async function getProfileVaults() {
      try {
        await profilesService.getVaultsForProfiles(route.params.id);
      } catch (error) {
        Pop.error(error, "[getAccountVaults]");
      }
    }
    onMounted(() => {
      getProfileById()
      getProfileKeeps();
      getProfileVaults();
    });
    onAuthLoaded(() => {});
    watchEffect(() => {});
    const route = useRoute();
    const router = useRouter();
    return {
      route,
      router,
         focusedBtn1: computed(() => AppState.focusedBtn1),
      keeps: computed(() => AppState.profileKeeps),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      toggleCreateForm() {
        AppState.vaultEditForm = false;
        console.log(AppState.vaultEditForm);
      },
          scrollDown() {
    AppState.focusedBtn1 = !AppState.focusedBtn1
      },
    };
  },
  components: { VaultCard },
};
</script>

<style scoped>
.container{
    --animate-duration: 500ms;
  --animate-delay: 1s;
}
.coverImg {
  height: 250px;
  object-position: center;

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
