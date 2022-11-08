<template>
  <form @submit.prevent="addToVault()" class="d-flex">
    <button
      v-if="!vKeep "
      class="btn border-0 mb-2 animate__animated animate__fadeIn"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#testMenu"
      aria-expanded="false"
      aria-controls="collapseExample"
    >
      <i
        class="mdi mdi-playlist-plus fs-3 text-purple dotHover p-1 selectable rounded"
        ><small>Save</small>
      </i>
    </button>
  </form>
  <div v-if="vKeepOwner">

    <button
      class="btn border-0 mb-2 animate__animated animate__fadeIn"
      @click="removeFromVault()"
      v-if="vKeep"
    >
      <i
        class="mdi mdi-playlist-remove fs-3 text-purple dotHover p-1 selectable rounded"
        ><small>Remove</small></i
      >
    </button>
  </div>
  <div
    class="collapse position-absolute end-100 bottom-50 rounded bShadow3"
    id="testMenu"
    v-if="!vKeep"
  >
    <h6
      class="mb-0 p-2 text-center bg-warning bShadow2 rounded-top no-select markoOne"
    >
      Save to vault
    </h6>
    <div class="bg-warning bShadow text-center p-3 scrollY">
      <AccountVaultList
        :vault="a"
        v-for="a in accountVaults"
        :key="a.id"
        @click="addToVault(a)"
      />
    </div>
    <div>
      <div class="d-flex dotHover bg-info rounded-bottom">
        <button
          class="btn me-3 d-flex align-items-center"
          data-bs-target="#vaultForm"
          data-bs-toggle="modal"
          @click="toggleCreateVault()"
        >
          <i class="mdi mdi-plus-box fs-2"></i>
          <h6 class="d-flex align-items-center m-0 ms-2 markoOne">
            Create vault
          </h6>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import { vaultKeepService } from "../services/VaultKeepService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { defineComponent } from "vue";
import Popper from "vue3-popper";
import { Modal } from "bootstrap";

export default {
  props: {},
  setup(props) {
    const editable = ref({});
    async function getAccountVaults() {
      try {
        await accountService.getAccountVaults();
      } catch (error) {
        Pop.error(error, "[getAccountVaults]");
      }
    }

    onMounted(() => {
      getAccountVaults();
    });
    watchEffect(() => {});

    return {
      editable,
      accountVaults: computed(() => AppState.accountVaults),
      vaults: computed(() => AppState.accountVaults),
      keep: computed(() => AppState.activeKeep),
      vKeepOwner: computed(() =>
        AppState.vKeepIds.find(
          (v) => v.creatorId == AppState.account.id
        )
      ),
      vKeep: computed(() =>
        AppState.vKeepIds.find((v) => v.keepId == AppState.activeKeep.id)
      ),
      account: computed(() => AppState.account.id),
      async addToVault(a) {
        try {
          let data = {
            vaultId: a.id,
            keepId: this.keep.id,
          };
          await vaultKeepService.createVaultKeep(data);
          AppState.activeKeep.kept++;
          Pop.success(`${this.keep.name} saved to ${a.name} `);
        } catch (error) {
          Pop.error(error, "[addToVault]");
        }
      },
      async removeFromVault() {
        try {
  
          if (await Pop.confirm()) {
            await vaultKeepService.deleteVaultKeep(this.vKeep.id);
            Pop.success(`${this.keep.name} removed from vault `);
          
             Modal.getOrCreateInstance("#activeKeep").hide();
        
          }
        } catch (error) {
          Pop.error(error, "[removeFromVault]");
        }
      },
      toggleCreateVault() {
        AppState.vaultEditForm = false;
        console.log(AppState.vaultEditForm);
      },
    };
  },
  components: { Popper },
};
</script>

<style lang="scss" scoped>
.scrollY {
  height: 58.5vh;
  overflow-y: auto;
}
::-webkit-scrollbar {
  display: none;
}
select {
  border: none !important;
  border-bottom: 3px solid rgb(20, 20, 20) !important;
  background: transparent;
  color: #a76bbd;
  font-size: 22px;
  font-weight: 600;
  padding: 0;
  text-align: center;
  option {
    border: none !important;
    border-radius: 4px !important;
  }
  .form-select {
    border: none !important;
    border-radius: 4px !important;
  }
}
select:focus {
  border: none !important;
}

@media only screen and (max-width: 768px) {
  .scrollY {
    height: 67.6vh;
  }

  #testMenu {
    transform: translateX(110%);
  }
}
</style>
