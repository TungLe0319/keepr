<template>
  <form @submit.prevent="addToVault()" class="d-flex py-2">
    <div class="btn-group" v-if="!vKeep">
      <select
        v-model="editable"
        class="form-select "
        aria-label="Default select example"
      >
        <option class="bg-warning text-dark " :value="v" v-for="v in vaults">
          {{ v.name }}
        </option>
      </select>
    </div>
    <div class="border-bottom border-3 " v-else>
      <i
        class="mdi mdi-playlist-remove fs-3 text-purple dotHover p-1 selectable rounded"
        @click="removeFromVault()"
        ><small>Remove</small></i
      >
    </div>
    <button type="submit" class="btn" v-if="!vKeep">Save</button>
    
  </form>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import { vaultKeepService } from "../services/VaultKeepService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

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
      vaults: computed(() => AppState.accountVaults),
      keep: computed(() => AppState.activeKeep),
      vaulted: computed(() =>
        AppState.vKeepIds.find((v) => v.creatorId == AppState.user.id)
      ),
      vKeep: computed(() =>
        AppState.vKeepIds.find((v) => v.keepId == AppState.activeKeep.id)
      ),
      async addToVault() {
        try {
          let data = {
            vaultId: editable.value.id,
            keepId: this.keep.id,
          };
          await vaultKeepService.createVaultKeep(data);
          AppState.activeKeep.kept++;
          Pop.success(`${this.keep.name} saved to ${editable.value.name} `);
        } catch (error) {
          Pop.error(error, "[addToVault]");
        }
      },
      async removeFromVault() {
        try {
          console.log(this.vKeep.id);
          console.log(AppState.account.id, "[accountId]");
          console.log(AppState.user.id, "[userId]");

          if (await Pop.confirm()) {
            await vaultKeepService.deleteVaultKeep(this.vKeep.id);
            Pop.success(
              `${this.keep.name} removed from ${editable.value.name} `
            );
          }
        } catch (error) {
          Pop.error(error, "[removeFromVault]");
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
select{
  border: none !important;
  background: transparent;
  color:#a76bbd ;
  font-size: 22px;
  font-weight: 600;
  padding: 0;
  text-align: center;
  option{
      border: none !important;
    border-radius: 4px !important;
  }
  .form-select{
      border: none !important;
    border-radius: 4px !important;
}
}
select:focus{
  border: none!important;
}


</style>
